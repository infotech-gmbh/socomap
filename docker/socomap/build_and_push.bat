@echo off
cd ../../src/dotnet/App
dotnet publish -c Release -o ../../../docker/socomap/out
cd ../../../docker/socomap

docker-compose build
::docker-compose push
