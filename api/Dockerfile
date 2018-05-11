FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish Pulse.Web/Pulse.Web.csproj -c Release -o ../out

FROM microsoft/aspnetcore:2.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Pulse.Web.dll"]
