FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build


WORKDIR /src

COPY ["BetCR.sln", "BetCR.sln"]
COPY ["BetCR.Web/BetCR.Web.csproj","BetCR.Web/" ]
COPY ["BetCR.Services/BetCR.Services.csproj","BetCR.Services/" ]
COPY ["BetCR.Repository/BetCR.Repository.csproj","BetCR.Repository/" ]
COPY ["BetCR.Library/BetCR.Library.csproj","BetCR.Library/" ]
COPY ["BetCR.Caching/BetCR.Caching.csproj","BetCR.Caching/" ]
COPY ["BetCR.Scheduled/BetCR.Scheduled.csproj","BetCR.Scheduled/" ]

RUN dotnet restore

COPY . .

WORKDIR /src
RUN dotnet build -c Release


FROM build AS publish
WORKDIR "/src/BetCR.Web"
RUN dotnet publish "BetCR.Web.csproj" --no-build --no-restore -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["dotnet", "BetCR.Web.dll"]

