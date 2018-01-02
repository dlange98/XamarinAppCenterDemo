# RefApp Mobile Clients Architecture Guide

## Client Project Structure

The mobile apps consists of four projects:

- `RefApp` - The bulk of the reusable code. This is the main forms project, that should contain 95 percent of the code both for the IOS and the Android projects. Note this project is configured as a .net 2.0 standard library.
- `RefApp.Droid` - The Android platform specific code
- `RefApp.IOS` - The IOS platform specific code
- `RefAppUITTest` - The code for the UI Tests

## Creating the IOS and Android Projects

### Initial Project Creation

Follow the standard mobile app solution creation process in Visual Studio. Select the Blank forms app, select use Portable Class Library, this will create the 3 main forms projects in the solution. This will create a forms PCL (Portable Class Library) project which needs to be converted to a `.NET 2.0 standard` project. See the steps in the next section to convert the project.

### Convert the Forms Project to the .NET 2.0 standard

The .NET 2.0 project standard is the latest and greatest project structure to allow sharing of code between applications. This new standar replaces the PCL standard. Note that the IOS and Android app are each built as a separate app. The forms app is linked to each of the two app executables. To convert to `.NET 2.0`, you need to create a new project and copy the source from the 'old' project to the new one. A summary of the steps is outlined below:

1. Verify that .NET Core 2.0 is installed.
2. Add a new project to the solution that is a .NET standard. Do this by selecting Library under .NET Core when creating a new project in the Visual Studio.
3. Delete the class that's added to the .NET Standard library. This class is not needed.
4. Add Xamarin Forms via NuGet
5. In the iOS and android project remove the reference to the old forms project and add a reference to the new forms (.NET Standard) project.
6. Copy the source files from the old forms project to the new forms project.
7. Delete the old PCL project.
8. Add the Microsoft.NETCore.Portable.Compatability via NuGet to the new Forms Project.

Note the last step will allow adding some non PCL standard frameworks to link with the new .NET 2.0 Standard forms project [.NET Standard support](https://blog.xamarin.com/net-standard-library-support-for-xamarin/). So the goal with the NuGet packages is to use the .NET 2.0 Standard library if one exists. If not, the PCL library 'may' function, but not always.

For more details on this new standard see [.NET 2.0 Standard Blog Post](https://blog.xamarin.com/building-xamarin-forms-apps-net-standard/).

### Packages added via NuGet to all Projects (except the RefAppUITTest project)

The following packages where added to the project in addition to the packages added by the new project wizard. Note adding these packages will also added the required referenced packages for each project.

- `Microsoft.NETCore.Portable.Compatability` - see above
- `Microsoft.AppCenter` - Used for base Visual Studio app center functionality
- `Microsoft.AppCenter.Analytics` - AppCenter Analytics
- `Microsoft.AppCenter.Crashes` - AppCenter crash reporting.
- `Newtonsoft.Json` - JSON marshaling Library
- `Service.Stack` - Client side http requests framework
- `sqlite-net.pcl (1.4.118)` - local db persistence. Note the version number and case, there are a number of sqlite frameworks in NuGet. This is the Frank A Krueger version.
- `Xam.Plugin.Connectivity` - API to obtain network information such as connection availability etc.

### Additional Packages added to the Android and iOS projects

- `Microsoft.IdentityModel.Clients.ActiveDirectory` - Framework to allow authentication with Azure

### Packages for the RefAppUITTest Project

- `NUnit` - Unit testing framework
- `Xamarin.UITest` - UITesting framework. Note this is added when creating a UITest project.

## Project Structure

The main project and the iOS and Android project follow this pattern. Note that 95 percent of the source code is found in the `RefApp` project. This project is broken up into the following packages:

- `Configuration` - This contains the `Settings.cs` file. All configurable variables such as AppServiceURL are located in this file.
- `DAO` - Data access objects are located in this package.
- `Services` - Services such as the server api, local persistence etc, are located in this packages
- `Models` - Model objects such as the Item
- `ViewModels` - The view models used in the views. The project is following the [MVVM](https://developer.xamarin.com/guides/xamarin-forms/xaml/xaml-basics/data_bindings_to_mvvm/) pattern.
- `Views` - The Forms and the code behind classes

### Android Project

The structure of the Android project has a few changes and additions to the generated code.

- `MainActivity.cs` that has the following code for authentication:

  ```
  protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
  {
      base.OnActivityResult(requestCode, resultCode, data);
      AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
  }
  ```

- `Services` - The implementation for the FileManager and the AuthenticationServices added to the project.

### iOS Project

The iOS project has a few small changes similar to the android project:

- `Services` - The implementation for the FileManager and the AuthenticationServices added to the project.
- `plist` - Is modified for the _AppCenter_ update notification functionality

## Security

The goal for the security model was to use the PAAS security that is provided by Azure. Thus, no code was needed on the server to secure the APIs. The client apps are using the [Microsoft IdentityModel for ActiveDirectory (ADAL)](https://www.nuget.org/packages/Microsoft.IdentityModel.Clients.ActiveDirectory/) framework.

### The AuthenticationService class

The `AuthenticationService.cs` class for both the android and iOS are very similar and implement the _login_ experience for the user. The application itself never has access to the user's credentials. The following code sets up the authentication context, the values for the various parameters are described in the security configuration section below:

```
  var authContext = new AuthenticationContext(authority);
  if (authContext.TokenCache.ReadItems().Any())
    authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

  var authParam = new PlatformParameters((Activity)Forms.Context);
```

The next line displays the login process to the user. Note this is an OATH type process, thus the user is presented with a web view to login.

```
  var authResult = await authContext.AcquireTokenAsync(resource, clientId, new Uri(returnUri), authParam);
```

After the user has successfully completed the login process, the login dialog is dismissed and the _authResult_ contains our token that we use for the APIs calls. The following line of code pulls out the accessToken from the _authResult_ object:

```
 AccessToken = authResult.AccessToken
```

### Security Configuration

The settings.cs file contains the configuration for security. The following elements need to be configured:

- `TenateId` - This is the ID of the Azure Tenant. It can be found in the Azure ActiveDirectory portal under properties.
- `ResourceId` - The is the id of the server side API project resource. The individual who set's up the server API can provide this information.
- `ClientId` - A client resource is configured on azure.

## API Docs

### Securing Functions API

### Securing Configuration - Mobile Access to API

### Mobile : Common

### Mobile : IOS Specific Elements

### Mobile: Andriod Specific Elements

### Services

#### Service Listing

#### Architecture Diagram

#### Unit Testing

### CI/CD

#### Build Definition

### Testing Gateway

#### Deployment
