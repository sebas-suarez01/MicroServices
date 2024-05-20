﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["InventoryAPI/InventoryAPI.csproj", "InventoryAPI/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
RUN dotnet restore "InventoryAPI/InventoryAPI.csproj"
COPY . .
WORKDIR "/src/InventoryAPI"
RUN dotnet build "InventoryAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "InventoryAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryAPI.dll"]