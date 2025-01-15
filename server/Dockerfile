# Use the official .NET SDK as the base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory
WORKDIR /app

# Copy the .csproj and restore the dependencies
COPY ["ProductApi/ProductApi.csproj", "ProductApi/"]
RUN dotnet restore "ProductApi/ProductApi.csproj"

# Copy the rest of the application files and build the app
COPY . .
RUN dotnet publish "ProductApi/ProductApi.csproj" -c Release -o /app/publish

# Use the official .NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

# Set the working directory for the final image
WORKDIR /app

# Copy the published files from the build stage
COPY --from=build /app/publish .

# Expose the port your app runs on
EXPOSE 5118

# Start the app
ENTRYPOINT ["dotnet", "ProductApi.dll"]
