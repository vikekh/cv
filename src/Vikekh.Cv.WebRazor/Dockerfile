#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Vikekh.Cv.WebRazor/Vikekh.Cv.WebRazor.csproj", "src/Vikekh.Cv.WebRazor/"]
COPY ["src/Vikekh.JsonResume/Vikekh.JsonResume.csproj", "src/Vikekh.JsonResume/"]
RUN dotnet restore "src/Vikekh.Cv.WebRazor/Vikekh.Cv.WebRazor.csproj"
COPY . .
WORKDIR "/src/src/Vikekh.Cv.WebRazor"
RUN dotnet build "Vikekh.Cv.WebRazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vikekh.Cv.WebRazor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vikekh.Cv.WebRazor.dll"]