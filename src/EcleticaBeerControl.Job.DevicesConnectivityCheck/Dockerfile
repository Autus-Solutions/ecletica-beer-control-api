#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EcleticaBeerControl.DevicesConnectivityCheck/EcleticaBeerControl.DevicesConnectivityCheck.csproj", "EcleticaBeerControl.DevicesConnectivityCheck/"]
RUN dotnet restore "EcleticaBeerControl.DevicesConnectivityCheck/EcleticaBeerControl.DevicesConnectivityCheck.csproj"
COPY . .
WORKDIR "/src/EcleticaBeerControl.DevicesConnectivityCheck"
RUN dotnet build "EcleticaBeerControl.DevicesConnectivityCheck.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcleticaBeerControl.DevicesConnectivityCheck.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcleticaBeerControl.DevicesConnectivityCheck.dll"]