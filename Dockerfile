FROM mcr.microsoft.com/dotnet/sdk:9.0 AS sdk-build
WORKDIR /src
COPY ["SubscriptionHub/SubscriptionHub.Api.csproj", "SubscriptionHub/"]
COPY ["SubscriptionHub.Application/SubscriptionHub.Application.csproj", "SubscriptionHub.Application/"]
COPY ["SubscriptionHub.Domain/SubscriptionHub.Domain.csproj", "SubscriptionHub.Domain/"]
COPY ["SubscriptionHub.Infrastructure/SubscriptionHub.Infrastructure.csproj", "SubscriptionHub.Infrastructure/"]
RUN dotnet restore "SubscriptionHub/SubscriptionHub.Api.csproj"
COPY . .
RUN dotnet publish "SubscriptionHub/SubscriptionHub.Api.csproj" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=sdk-build /app/publish .
ENTRYPOINT ["dotnet", "SubscriptionHub.Api.dll"]