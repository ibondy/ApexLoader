# ApexLoader
Small service enabling download of Netune Apex data.
User RavenDB as data repository. 
Curently support download from 2 Neptune Apex devices and stores in a single database.

### Pre-requisites:
RavenDb - https://ravendb.net/docs/article-page/5.0/csharp/start/getting-started
Follow instructions to install RavenDb and create database named 'ApexClient'

### Service installation
Copy files to from https://github.com/ibondy/ApexLoader/releases/tag/1.0 
to the local folder. (i.e. c:\apexloader)
Modify settings.json with your Apex values

Open Windows Terminal as Administrator and run:  
**sc create "ApexLoader Service" binPath="<your folder>\apexloader.exe" start=delayed-auto DisplyName="Automatic downloader from Apex to RavenDb"**

**sc start "ApexLoader Service"**

### Clean up
Open Windows Terminal as Administrator and run:   

**sc stop "ApexLoader Service"**    

**sc delete "ApexLoader Service"**

Delete your local folder with app files.

 