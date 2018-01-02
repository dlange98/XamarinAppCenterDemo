# Services Reference Architecture Walkthrough

## Contents

Topic           | Page
--------------- | ----
Service Architecture  | [Service Architecture Doc](./Infrastructure.md)
Security        |
Mobile          | [Mobile Architecture Doc](./MOBILE.md)
Services        |
CI/CD           | [DevOps Doc](./DevOps.md)
## Service Architecture
## Security

We followed the following procedure to establish Mobile App Security and Service Access Security

Step 1: Select Express Security for the Functions App in Azure ![Screenshot of Function Platform Features](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_01.png) Step 2: Select Express ![Screenshot of selecting Express Security](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_02.png) This will create an app registration [Azure Active Directory -> App Registrations] in the tenant ![Screenshot of App Registration](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_03.png) And an Enterprise Application [Azure Active Directory -> Enterprise Applications] entry which allows a native app registration to reference this resource ![Screenshot of Function API Enterprise App Listing](https://a65edf37839fb441e9d71f25.blob.core.windows.net/screenshots/SC_Security_04.png)

## Mobile Client Architecture

The [Mobile Architecture Doc](./MOBILE.md) contains the documentation for the Mobile Clients.
