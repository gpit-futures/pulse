FROM node:carbon-alpine as build-ui
WORKDIR /app-ui

COPY ./ui/package.json .
RUN npm install

COPY ./ui/ .
RUN npm run build

FROM microsoft/dotnet:2.1-sdk-alpine AS build-api
WORKDIR /app-api

COPY ./api ./
RUN dotnet restore
RUN dotnet publish Pulse.Web/Pulse.Web.csproj -c Release -o ../out


FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine AS runtime
WORKDIR /app
COPY --from=build-api /app-api/out .
COPY --from=build-ui /app-ui/dist ./wwwroot
ENTRYPOINT ["dotnet", "Pulse.Web.dll"]