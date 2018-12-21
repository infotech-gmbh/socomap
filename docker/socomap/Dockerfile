FROM microsoft/dotnet:2.2-aspnetcore-runtime

COPY out/ /app/
WORKDIR /app
ENTRYPOINT [ "dotnet", "Socomap.dll"]

LABEL maintainer="Sebastian Kaldeweide <kaldeweide@infotech.de>"
LABEL vendor="infotech"
