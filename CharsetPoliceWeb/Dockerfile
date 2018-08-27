FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 11693
EXPOSE 44330

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY CharsetPoliceWeb/CharsetPoliceWeb.csproj CharsetPoliceWeb/
COPY CharsetPolice/CharsetPolice.csproj CharsetPolice/
RUN dotnet restore CharsetPoliceWeb/CharsetPoliceWeb.csproj
COPY . .
WORKDIR /src/CharsetPoliceWeb
RUN dotnet build CharsetPoliceWeb.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish CharsetPoliceWeb.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CharsetPoliceWeb.dll"]
