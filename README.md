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
