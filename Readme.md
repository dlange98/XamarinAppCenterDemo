# Reference Architecture Walkthrough

## Contents

Topic                | Page
-------------------- | ------------------------------------------------
Service Architecture | [Service Architecture Doc](./Infrastructure.md)
Security             | [Security Doc](./ServiceSecurity.md)
Services             | [Service Layer Doc](./Services.md)
Persistence          | [Service Persistance Doc](./ServiceDataLayer.md)
Monitoring           | [Service Monitoring Doc](./ServiceMonitoring.md)
DevOps               | [DevOps Doc](./DevOps.md)
Mobile Client Arch   | [Mobile Architecture Doc](./MOBILE.md)
Mobile DevOps        | [Mobile Dev Ops](./MOBILE_DEV_OPS.md)

## Service Architecture

[Please Reference the Architecture Sub Document](./Infrastructure.md)

## Security

The Service Security description can be found here: [[Security Doc](./ServiceSecurity.md)]. Mobile Client security will be described in the mobile client description.

## Services

The underlying service layer is implemented using Azure Functions, Entity Framework, and SQL Server. The service layer is described here: [[Service Layer Doc](./Services.md)]. Service Health and Operation is monitored using Application Insights and

### Data Layer / Persistance
SQL Server is used for the Persistance Layer, with Entity Framework managing the ORM.
### Service Monitoring
Azure Application Insights
## DevOps

## Mobile Client Architecture

The [Mobile Architecture Doc](./MOBILE.md) contains the architecture documentation for the Mobile Clients.

## Mobile DevOps

The [Mobile Dev Ops Doc](./MOBILE_DEV_OPS.md) contains the documentation for testing (unit and UI), Continuous Integration, distribution, Crash Detection, and Analytics for the iOS and Android apps.
