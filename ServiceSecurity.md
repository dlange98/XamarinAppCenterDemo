## Security

We followed the following procedure to establish Mobile App Security and Service Access Security

### Step 1: Select Express Security for the Functions App in Azure 
Select the authentication / Authorization menu item as shown below
![Screenshot of Function Platform Features](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_01.png)

### Step 2: Select Express 
#### 2.1 Turn on the Authorization feature (1) ####
#### 2.2 Select Azure Active Directory from the listed providers
#### 2.3 Configure Azure Active Directory by selecting <i>Express</i> #####
![Screenshot of selecting Express Security](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_02.png)

#### 2.4:Result ####
This will create an app registration [Azure Active Directory -> App Registrations] in the tenant as shown
![Screenshot of App Registration](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_03.png) 

And an Enterprise Application [Azure Active Directory -> Enterprise Applications] entry which allows a native app registration to reference this resource 

![Screenshot of Function API Enterprise App Listing](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_04.png)

### 3.0 CORS ###
Currently CORS is set to "*" which allows all.  For mobile use with authentication CORS is not applicable.