FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiGateway/Projects.Gateway.csproj", "ApiGateway/"]
RUN dotnet restore "Projects.Gateway.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Projects.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Projects.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Projects.Gateway.dll"]
