#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:lts AS spa-build
WORKDIR /src
COPY . .
WORKDIR "/src/Todo.UI"
RUN rm -f .env
RUN yarn install
RUN yarn build

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Todo.Api/Todo.Api.csproj", "Todo.Api/"]
COPY ["Todo.Domain/Todo.Domain.csproj", "Todo.Domain/"]
COPY ["Todo.Infra/Todo.Infra.csproj", "Todo.Infra/"]
RUN dotnet restore "Todo.Api/Todo.Api.csproj"
COPY . .
WORKDIR "/src/Todo.Api"
RUN dotnet build "Todo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Todo.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=spa-build "/src/Todo.UI/dist" /wwwroot
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todo.Api.dll"]