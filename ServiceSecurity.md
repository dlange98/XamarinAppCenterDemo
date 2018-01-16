## Security

Best Practice is to secure services in azure with a Identity Management provider.  This reference uses Azure Active Directory (AAD).  In order to access the services secured through AAD a Security Token must be generated from the users credentials via AAD.

The following procedure establishes the specific registrations that define which resources and permissions a user can access via the registered mobile application.

Key Elements necessary for Token Generation :
<ul>
<li>Generating Resource ID from Service Application Registration in Azure Active Directory (AAD)</li>
<li>Generating Client ID from Mobile pplication Registration in AAD</li>
<li>Required Permissions established on Application Registration</li>
</ul>

### Registering the Service ###
The following is how we enable security for the Azure Functions API and register the Application in AAD as a Resource
#### Step 1: Select Express Security for the Functions App in Azure ####
In Azure select the Functions API Application Resource.  In the Application Blade (shown below) select the authentication / Authorization menu item.
![Screenshot of Function Platform Features](https://abaf81c44da6f407f97a8bac.blob.core.windows.net/screenshots/SC_Security_01.png)

#### Step 2: Select Express Management Mode####
As highlighted in the screen shot below, do the following:
##### 2.1 Turn on the Authorization feature (1) #####
##### 2.2 Select Azure Active Directory from the listed providers (2) #####
##### 2.3 Configure Azure Active Directory by selecting <i>Express</i> (3) Management mode ######
![Screenshot of selecting Express Security](https://abaf81c44da6f407f97a8bac.blob.core.windows.net/screenshots/SC_Security_02.png)

#### 2.4:Result ####
This will create an app registration [Azure Active Directory -> App Registrations] in the tenant as shown below.

![Screenshot of App Registration](https://abaf81c44da6f407f97a8bac.blob.core.windows.net/screenshots/SC_Security_03.png) 

And an Enterprise Application [Azure Active Directory -> Enterprise Applications] entry which allows a native app registration to reference this resource.  The circled Application ID Is the <b>Resource ID</b> you will use in the mobile application configuration to generate a security token.

![Screenshot of Function API Enterprise App Listing](https://abaf81c44da6f407f97a8bac.blob.core.windows.net/screenshots/SC_Security_04.png)

### Registering the Client ###
The following is how we enable security for the Mobile Client and establish it has permission to use the resources registered in the previous step.

#### Register a new Application in AAD  ####

Create a new App Registration in AAD.  Select Native as Application type and Name it appropriatly.  Enter a placeholder Redirect URL (i.e. https://Localhost).  This will need to be matched in calls to the token provider, but no redirect will be used.
![Screenshot of Creating new App Registration](https://abaf81c44da6f407f97a8bac.blob.core.windows.net/screenshots/SC_Security_05.png)

Now we must ensure the correct permissions are granted the App Registration.  In the Required Permissions Tab, select ADD to include the Resource Registered in the previous step.  Once included in the API List, ensure Delegated Permissions includes Access to the Resource

![Screenshot of adding Permissions to new Client App Registration](https://abaf81c44da6f407f97a8bac.blob.core.windows.net/screenshots/SC_Security_06.png)

The Client ID required for token generation is the Application ID of the new Registration, as indicated below.  This will be needed to allow the mobile client to aquire the security token

![Screenshot of Client ID in App Registration](https://abaf81c44da6f407f97a8bac.blob.core.windows.net/screenshots/SC_Security_07.png)

### 3.0 CORS for Service API ###
Currently CORS is set to "*" which allows all.  For mobile use with authentication CORS is not applicable.

<UL>
References:
<li><a href="https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-protocols-oauth-code">Authorize access to web applications using OAuth 2.0 and Azure Active Directory</a></li>
<li><a href="https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-authentication-scenarios">Authentication Scenarios for Azure AD</a></li>
<li><a href="https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-v2-tokens">Azure Active Directory v2.0 tokens reference</a></li>
<li><a href="https://docs.microsoft.com/en-us/azure/azure-functions/functions-how-to-use-azure-function-app-settings" >How to manage a function app in the Azure portal</a></li>
<li><a href="https://contos.io/working-with-identity-in-an-azure-function-1a981e10b900">Working with identity in an Azure Function</a></li>
</UL>