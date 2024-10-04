# Stage 1: Base image for building, linting, and testing
FROM mcr.microsoft.com/dotnet/sdk:8.0 as base
WORKDIR /src

# Copy solution and project files
COPY *.sln ./
COPY ./Src/Api/*.csproj ./Src/Api/
COPY ./Src/Integration/*.csproj ./Src/Integration/
COPY ./Tests/Api.Tests/*.csproj ./Tests/Api.Tests/

# Restore dependencies
RUN dotnet restore

# Copy all source code
COPY . .

# Stage 2: Linting
FROM base as lint
RUN dotnet tool install -g dotnet-format
ENV PATH="$PATH:$HOME/.dotnet/tools"
RUN dotnet format whitespace --verify-no-changes --no-restore

# Stage 3: Testing
FROM base as test
RUN dotnet test --no-restore

# Stage 4: Build the application
FROM base as build
WORKDIR /src/Src/Api
RUN dotnet publish -c Release -o /src/Src/Api/out

# Stage 5: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as final
WORKDIR /app

# Expose port 8080, which is the port the application is listening on
EXPOSE 8080

# Copy the published output from the build stage
COPY --from=build /src/Src/Api/out .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "Api.dll"]
