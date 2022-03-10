FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["M9.Web.Api/M9.Web.Api.csproj", "M9.Web.Api/"]
RUN dotnet restore "M9.Web.Api/M9.Web.Api.csproj"
COPY . .
WORKDIR "/src/M9.Web.Api"
RUN dotnet build "M9.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "M9.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "M9.Web.Api.dll"]
