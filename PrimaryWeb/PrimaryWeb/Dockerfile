#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY ["PrimaryWeb/PrimaryWeb.csproj", "PrimaryWeb/"]
RUN dotnet restore "PrimaryWeb/PrimaryWeb.csproj"
COPY . .
WORKDIR "/src/PrimaryWeb"
RUN dotnet build "PrimaryWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PrimaryWeb.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PrimaryWeb.dll"]