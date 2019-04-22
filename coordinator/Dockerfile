FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["coordinator/coordinator.csproj", "coordinator/"]
RUN dotnet restore "coordinator/coordinator.csproj"
COPY . .
WORKDIR "/src/coordinator"
RUN dotnet build "coordinator.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "coordinator.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "coordinator.dll"]