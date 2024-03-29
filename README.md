# RFID Tag Publish

- [Azure Static Web Apps documentation](https://learn.microsoft.com/en-us/azure/static-web-apps/)
- [Static Web Apps CLI](https://azure.github.io/static-web-apps-cli/)

## Run the frontend and API locally

### Install command line tools
```Shell
npm install -D @azure/static-web-apps-cli
```

### Build frontend app

```Shell
npm install
npm vite build
```

### Start the CLI
Run the frontend app and API together by starting the app with the Static Web Apps CLI.
```Shell
swa start build --api-location api
```

## Environment setup
1. Create Azure Static Web App
2. Add SignalR service
3. Add Application Insights
4. Add dependency to functions app - Microsoft.Azure.WebJobs.Extensions.SignalRService
5. Add SignalR connection string to functions app - func settings add AzureSignalRConnectionString "CONN_STR"
6. Add APPLICATIONINSIGHTS_CONNECTION_STRING setting to functions app
7. Add XLinkToAuth environment variable in Azure to secure the API

For development add file `api/local.settings.json`, not committed to source control.

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "AzureSignalRConnectionString": "<Connection string from Azure SignalR service>"
  },
  "ConnectionStrings": {}
}
```

## Azure
- React app: https://witty-beach-0c176770f.4.azurestaticapps.net/
- Hub hostname: rfid-tag-publish.service.signalr.net
- Post tags to: https://witty-beach-0c176770f.4.azurestaticapps.net/api/tag-scanned
    ```json
    {
        "TagId": "E2801190200072BB20330632"
    }
    ```
