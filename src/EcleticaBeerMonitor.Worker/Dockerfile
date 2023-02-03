#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EcleticaBeerMonitor.Worker/EcleticaBeerMonitor.Worker.csproj", "EcleticaBeerMonitor.Worker/"]
RUN dotnet restore "EcleticaBeerMonitor.Worker/EcleticaBeerMonitor.Worker.csproj"
COPY . .
WORKDIR "/src/EcleticaBeerMonitor.Worker"
RUN dotnet build "EcleticaBeerMonitor.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcleticaBeerMonitor.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcleticaBeerMonitor.Worker.dll"]