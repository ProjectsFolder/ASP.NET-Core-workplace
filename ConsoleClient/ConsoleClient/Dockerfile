#docker run -it --rm --network webtest_backend webtest_console /bin/sh -c "./ConsoleClient"
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ConsoleClient/ConsoleClient.csproj", "ConsoleClient/"]
RUN dotnet restore "./ConsoleClient/./ConsoleClient.csproj"

COPY . .
WORKDIR "/src/ConsoleClient"
RUN dotnet publish "./ConsoleClient.csproj" -c $BUILD_CONFIGURATION -o /src/publish -r linux-x64 --self-contained /p:UseAppHost=true

FROM base AS final
WORKDIR /app
COPY --from=build /src/publish .
