FROM microsoft/aspnetcore-build:2.0-jessie AS build-env
WORKDIR /app

# Copy everything else and build
COPY . ./
RUN dotnet restore Wappa.sln
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/Wappa.WebApi/out .
ENTRYPOINT ["dotnet", "Wappa.WebApi.dll"]

# dockerize
# ENV DOCKERIZE_VERSION v0.6.0
# RUN apt-get update && apt-get install -y wget mysql-client
# RUN wget https://github.com/jwilder/dockerize/releases/download/$DOCKERIZE_VERSION/dockerize-linux-amd64-$DOCKERIZE_VERSION.tar.gz \
#     && tar -C /usr/local/bin -xzvf dockerize-linux-amd64-$DOCKERIZE_VERSION.tar.gz \
#     && rm dockerize-linux-amd64-$DOCKERIZE_VERSION.tar.gz