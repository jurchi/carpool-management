# Pull .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Fetch and install Node
RUN curl --silent --location https://deb.nodesource.com/setup_19.x | bash -
RUN apt-get install --yes nodejs

WORKDIR /CarpoolManagement

#Copy Everything
COPY . ./
 
RUN dotnet restore

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet ef database update

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
EXPOSE 80
COPY --from=build-env /CarpoolManagement/CarpoolManagement.db .
COPY --from=build-env /CarpoolManagement/out .
ENTRYPOINT ["dotnet", "CarpoolManagement.dll"]