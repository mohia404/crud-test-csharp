#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/CustomerManager/CustomerManager.API/CustomerManager.API.csproj", "src/Services/CustomerManager/CustomerManager.API/"]
COPY ["src/Services/CustomerManager/CustomerManager.Infrastructure/CustomerManager.Infrastructure.csproj", "src/Services/CustomerManager/CustomerManager.Infrastructure/"]
COPY ["src/Services/CustomerManager/CustomerManager.Application/CustomerManager.Application.csproj", "src/Services/CustomerManager/CustomerManager.Application/"]
COPY ["src/Services/CustomerManager/CustomerManager.Contracts/CustomerManager.Contracts.csproj", "src/Services/CustomerManager/CustomerManager.Contracts/"]
COPY ["src/Services/CustomerManager/CustomerManager.Domain/CustomerManager.Domain.csproj", "src/Services/CustomerManager/CustomerManager.Domain/"]
RUN dotnet restore "src/Services/CustomerManager/CustomerManager.API/CustomerManager.API.csproj"
COPY . .
WORKDIR "/src/src/Services/CustomerManager/CustomerManager.API"
RUN dotnet build "CustomerManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerManager.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerManager.API.dll"]