# Reference Architecture Walkthrough #
## Contents ##
<table>
    <tr>
        <th>Topic</th>
        <th>Page</th>
    </tr>
    <tr>
        <td>Infrastructure</td>
        <td></td>
    </tr>
    <tr>
        <td>Security</td>
        <td></td>
    </tr>
    <tr>
        <td>Mobile : Common</td>
        <td></td>
    </tr>
    <tr>
        <td>Mobile : IOS</td>
        <td></td>
    </tr>
    <tr>
        <td>Mobile: Andriod</td>
        <td></td>
    </tr>
    <tr>
        <td>Services</td>
        <td></td>
    </tr>
    <tr>
        <td>CI/CD</td>
        <td></td>
    </tr>
</table>

## Infrastructure ## 
The infrastructure is composed of the following elements in the Azure Environment.
<table>
    <tr>
        <th>Resource Name</th>
        <th>Type</th>
        <th>Version</th>
    </tr>
<tr>
        <td>OauthBackend_ApplicationInsights</td>
        <td>microsoft.insights/components</td>
        <td></td>
    </tr>
<tr>
        <td>ApplicationInsights(KindredPOCAnalytics)</td>
        <td>Microsoft.OperationsManagement/solutions</td>
        <td></td>
    </tr>
<tr>
        <td>NetworkMonitoring(KindredPOCAnalytics)</td>
        <td>Microsoft.OperationsManagement/solutions</td>
        <td></td>
    </tr>
<tr>
        <td>ServiceMap(KindredPOCAnalytics)</td>
        <td>Microsoft.OperationsManagement/solutions</td>
        <td></td>
    </tr>
<tr>
        <td>KindredPOCAnalytics</td>
        <td>Microsoft.OperationalInsights/workspaces</td>
        <td></td>
    </tr>
<tr>
        <td>kindredpoc</td>
        <td>Microsoft.Sql/servers</td>
        <td>v12.0</td>
    </tr>
    <tr>
        <td>kindredpoc/KindredPOC</td>
        <td>Microsoft.Sql/servers/databases</td>
        <td>v12.0,user</td>
    </tr>
    <tr>
        <td>a65edf37839fb441e9d71f25</td>
        <td>Microsoft.Storage/storageAccounts</td>
        <td></td>
    </tr>
    <tr>
        <td>OAuthBackendPlan</td>
        <td>Microsoft.Web/serverFarms</td>
        <td>functionapp</td>
    </tr>
    <tr>
        <td>OAuthBackend</td>
        <td>Microsoft.Web/sites</td>
        <td>functionapp</td>
    </tr>
</table>

*DAN Need Mobile Center and Push Notification Elements Added*

![Screenshot of Resources in Azure](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Resources.PNG)

## Security ##
We followed the following procedure to establish Mobile App Security and Service Access Security

Step 1:  Select Express Security for the Functions App in Azure
![Screenshot of Function Platform Features](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_01.png)
Step 2: Select Express
![Screenshot of selecting Express Security](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_02.png)
This will create an app registration [Azure Active Directory -> App Registrations] in the tenant
![Screenshot of App Registration](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_03.png)
And an Enterprise Application [Azure Active Directory -> Enterprise Applications] entry which allows a native app registration to reference this resource
![Screenshot of Function API Enterprise App Listing](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_04.png)

*DAN write up of Mobile Setup*
### Securing Functions API ###

### Securing Configuration - Mobile Access to API ###

### Mobile : Common ###
        
### Mobile : IOS Specific Elements

### Mobile: Andriod Specific Elements

### Services ###

#### Service Listing ####
#### Architecture Diagram ####
#### Unit Testing ####

### CI/CD ###
#### Build Definition ####
### Testing Gateway ####
#### Deployment ####




