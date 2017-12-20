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

## Security ##
We followed the following procedure to establish Mobile App Security and Service Access Security
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




