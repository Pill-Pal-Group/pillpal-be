FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PillPal.WebApi/PillPal.WebApi.csproj", "src/PillPal.WebApi/"]
COPY ["src/PillPal.Application/PillPal.Application.csproj", "src/PillPal.Application/"]
COPY ["src/PillPal.Core/PillPal.Core.csproj", "src/PillPal.Core/"]
COPY ["src/PillPal.Infrastructure/PillPal.Infrastructure.csproj", "src/PillPal.Infrastructure/"]
RUN dotnet restore "./src/PillPal.WebApi/PillPal.WebApi.csproj"
COPY . .
WORKDIR "/src/src/PillPal.WebApi"
RUN dotnet build "./PillPal.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PillPal.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PillPal.WebApi.dll"]