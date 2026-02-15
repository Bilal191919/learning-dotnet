FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "Stage-4.csproj"

RUN dotnet build "Stage-4.csproj" -c Release -o /app/build -f net8.0

FROM build AS publish
RUN dotnet publish "Stage-4.csproj" -c Release -o /app/publish /p:UseAppHost=false -f net8.0

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stage-4.dll"]