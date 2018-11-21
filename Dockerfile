FROM microsoft/dotnet:2.1-sdk-alpine AS build-env
WORKDIR /app
COPY ./ ./
RUN cd Domain.Test && \
    dotnet test
RUN dotnet publish -c Release -o out
RUN cd ChallengeDev && \
    rm -f ChallengeDev.db && \
    dotnet ef migrations add InitialCreate && \
    dotnet ef database update && \
    cp ChallengeDev.db out/ChallengeDev.db

FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine
WORKDIR /app
ENV GEOCODING_API_KEY ""
COPY --from=build-env /app/ChallengeDev/out .
CMD dotnet ChallengeDev.dll