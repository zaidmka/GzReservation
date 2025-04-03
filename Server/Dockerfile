# ---------- Build Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# انسخ فقط الملفات الخاصة بالمشروع داخل مجلد Server
COPY Server/*.csproj ./Server/
COPY Shared/*.csproj ./Shared/
COPY Client/*.csproj ./Client/

# انسخ كل المشروع
COPY . .

# انشر السيرفر
WORKDIR /app/Server
RUN dotnet publish GzReservation.Server.csproj -c Release -o /app/publish

# ---------- Runtime Stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "GzReservation.Server.dll"]
