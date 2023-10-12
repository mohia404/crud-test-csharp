#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/WebApps/Mc2.CrudTest.Presentation/Server/Mc2.CrudTest.Presentation.Server.csproj", "src/WebApps/Mc2.CrudTest.Presentation/Server/"]
COPY ["src/WebApps/Mc2.CrudTest.Presentation/Client/Mc2.CrudTest.Presentation.Client.csproj", "src/WebApps/Mc2.CrudTest.Presentation/Client/"]
COPY ["src/Services/CustomerManager/CustomerManager.Contracts/CustomerManager.Contracts.csproj", "src/Services/CustomerManager/CustomerManager.Contracts/"]
COPY ["src/WebApps/Mc2.CrudTest.Presentation/Shared/Mc2.CrudTest.Presentation.Shared.csproj", "src/WebApps/Mc2.CrudTest.Presentation/Shared/"]
RUN dotnet restore "src/WebApps/Mc2.CrudTest.Presentation/Server/Mc2.CrudTest.Presentation.Server.csproj"
COPY . .
WORKDIR "/src/src/WebApps/Mc2.CrudTest.Presentation/Server"
RUN dotnet build "Mc2.CrudTest.Presentation.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mc2.CrudTest.Presentation.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mc2.CrudTest.Presentation.Server.dll"]