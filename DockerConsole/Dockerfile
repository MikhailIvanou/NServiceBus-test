FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build

# Change current working directory
WORKDIR /app

# Copy existing files from host machine
COPY . ./

# Publish application to the "out" folder
RUN dotnet publish --configuration Debug --output out

# Start container by running application DLL
ENTRYPOINT ["dotnet", "out/DockerConsole.dll"]