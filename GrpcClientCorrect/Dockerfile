# Базовый образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Перенос файлов проекта
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["GrpcClientCorrect/GrpcClientCorrect.csproj", "GrpcClientCorrect/"]
RUN dotnet restore "GrpcClientCorrect/GrpcClientCorrect.csproj"
COPY . .
WORKDIR "/src/GrpcClientCorrect"
RUN dotnet build "GrpcClientCorrect.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GrpcClientCorrect.csproj" -c Release -o /app/publish

# Настройка и запуск приложения
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ./GrpcClientCorrect/appsettings.json .
ENTRYPOINT ["dotnet", "GrpcClientCorrect.dll"]