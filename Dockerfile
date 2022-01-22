FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "FpaABC.dll"]
EXPOSE 80
ENV DOTNET_EnableDiagnostics=0