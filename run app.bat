SET projectPath=%cd%

nuget.exe restore

dotnet build

cd c:\Program Files (x86)\IIS Express\

start "" http://localhost:8080

iisexpress /path:"%projectPath%\Web" /port:8080