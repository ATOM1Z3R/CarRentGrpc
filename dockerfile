FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY src/GrpcInterface/GrpcInterface.csproj GrpcInterface/

RUN dotnet restore "GrpcInterface/GrpcInterface.csproj"
COPY src/ .
RUN dotnet publish "GrpcInterface/GrpcInterface.csproj" -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=build /publish .

EXPOSE 5000

ENTRYPOINT [ "dotnet", "GrpcInterface.dll" ]