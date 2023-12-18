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



