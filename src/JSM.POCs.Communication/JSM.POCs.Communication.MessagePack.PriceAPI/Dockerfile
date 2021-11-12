#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["JSM.POCs.Communication.MessagePack.PriceAPI/JSM.POCs.Communication.MessagePack.PriceAPI.csproj", "JSM.POCs.Communication.MessagePack.PriceAPI/"]
RUN dotnet restore "JSM.POCs.Communication.MessagePack.PriceAPI/JSM.POCs.Communication.MessagePack.PriceAPI.csproj"
COPY . .
WORKDIR "/src/JSM.POCs.Communication.MessagePack.PriceAPI"
RUN dotnet build "JSM.POCs.Communication.MessagePack.PriceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JSM.POCs.Communication.MessagePack.PriceAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JSM.POCs.Communication.MessagePack.PriceAPI.dll"]