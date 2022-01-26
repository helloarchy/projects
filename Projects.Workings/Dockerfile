FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Projects.Workings/Projects.Workings.csproj", "Projects.Workings/"]
RUN dotnet restore "Projects.Workings/Projects.Workings.csproj"
COPY . .
WORKDIR "/src/Projects.Workings"
RUN dotnet build "Projects.Workings.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Projects.Workings.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Projects.Workings.dll"]
