docker run --rm -v "%CD%:/local" openapitools/openapi-generator-cli generate ^
  -g aspnetcore ^
  -i /local/socomap.yaml ^
  -o /local/gen
REM Die Solution-Datei wird mit Fehlern generiert. Workaround ist, sie einfach zu löschen.
del gen\Org.OpenAPITools.sln
REM Falls gewünscht das generierte Projekt direkt öffnen. Dazu die folgende Zeile auskommentieren.
gen\src\Org.OpenAPITools\Org.OpenAPITools.csproj
