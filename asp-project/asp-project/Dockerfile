FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["asp-project/asp-project.csproj", "asp-project/"]
RUN dotnet restore "asp-project/asp-project.csproj"
COPY . .
WORKDIR "/src/asp-project"
RUN dotnet build "asp-project.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "asp-project.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "asp-project.dll"]
