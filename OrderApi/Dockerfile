# ================================
# Build Stage
# ================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["OrderApi.csproj", "./"]

RUN dotnet restore "OrderApi.csproj"

COPY . .

RUN dotnet publish "OrderApi.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false


# ================================
# Runtime Stage
# ================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "OrderApi.dll"]