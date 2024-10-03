# Используем образ .NET SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

# Рабочая директория для сборки
WORKDIR /src

# Копируем файл проекта и зависимостей в соответствующую директорию
COPY ./GrpcServer/GrpcServer.csproj ./GrpcServer/
RUN dotnet restore "./GrpcServer/GrpcServer.csproj"

# Копируем остальные файлы проекта
COPY ./GrpcServer/ ./GrpcServer/

# Строим проект
WORKDIR /src/GrpcServer
RUN dotnet build "GrpcServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этап публикации
FROM build AS publish
RUN dotnet publish "GrpcServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ для рабочего окружения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Копируем опубликованные выходные данные из предыдущего этапа
COPY --from=publish /app/publish .

# Копируем appsettings.json
COPY ./GrpcServer/appsettings.json .

# Команда для запуска приложения
ENTRYPOINT ["dotnet", "GrpcServer.dll"]