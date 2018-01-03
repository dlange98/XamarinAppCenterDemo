# Services Reference Architecture Walkthrough

## Contents

Topic           | Page
--------------- | ----
Service Architecture  | [Service Architecture Doc](./Infrastructure.md)
Security        | [Security Doc](./ServiceSecurity.md)
Mobile          | [Mobile Architecture Doc](./MOBILE.md)
Services        | [Service Layer Doc](./Services.md)
Persistence        | [Service Persistance Doc](./ServiceDataLayer.md)
Monitoring        | [Service Monitoring Doc](./ServiceMonitoring.md)
DevOps           | [DevOps Doc](./DevOps.md)
## Service Architecture
[Please Reference the Architecture Sub Document](./Infrastructure.md)

## Security
[The Service Security description can be found here](./ServiceSecurity.md)  Mobile Client security will be described in the mobile client description.

## Mobile Client Architecture

The [Mobile Architecture Doc](./MOBILE.md) contains the documentation for the Mobile Clients.

## Services
The underlying service layer is implemented using Azure Functions, Entity Framework, and SQL Server. [The service layer is described here](./Services.md)
Service Health and Operation is monitored using Application Insights and 

### Data Layer / Persistance ###
SQL Server is used for the Persistance Layer, with Entity Framework managing the ORM.
### Service Monitoring ###
Azure 

## DevOps
