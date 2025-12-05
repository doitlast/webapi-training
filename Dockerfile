# Use the official .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env


# Set the working directory inside the container
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out  

# Use the official .NET runtime image as the base image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
ENV PATH=$PATH:/usr/share/dotnet
# Set the working directory
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build-env /app/out ./

# Expose port 80 for HTTP
EXPOSE 80

#RUN chmod a+x ProductApi.dll
# Specify the entry point of the application
ENTRYPOINT ["dotnet", "ProductApi.dll"] # Replace ProductApi with your project's name