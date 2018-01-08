
## Infrastructure Listing

The infrastructure is composed of the following elements in the Azure Environment.
# !!! Regen List FOR Kindred Azure Environment !!!
Resource Name                            | Type                                     | Version
---------------------------------------- | ---------------------------------------- | -----------
OauthBackend_ApplicationInsights         | microsoft.insights/components            |
ApplicationInsights(KindredPOCAnalytics) | Microsoft.OperationsManagement/solutions |
NetworkMonitoring(KindredPOCAnalytics)   | Microsoft.OperationsManagement/solutions |
ServiceMap(KindredPOCAnalytics)          | Microsoft.OperationsManagement/solutions |
KindredPOCAnalytics                      | Microsoft.OperationalInsights/workspaces |
kindredpoc                               | Microsoft.Sql/servers                    | v12.0
kindredpoc/KindredPOC                    | Microsoft.Sql/servers/databases          | v12.0,user
a65edf37839fb441e9d71f25                 | Microsoft.Storage/storageAccounts        |
OAuthBackendPlan                         | Microsoft.Web/serverFarms                | functionapp
OAuthBackend                             | Microsoft.Web/sites                      | functionapp

Logical Data Structure ![Logical Diagram of Reference Application](https://abaf81c44da6f407f97a8bac.blob.core.windows.net/screenshots/SC_Diagram_01.png) 


![Screenshot of Resources in Azure](https://abaf81c44da6f407f97a8bac.blob.core.windows.net/screenshots/SC_Resources.PNG)

<UL>
References:
<li><a href="https://docs.microsoft.com/en-us/azure/azure-functions/functions-triggers-bindings">Azure Functions triggers and bindings concepts</a></li>
<li><a href="https://azure.microsoft.com/en-us/solutions/architecture/mobile-app-consumer/">Microsoft ask-based consumer mobile app Guidance</a></li>
</UL>
