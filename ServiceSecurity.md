## Security

Best Practice is to secure services in azure with a Identity Management provider.  This reference uses Azure Active Directory (AAD).  In order to access the services secured through AAD a Security Token must be generated from the users credentials via AAD.

The following procedure to establishes the specific registrations that define which resources and permissions a user can access via the registered mobile application.

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
![Screenshot of Function Platform Features](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_01.png)

#### Step 2: Select Express ####
As highlighted in the screen shot below, do the following:
##### 2.1 Turn on the Authorization feature (1) #####
##### 2.2 Select Azure Active Directory from the listed providers (2) #####
##### 2.3 Configure Azure Active Directory by selecting <i>Express</i> (3) ######
![Screenshot of selecting Express Security](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_02.png)

#### 2.4:Result ####
This will create an app registration [Azure Active Directory -> App Registrations] in the tenant as shown
![Screenshot of App Registration](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_03.png) 

And an Enterprise Application [Azure Active Directory -> Enterprise Applications] entry which allows a native app registration to reference this resource.  The circled Application ID Is the Resource ID you will use in the mobile application configuration to generate a security token.
![Screenshot of Function API Enterprise App Listing](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_04.png)

### 3.0 CORS ###
Currently CORS is set to "*" which allows all.  For mobile use with authentication CORS is not applicable.