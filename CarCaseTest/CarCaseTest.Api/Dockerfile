#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CarCaseTest.Api/CarCaseTest.Api.csproj", "CarCaseTest.Api/"]
RUN dotnet restore "CarCaseTest.Api/CarCaseTest.Api.csproj"
COPY . .
WORKDIR "/src/CarCaseTest.Api"
RUN dotnet build "CarCaseTest.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarCaseTest.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarCaseTest.Api.dll"]