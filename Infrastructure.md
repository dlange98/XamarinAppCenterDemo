
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

Logical Data Structure ![Logical Diagram of Reference Application](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Diagram_01.png) 


![Screenshot of Resources in Azure](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Resources.PNG)
