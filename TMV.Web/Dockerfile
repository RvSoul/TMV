#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TMV.Web/TMV.Web.csproj", "TMV.Web/"]
COPY ["TMV.Web.Core/TMV.Web.Core.csproj", "TMV.Web.Core/"]
COPY ["TMV.Application/TMV.Application.csproj", "TMV.Application/"]
COPY ["TMV.Core/TMV.Core.csproj", "TMV.Core/"]
COPY ["TMV.DTO/TMV.DTO.csproj", "TMV.DTO/"]
RUN dotnet restore "TMV.Web/TMV.Web.csproj"
COPY . .
WORKDIR "/src/TMV.Web"
RUN dotnet build "TMV.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TMV.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TMV.Web.dll"]