FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY WappaMobile.sln ./
COPY Driver.BackgroundTasks/Driver.BackgroundTasks.csproj Driver.BackgroundTasks/
RUN dotnet restore Driver.BackgroundTasks/Driver.BackgroundTasks.csproj
COPY . .
WORKDIR /src/Driver.BackgroundTasks
RUN dotnet build Driver.BackgroundTasks.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Driver.BackgroundTasks.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WappaMobile.Driver.BackgroundTasks.dll"]
