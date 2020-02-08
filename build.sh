
dotnet build --configuration Release

sudo systemctl stop kestrel-globe
dotnet publish --configuration Release -o /var/www/globe/
sudo systemctl start kestrel-globe
