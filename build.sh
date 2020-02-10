sudo systemctl stop kestrel-globe
dotnet publish --configuration Release -o /var/www/globe/ --self-contained true -r linux-x64 
sudo systemctl start kestrel-globe
