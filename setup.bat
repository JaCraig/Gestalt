dotnet new tool-manifest
dotnet tool install Husky
dotnet tool install Versionize
dotnet tool update -g docfx
docfx init
dotnet husky install