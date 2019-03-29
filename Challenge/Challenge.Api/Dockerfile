FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY Challenge.Api/Challenge.Api.csproj Challenge.Api/
COPY Challenge.Domain/Challenge.Domain.csproj Challenge.Domain/
RUN dotnet restore Challenge.Api/Challenge.Api.csproj
COPY . .
WORKDIR /src/Challenge.Api
RUN dotnet build Challenge.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Challenge.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Challenge.Api.dll"]
