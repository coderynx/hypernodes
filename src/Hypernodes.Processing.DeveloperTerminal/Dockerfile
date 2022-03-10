FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MediaBound.DeveloperTerminal/MediaBound.DeveloperTerminal.csproj", "MediaBound.DeveloperTerminal/"]
RUN dotnet restore "MediaBound.DeveloperTerminal/MediaBound.DeveloperTerminal.csproj"
COPY . .
WORKDIR "/src/MediaBound.DeveloperTerminal"
RUN dotnet build "MediaBound.DeveloperTerminal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MediaBound.DeveloperTerminal.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediaBound.DeveloperTerminal.dll"]
