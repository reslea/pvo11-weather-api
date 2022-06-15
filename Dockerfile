#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WeatherApi.csproj", "."]
RUN dotnet restore "./WeatherApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WeatherApi.csproj" -c Release -o /app/build

#FROM build AS test
#RUN dotnet test "WeatherApi.csproj"

FROM build AS publish
RUN dotnet publish "WeatherApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "WeatherApi.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet WeatherApi.dll