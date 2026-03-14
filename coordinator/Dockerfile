FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["coordinator.csproj", "."]
RUN dotnet restore "coordinator.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "coordinator.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "coordinator.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "coordinator.dll"]