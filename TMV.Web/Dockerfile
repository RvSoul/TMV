#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

COPY . .
EXPOSE 5000
EXPOSE 5395
ENTRYPOINT ["dotnet", "TMV.Web.dll"]