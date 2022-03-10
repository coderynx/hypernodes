FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MediaBound/MediaBound.csproj", "MediaBound/"]
RUN dotnet restore "MediaBound/MediaBound.csproj"
COPY . .
WORKDIR "/src/MediaBound"
RUN dotnet build "MediaBound.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MediaBound.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediaBound.dll"]
