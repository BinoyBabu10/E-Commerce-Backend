FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["E-Commerce.csproj", "./"]
RUN dotnet restore "E-Commerce.csproj"

COPY . .
RUN dotnet publish "E-Commerce.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "E-Commerce.dll"]