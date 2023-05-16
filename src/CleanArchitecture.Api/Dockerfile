FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "src/"]
COPY ["Directory.Packages.props", "src/"]
COPY ["src/CleanArchitecture.Api/CleanArchitecture.Api.csproj", "src/CleanArchitecture.Api/"]
COPY ["src/CleanArchitecture.Infrastructure/CleanArchitecture.Infrastructure.csproj", "src/CleanArchitecture.Infrastructure/"]
COPY ["src/CleanArchitecture.Application/CleanArchitecture.Application.csproj", "src/CleanArchitecture.Application/"]
COPY ["src/CleanArchitecture.Domain/CleanArchitecture.Domain.csproj", "src/CleanArchitecture.Domain/"]
RUN dotnet restore "src/CleanArchitecture.Api/CleanArchitecture.Api.csproj"
COPY . .
RUN dotnet build "src/CleanArchitecture.Api/CleanArchitecture.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/CleanArchitecture.Api/CleanArchitecture.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitecture.Api.dll"]