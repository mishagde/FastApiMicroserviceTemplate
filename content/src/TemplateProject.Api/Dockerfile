# ===============================================
# 1. Build stage
# ===============================================
# Build context must be repository root: docker build -f src/{{projectName}}.Api/Dockerfile .
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution + projects
COPY TemplateProject.sln ./
COPY src/TemplateProject.Api/TemplateProject.Api.csproj ./src/TemplateProject.Api/
COPY src/TemplateProject.Application/TemplateProject.Application.csproj ./src/TemplateProject.Application/
COPY src/TemplateProject.Infrastructure/TemplateProject.Infrastructure.csproj ./src/TemplateProject.Infrastructure/
COPY src/TemplateProject.Domain/TemplateProject.Domain.csproj ./src/TemplateProject.Domain/

# Restore dependencies
RUN dotnet restore TemplateProject.sln

# Copy the rest of the source code
COPY src ./src

# Build (no restore needed)
RUN dotnet build src/TemplateProject.Api/TemplateProject.Api.csproj -c Release -o /app/build

# Publish self-contained for faster startup (optional)
RUN dotnet publish src/TemplateProject.Api/TemplateProject.Api.csproj -c Release -o /app/publish


# ===============================================
# 2. Runtime stage
# ===============================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Create non-root user
RUN useradd -m appuser

WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:5000
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Expose port
EXPOSE 5000

# HEALTHCHECK (optional but recommended)
HEALTHCHECK --interval=10s --timeout=3s \
  CMD curl -f http://localhost:5000/health || exit 1

# Run as non-root
USER appuser

ENTRYPOINT ["dotnet", "TemplateProject.Api.dll"]
