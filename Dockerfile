FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /
COPY ["CKO.Payments/CKO.Payments.csproj", "CKO.Payments/"]
COPY ["CKO.Payments.Bank/CKO.Payments.Bank.csproj", "CKO.Payments/"]
COPY ["CKO.Payments.Services/CKO.Payments.BL.csproj", "CKO.Payments/"]
COPY ["CKO.Payments.DAL/CKO.Payments.DAL.csproj", "CKO.Payments/"]
COPY ["CKO.Payments.Data/CKO.Payments.Data.csproj", "CKO.Payments/"]
RUN dotnet restore "CKO.Payments/CKO.Payments.csproj"
COPY . .
WORKDIR "/CKO.Payments"
RUN dotnet build "CKO.Payments.csproj" -c Release -o /build

FROM build AS publish
RUN dotnet publish "CKO.Payments.csproj" -c Release -o /publish

# docker pull mcr.microsoft.com/mssql/server:2019-latest
EXPOSE 1433/tcp

FROM base AS final
WORKDIR /
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "CKO.Payments.dll"]