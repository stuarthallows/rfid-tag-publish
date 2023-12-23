# RFID Tag Publish

- [Azure Static Web Apps documentation](https://learn.microsoft.com/en-us/azure/static-web-apps/)
- [Static Web Apps CLI](https://azure.github.io/static-web-apps-cli/)

## TODO
- Add security to Functions
- Add Application Insights
- Swap the CRA for a Vite app

## Run the frontend and API locally

### Install command line tools
```bash
npm install -D @azure/static-web-apps-cli
```

### Build frontend app
```bash
npm install
npm run build
```

### Start the CLI
Run the frontend app and API together by starting the app with the Static Web Apps CLI.
```bash
swa start build --api-location api
```

## Environment setup
1. Create Azure Static Web App
2. Add SignalR service
3. Add Application Insights
4. Add dependency to functions app - Microsoft.Azure.WebJobs.Extensions.SignalRService
5. Add SignalR connection string to functions app - func settings add AzureSignalRConnectionString "CONN_STR"
6. Add APPLICATIONINSIGHTS_CONNECTION_STRING setting to functions app







