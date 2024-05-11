#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/SHJ.Commerce.AuthServer.Web.MVC/SHJ.Commerce.AuthServer.Web.MVC.csproj", "src/SHJ.Commerce.AuthServer.Web.MVC/"]
COPY ["src/SHJ.Commerce.Application/SHJ.Commerce.Application.csproj", "src/SHJ.Commerce.Application/"]
COPY ["src/SHJ.Commerce.ApplicationContracts/SHJ.Commerce.ApplicationContracts.csproj", "src/SHJ.Commerce.ApplicationContracts/"]
COPY ["src/SHJ.Commerce.Infrastructure/SHJ.Commerce.Infrastructure.csproj", "src/SHJ.Commerce.Infrastructure/"]
COPY ["src/SHJ.Commerce.Domain/SHJ.Commerce.Domain.csproj", "src/SHJ.Commerce.Domain/"]
COPY ["src/SHJ.Commerce.Domain.Shared/SHJ.Commerce.Domain.Shared.csproj", "src/SHJ.Commerce.Domain.Shared/"]
COPY ["src/SHJ.Commerce.Shared/SHJ.Commerce.Shared.csproj", "src/SHJ.Commerce.Shared/"]
RUN dotnet restore "./src/SHJ.Commerce.AuthServer.Web.MVC/./SHJ.Commerce.AuthServer.Web.MVC.csproj"
COPY . .
WORKDIR "/src/src/SHJ.Commerce.AuthServer.Web.MVC"
RUN dotnet build "./SHJ.Commerce.AuthServer.Web.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SHJ.Commerce.AuthServer.Web.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SHJ.Commerce.AuthServer.Web.MVC.dll"]