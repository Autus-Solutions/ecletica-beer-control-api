#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EcleticaBeerControl.Api/EcleticaBeerControl.Api.csproj", "EcleticaBeerControl.Api/"]
COPY ["EcleticaBeerControl.Application/EcleticaBeerControl.Application.csproj", "EcleticaBeerControl.Application/"]
COPY ["EcleticaBeerControl.Domain/EcleticaBeerControl.Domain.csproj", "EcleticaBeerControl.Domain/"]
COPY ["EcleticaBeerControl.Infrastructure/EcleticaBeerControl.Infrastructure.csproj", "EcleticaBeerControl.Infrastructure/"]
COPY ["EcleticaBeerControl.Persistence.Supabase/EcleticaBeerControl.Persistence.Supabase.csproj", "EcleticaBeerControl.Persistence.Supabase/"]
RUN dotnet restore "EcleticaBeerControl.Api/EcleticaBeerControl.Api.csproj"
COPY . .
WORKDIR "/src/EcleticaBeerControl.Api"
RUN dotnet build "EcleticaBeerControl.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcleticaBeerControl.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcleticaBeerControl.Api.dll"]