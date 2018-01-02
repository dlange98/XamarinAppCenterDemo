# Reference Architecture Walkthrough

## Contents

Topic           | Page
--------------- | ----
Infrastructure  |
Security        |
Mobile : Common |
Mobile : IOS    |
Mobile: Andriod |
Services        |
CI/CD           |

## Infrastructure

The infrastructure is composed of the following elements in the Azure Environment.

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

Logical Data Structure ![Logical Diagram of Reference Application](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Diagram_01.png) _DAN Need Mobile Center and Push Notification Elements Added_

![Screenshot of Resources in Azure](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Resources.PNG)

## Security

We followed the following procedure to establish Mobile App Security and Service Access Security

Step 1: Select Express Security for the Functions App in Azure ![Screenshot of Function Platform Features](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_01.png) Step 2: Select Express ![Screenshot of selecting Express Security](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_02.png) This will create an app registration [Azure Active Directory -> App Registrations] in the tenant ![Screenshot of App Registration](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_03.png) And an Enterprise Application [Azure Active Directory -> Enterprise Applications] entry which allows a native app registration to reference this resource ![Screenshot of Function API Enterprise App Listing](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_04.png)

## Mobile Client Architecture

The [Mobile Architecture Doc](./MOBILE.md) contains the documentation for the Mobile Clients.
