FROM microsoft/dotnet:latest

COPY ./src /app

WORKDIR /app

RUN ["dotnet", "restore"]

RUN ["dotnet", "build", "Sandbox.Server.Http"]

EXPOSE 5000/tcp

WORKDIR /app/Sandbox.Server.Http

ENTRYPOINT ["dotnet", "run", "--server.urls", "http://0.0.0.0:5000"]
