# ApexLoader
Small service enabling download of Netune Apex data.
Uses RavenDB as a data repository. 
Curently supports download from up to 2 Neptune Apex devices and stores downloaded data in a single database.

### Pre-requisites:
RavenDb - https://ravendb.net/docs/article-page/5.0/csharp/start/getting-started
Follow instructions to install RavenDb and create database named 'ApexClient'
RavenDb is expected at the default "http://127.0.0.1:8080" location.

### Service installation
Copy files to from https://github.com/ibondy/ApexLoader/releases/tag/1.0 
to the local folder. (i.e. c:\apexloader)
Modify settings.json with your Apex values
- URL - Local URL of your Neptune Apex
- User - User name used to login into the local Apex
- Password - Password used to login into local Apex

If you have a second Apex, you can configure it in the Apex2 section in the config file and change   
- Active to "True"

Default download interval from Apex is 1 minute. If you like to extend this interval change config file: 
- DownloadInterval from 1 to x, where x is the number of minutes to wait between downloads

Open Windows Terminal as Administrator and run:  
**sc create "ApexLoader Service" binPath="<your folder>\apexloader.exe" start=auto DisplayName="Automatic downloader from Apex to RavenDb"**

**sc start "ApexLoader Service"**

### Clean up
Open Windows Terminal as Administrator and run:   

**sc stop "ApexLoader Service"**    

**sc delete "ApexLoader Service"**

Delete your local folder with app files.

 
