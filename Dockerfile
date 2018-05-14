FROM node:carbon-alpine as build-ui
WORKDIR /app-ui

COPY ./ui ./
RUN npm install
RUN npm run build

FROM microsoft/aspnetcore-build:2.0 AS build-api
WORKDIR /app-api

COPY ./api ./
RUN dotnet restore
RUN dotnet publish Pulse.Web/Pulse.Web.csproj -c Release -o ../out


FROM microsoft/aspnetcore:2.0 AS runtime
WORKDIR /app
COPY --from=build-api /app-api/out .
COPY --from=build-ui /app-ui/dist ./wwwroot
ENTRYPOINT ["dotnet", "Pulse.Web.dll"]