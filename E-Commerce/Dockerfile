# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file
COPY ["E-Commerce.csproj", "./"]

# Restore dependencies
RUN dotnet restore "E-Commerce.csproj"

# Copy everything
COPY . .

# Publish app
RUN dotnet publish "E-Commerce.csproj" -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Run app
ENTRYPOINT ["dotnet", "E-Commerce.dll"]