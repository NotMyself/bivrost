FROM microsoft/dotnet:2.2-sdk-alpine AS buildcore
WORKDIR /app

RUN apk --no-cache add nodejs nodejs-npm

COPY . .

# build Server
RUN dotnet restore ./src/server
RUN dotnet build -c Release ./src/server
RUN dotnet publish -c Release -o /app/deploy/server ./src/server

#build Client
WORKDIR /app/src/client
RUN npm install
RUN npm run-script build

#build Image
FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine AS runtime
WORKDIR /app


COPY --from=buildcore /app/deploy/server ./server
COPY --from=buildcore /app/src/client/dist ./client

ENV BIVROST_TWITCH_BOT_USER_NAME enter-your-username
ENV BIVROST_TWITCH_BOT_ACCESS_TOKEN enter-your-access-token
ENV BIVROST_TWITCH_BOT_CHANNEL enter-your-channel

EXPOSE 80

WORKDIR /app/server

ENTRYPOINT [ "dotnet", "server.dll" ]
