# Etapa 1: Construcción de la aplicación en modo Debug
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo de proyecto y restaurar dependencias
COPY ["API/ProductManagementAPI.csproj", "API/"]
COPY . .
WORKDIR /src/API
RUN dotnet restore "ProductManagementAPI.csproj"

# Compilar y publicar en modo Debug
RUN dotnet publish "ProductManagementAPI.csproj" -c Debug -o /app/publish

# Etapa 2: Imagen runtime para desarrollo
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Establecer la variable de entorno a Development
ENV ASPNETCORE_ENVIRONMENT=Development

# Exponer el puerto de la aplicación
EXPOSE 80

ENTRYPOINT ["dotnet", "ProductManagementAPI.dll"]
