FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY ["Wappa.API/Wappa.API.csproj", "Wappa.API/"]
RUN dotnet restore "Wappa.API/Wappa.API.csproj"
COPY . .
WORKDIR "/src/Wappa.API"
RUN dotnet build "Wappa.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Wappa.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Wappa.API.dll"]