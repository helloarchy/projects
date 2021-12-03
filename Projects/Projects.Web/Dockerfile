FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["Projects.Web/Projects.Web.csproj", "Projects.Web/"]
RUN dotnet restore "Projects.Web/Projects.Web.csproj"
COPY . .
WORKDIR "/src/Projects.Web"
RUN dotnet build "Projects.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Projects.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Projects.Web.dll"]
