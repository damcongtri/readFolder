FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ReadFolder/ReadFolder.csproj", "ReadFolder/"]
RUN dotnet restore "ReadFolder/ReadFolder.csproj"
COPY . .
WORKDIR "/src/ReadFolder"
RUN dotnet build "ReadFolder.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReadFolder.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReadFolder.dll"]
