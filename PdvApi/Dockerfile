FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 9090
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PdvApi/PdvApi.csproj", "PdvApi/"]
RUN dotnet restore "PdvApi/PdvApi.csproj"
COPY . .
WORKDIR "/src/PdvApi"
RUN dotnet build "PdvApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PdvApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PdvApi.dll"]