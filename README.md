# 🚀 Fast API Microservice Template  
### FastEndpoints • MassTransit • PostgreSQL • RabbitMQ • Redis • Serilog • OpenTelemetry • Docker • Clean Architecture

Полноценный боевой шаблон микросервиса под **.NET 8/9**, построенный по принципам **Clean Architecture**, с поддержкой:

- 🚀 FastEndpoints (Minimal API + CQRS)
- 🐇 MassTransit + RabbitMQ (event-driven)
- 🐘 PostgreSQL + EF Core миграции
- 🔐 JWT (через внешний Auth Service)
- 🧩 Mapster (быстрый object mapping)
- 🧵 Redis (IDistributedCache)
- 🪵 Serilog + OpenSearch (логирование)
- 📡 OpenTelemetry (traces, metrics, logs)
- 🐳 Готовый Dockerfile (prod)
- 🧱 Готовый docker-compose (полное окружение)
- 🧪 Unit-тесты (Application layer)
- 🔧 Авто-применение EF миграций при старте

Это шаблон, который позволяет **создать реальный микросервис за < 2 секунды**:

```bash
dotnet new fast-api-microservice-template -n BillingService
```

# Как установить шаблон

### NuGet

Link: https://www.nuget.org/packages/MishaGde.FastApiMicroserviceTemplate/

# Как публиковать

## Windows локально + NuGet

Как собрать локально под windows:
1. Указать вручную версию в nuspec
2. Заменить "content" на "" в target
3. Заменить target=".template.config" на target=""
4. Использовать команду (чтобы гитигноры не протерять):
```bash
nuget pack FastApiMicroserviceTemplate.nuspec -NoDefaultExcludes
```

Публикация в nuget:

```bash
nuget push MishaGde.FastApiMicroserviceTemplate.1.0.0.nupkg -Source https://api.nuget.org/v3/index.json -ApiKey 
```

## GitLab

Создать ветку вида `release/*.*.*` - автоматически возьмёт версию из ветки.