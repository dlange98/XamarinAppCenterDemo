## Application and service monitoring
Every application requires logging, monitoring, telemetry and analytice.  The extent depends on the complexity and criticality of the operations suported.

### Application Insights
For Microsft Azure, Applications Insights is the recommended service and application monitoring platform.  There are multiple strategies for entering data into App Insights.  Azure Functions has some built-in monitoring that lights up when an instrumentation key is supplied in the parameters console.

We also used some special eventing code within the assemblies to record business process events, and hooked into Entity Framework to log standardized events.

Example of monitoring view

![App Insights Dashboard](https://abaf81c44da6f407f97a8bac.blob.core.windows.net/screenshots/SC_Monitoring_01.PNG)


### Microsoft Operations Management Suite (OMS)

Operations Management Suite (also known as OMS) is a collection of management services that were designed for use in Azure. In this reference architecture we are utilizing it's Log Analytics functionality.  Howerever it can also provide roles in automation, backup, and recovery operations.

In this simplistic setup OMS doesn't add much value.  In much larger installations it would provide insight across multiple service endpoints, data repository performance, and provisional network infrastructure.  This would allow for ensuring system health and ensuring SLA levels.