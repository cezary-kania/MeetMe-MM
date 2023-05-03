FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Bootstrapper/MeetMe.Bootstapper/MeetMe.Bootstapper.csproj", "src/Bootstrapper/MeetMe.Bootstapper/"]
COPY ["src/Shared/MeetMe.Shared.Abstractions/MeetMe.Shared.Abstractions.csproj", "src/Shared/MeetMe.Shared.Abstractions/"]
COPY ["src/Shared/MeetMe.Shared/MeetMe.Shared.csproj", "src/Shared/MeetMe.Shared/"]
COPY ["src/Modules/Users/MeetMe.Modules.Users.Shared/MeetMe.Modules.Users.Shared.csproj", "src/Modules/Users/MeetMe.Modules.Users.Shared/"]
COPY ["src/Modules/Users/MeetMe.Modules.Users.Infrastructure/MeetMe.Modules.Users.Infrastructure.csproj", "src/Modules/Users/MeetMe.Modules.Users.Infrastructure/"]
COPY ["src/Modules/Users/MeetMe.Modules.Users.Application/MeetMe.Modules.Users.Application.csproj", "src/Modules/Users/MeetMe.Modules.Users.Application/"]
COPY ["src/Modules/Users/MeetMe.Modules.Users.Domain/MeetMe.Modules.Users.Domain.csproj", "src/Modules/Users/MeetMe.Modules.Users.Domain/"]
COPY ["src/Modules/Profiles/MeetMe.Modules.Profiles.Shared/MeetMe.Modules.Profiles.Shared.csproj", "src/Modules/Profiles/MeetMe.Modules.Profiles.Shared/"]
COPY ["src/Modules/Profiles/MeetMe.Modules.Profiles.Core/MeetMe.Modules.Profiles.Core.csproj", "src/Modules/Profiles/MeetMe.Modules.Profiles.Core/"]
COPY ["src/Modules/MeetMe.Modules.Matching.Core/MeetMe.Modules.Matching.Core.csproj", "src/Modules/MeetMe.Modules.Matching.Core/"]
COPY ["src/Modules/MeetMe.Modules.Matching.Api/MeetMe.Modules.Matching.Api.csproj", "src/Modules/MeetMe.Modules.Matching.Api/"]
COPY ["src/Modules/Profiles/MeetMe.Modules.Profiles.Api/MeetMe.Modules.Profiles.Api.csproj", "src/Modules/Profiles/MeetMe.Modules.Profiles.Api/"]
COPY ["src/Modules/Users/MeetMe.Modules.Users.Api/MeetMe.Modules.Users.Api.csproj", "src/Modules/Users/MeetMe.Modules.Users.Api/"]
RUN dotnet restore "src/Bootstrapper/MeetMe.Bootstapper/MeetMe.Bootstapper.csproj"
COPY . .
WORKDIR "/src/src/Bootstrapper/MeetMe.Bootstapper"
RUN dotnet build "MeetMe.Bootstapper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeetMe.Bootstapper.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeetMe.Bootstapper.dll"]
