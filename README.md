### packages
```bash
Microsoft.Extensions.Hosting
Microsoft.Extensions.Hosting.WindowsServices
Serilog.AspNetCore
Serilog.Sinks.File
Serilog.Sinks.Console
```

### commands to run as windows service
```bash
# open cmd/terminal
(windows) open cmd with administrator

# Create a Windows Service
sc create MyWS DisplayName="MyWorkerService" binPath="C:\full_path\workerservice.exe"

# Start a Windows Service
sc start MyWS

# Stop a Windows Service
sc stop MyWS

# Delete a Windows Service
sc delete MyWS
```

