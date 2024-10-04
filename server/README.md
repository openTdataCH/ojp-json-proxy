# 🚃 OJP-Proxy
This repository contains the backend application `OJP-Proxy` for `OJP-Explorer`. OJP-Proxy takes REST API calls and translates them into OJP XML format and sends them to the OJP Service.
The response gets translated from OJP XML to JSON. 

[OJP - Open Data Platform](https://opentransportdata.swiss/en/cookbook/open-journey-planner-ojp/)

## appsettings
The connection to the OJP-Service is configured in the `appsettings.json` file under the keys:
```json
  "OjpServiceUrlV1": "https://api.opentransportdata.swiss/ojp2020",
  "OjpServiceUrlV2": "https://api.opentransportdata.swiss/ojp20"
```

# Local Setup

Install Dependencies:
Run the following command to restore the necessary NuGet packages:
```bash
dotnet restore
```

Build the application:
```bash
dotnet build
```

Run the application locally:
You can run the application using the dotnet CLI:
```bash
dotnet run
```

Access the application:
The application will be available on http://localhost:5047



# Setup for production

Build the docker container:
```
docker build -t ojp-proxy:latest .
```

Run container
```
docker run --name ojp-proxy -d -p 8080:80 ojp-proxy:latest
```
Access the application on http://localhost:8080

---
> built by Tobias Sturzenegger and Cédric Wagner @ [FHNW](https://www.fhnw.ch/en/)