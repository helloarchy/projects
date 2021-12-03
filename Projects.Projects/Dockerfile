FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Projects.Projects/Projects.Projects.csproj", "Projects.Projects/"]
RUN dotnet restore "Projects.Projects/Projects.Projects.csproj"
COPY . .
WORKDIR "/src/Projects.Projects"
RUN dotnet build "Projects.Projects.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Projects.Projects.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Projects.Projects.dll"]
