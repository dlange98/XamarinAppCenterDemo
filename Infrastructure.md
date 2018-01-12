
## Infrastructure Listing

## Service Architecture Guide
The Service API in the _RefApp_ solution consists of two projects:

- `KindredPOC.API` - This project is a Function project that contains the items service endpoint.  This p
- `KindredPOC.API.TESTS` - The Unit test for the code

The API uses the following Nuget Packages

Nuget Package                                       | Version
--------------------------------------------------- | -------
EntityFramework                                     | 6.2.0    
Microsoft.ApplicationInsights                       | 2.4.0    
Microsoft.Azure.NotificationHubs                    | 1.0.9
Microsoft.IdentityModel.Clients.ActiveDirectory     | 3.17.3    
Microsoft.NET.Sdk.Functions                         | 1.0.7    
Newtonsoft.Json                                     | 10.0.3



#### Creating the Projects ####


The functions solution type is available with File > New Project from the Visual Studio 2017 menus if you have the [Azure Functions and Web Jobs Tools](https://marketplace.visualstudio.com/items?itemName=VisualStudioWebandAzureTools.AzureFunctionsandWebJobsTools)installed from Microsoft. The unit test project is a standard c# MSTest unit test project with the same references as the API project.
The solutions were built using .Net Framework 4.7.

#### Persistance ####


Local persistance is managed through a combination of Entity Framework and Sql Server, as discussed here: [Service Persistance Document](./ServiceDataLayer.md)

#### Security ####

Security was managed at the host level.  No service level Authentication or Authorization was used, with AuthorizationLevel.Anonymous specified in the  Function callinge signature arguments.  However, if you wished to implement a more fine grain authorization scheme you could employ other AuthorizationLevel attributes.  Further information is described here: [Azure Functions HTTP – Authorization Levels](https://vincentlauzon.com/2017/12/04/azure-functions-http-authorization-levels/) and here [Azure Functions HTTP and webhook bindings](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-http-webhook)

##### Push Notifications #####
Push notifications are enabled server side by calling out to the Notification Portal from the Microsoft.Azure.NotificationHubs Library.  This library has a client which connects to the hub via a "service-bus like" connection.  In our reference we inform the clients anytime an item is added to the repository.  

For use cases which involve distributing notifications to individuals or groups a tag system will need to be implemented wich registers and persists tagging schemes for devices.  Examples of tgging schemes are: 
- by individual
- By device
- by group membership
- by geography
- by interest.

These tags would need to be persisted, managed, and recalled based on specific use cases.

##### Overall Project Data Structure #####
![Logical Diagram of Reference Application](https://abaf81c44da6f407f97a8bac.blob.core.windows.net/screenshots/SC_Diagram_01.png) 

##### Azure Assets #####

Listed below are the assets created in azure for this reference application.  Note that Operations Manager is not currently scriptable through ARM templates


Name                                        | ResourceType                                            | Kind              
------------------------------------------- |-------------------------------------------------------  |------------------ 
ReferenceApplicationAppInsights             | microsoft.insights/components                           | other             
ReferenceAppNameSpace                       | Microsoft.NotificationHubs/namespaces                   | NotificationHub   
ReferenceAppNameSpace/ReferenceAppHub       | Microsoft.NotificationHubs/namespaces/notificationHubs  |                   
ReferenceAppAnalytics                       | Microsoft.OperationalInsights/workspaces                |                   
ApplicationInsights(ReferenceAppAnalytics)  | Microsoft.OperationsManagement/solutions                |                   
referenceappsvr                             | Microsoft.Sql/servers                                   | v12.0
referenceappsvr/ReferenceAppDb              | Microsoft.Sql/servers/databases	                      | v12.0,user
abaf81c44da6f407f97a8bac                    | Microsoft.Storage/storageAccounts                       | Storage           
RefAppFuncAPIPlan                           | Microsoft.Web/serverFarms                               | functionapp       
RefAppFuncAPI                               | Microsoft.Web/sites                                     | functionapp       

<UL>
References:
<li><a href="https://docs.microsoft.com/en-us/azure/azure-functions/functions-triggers-bindings">Azure Functions triggers and bindings concepts</a></li>
<li><a href="https://azure.microsoft.com/en-us/solutions/architecture/mobile-app-consumer/">Microsoft ask-based consumer mobile app Guidance</a></li>
</UL>
