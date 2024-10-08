FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["booking-api/BookingRoom.API/BookingRoom.API.csproj", "booking-api/BookingRoom.API/"]
RUN dotnet restore "booking-api/BookingRoom.API/BookingRoom.API.csproj"


COPY . .
WORKDIR "/src/booking-api/BookingRoom.API"
RUN dotnet build "BookingRoom.API.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BookingRoom.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingRoom.API.dll"]