namespace POC.Serilog.WorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private HttpClient _httpClient;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[{DateTime.Now}]::StartAsync::service started");
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "DemoWorkerService2");
        return base.StartAsync(cancellationToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[{DateTime.Now}]::StopAsync::service stoped");
        _httpClient.Dispose();
        return base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var result = await _httpClient.GetAsync("https://api.github.com/gists/public");
            if (result.IsSuccessStatusCode)
            {
                var data = await result.Content.ReadAsStringAsync();
                _logger.LogInformation($"[{DateTime.Now}]::ExecuteAsync::{result.StatusCode}");
            }
            else
            {
                _logger.LogWarning($"[{DateTime.Now}]::ExecuteAsync::{result.StatusCode}");
            }
            await Task.Delay(3000, stoppingToken);
        }
    }
}