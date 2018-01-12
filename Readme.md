# Reference Architecture Walkthrough

## Contents

Topic                | Page
-------------------- | ------------------------------------------------
Executive Sumary     | [This Document](./ReadMe.md)
Mobile Client Arch   | [Mobile Architecture Doc](./MOBILE.md)
Mobile DevOps        | [Mobile Dev Ops](./MOBILE_DEV_OPS.md)
Service Architecture | [Service Architecture Doc](./Infrastructure.md)
Security             | [Security Doc](./ServiceSecurity.md)
Services             | [Service Layer Doc](./Services.md)
Persistence          | [Service Persistance Doc](./ServiceDataLayer.md)
Monitoring           | [Service Monitoring Doc](./ServiceMonitoring.md)
DevOps               | [DevOps Doc](./DevOps.md)


## Summary ##
This Reference Application is intended to define a recomended best practice approach to deploying a mobile application.  This document will define the necessary elements for a functional application and the reasoning on why it is the best practice along with guidance on when a different approach should be considered.

The guidance assumes experience  with Microsoft Azure and Visual Studio Development.  Details specific to the projects or frameworks chosen will be provided as part of the guidance.  The project can be considered as two parts.  The client and the service.  While both are included in the project, seperate build and deployment paths are appropriate for each.  The following documents will describe both elements and how to create and deploy them.

Also provided are scripts, example code, and other resources to use this guidance to bootstrap new mobile application development.

## Client Description
### Mobile Client Architecture

The [Mobile Architecture Doc](./MOBILE.md) contains the architecture documentation for the Mobile Clients.

### Mobile DevOps

The [Mobile Dev Ops Doc](./MOBILE_DEV_OPS.md) contains the documentation for testing (unit and UI), Continuous Integration, distribution, Crash Detection, and Analytics for the iOS and Android apps.


## Service Description

[Please Reference the Architecture Sub Document](./Infrastructure.md)

### Security

The Service Security description can be found here: [[Security Doc](./ServiceSecurity.md)]. Mobile Client security will be described in the mobile client description.

### Services

The underlying service layer is implemented using Azure Functions, Entity Framework, and SQL Server. The service layer is described here: [[Service Layer Doc](./Services.md)]. Service Health and Operation is monitored using Application Insights and

### Data Layer / Persistance
SQL Server is used for the Persistance Layer, with Entity Framework managing the ORM.
### Service Monitoring
Azure Application Insights and Microsoft OperationsMonitor are used to monitor application health.  These are describe in the [Monitoring section](./ServiceMonitoring.md)
### DevOps
The [DevOps elements (link)](./DevOps.md) describe how the services and resources are deployed and updated in an automated fashion.
