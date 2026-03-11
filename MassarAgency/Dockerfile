# مرحلة البناء
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# نسخ ملف المشروع وعمل Restore
COPY *.csproj .
RUN dotnet restore

# نسخ باقي الملفات وبناء المشروع
COPY . .
RUN dotnet publish -c Release -o out

# مرحلة التشغيل
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .

# إعداد Port للـ Render
ENV PORT 10000
EXPOSE 10000

# تشغيل المشروع
ENTRYPOINT ["dotnet", "MassarAgency.dll"]