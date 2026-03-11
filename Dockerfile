FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app
COPY MassarAgency/*.csproj ./MassarAgency/
RUN dotnet restore ./MassarAgency/
COPY . ./
RUN dotnet publish ./MassarAgency/ -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .
ENV PORT=8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "MassarAgency.dll"]