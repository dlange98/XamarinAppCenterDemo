# RefApp Mobile Clients Architecture Guide

## Client Solution Structure

The mobile app projects in the _RefApp_ solution consists of five projects:

- `RefApp` - The bulk of the reusable code. This is the main forms project, that contains 95 percent of the code both for the IOS and the Android apps. Note this project is configured as a .net 2.0 standard library.
- `RefApp.Droid` - The Android platform specific code
- `RefApp.IOS` - The IOS platform specific code
- `RefApp.NUnitTests` - The code for the NUnit tests.
- `RefAppUITTest` - The code for the UI Tests.

## Creating the IOS and Android Projects

### Initial Project Creation

Follow the standard mobile app solution creation process in Visual Studio. Select the Blank forms app, then select use Portable Class Library, this will create the 3 main forms projects in the solution. The add automated UI test project can also be selected at this time. This will create a forms PCL (Portable Class Library) project which needs to be converted to a `.NET 2.0 standard` project. See the steps in the next section to convert the project.

### Convert the Forms Project to the .NET 2.0 standard

The .NET 2.0 project standard is the latest and greatest project structure to allow sharing of code between applications. This new standard replaces the PCL standard. Note, the IOS and Android app are each built as a separate app. The forms app is linked to each of the two app executables. To convert to `.NET 2.0`, you need to create a new project and copy the source from the 'old' project to the new one. A summary of the steps is outlined below and can be found in detail at [Building Xamarin Forms with .NET Standard](https://blog.xamarin.com/building-xamarin-forms-apps-net-standard/).

1. Verify that .NET Core 2.0 is installed.
2. Add a new project to the solution that is a .NET standard. Do this by selecting Library under .NET Core when creating a new project in the Visual Studio.
3. Delete the _class.cs_ file that's added to the .NET Standard library. This class is not needed.
4. Add Xamarin Forms via NuGet to the new project.
5. In the iOS and android project remove the reference to the old forms project and add a reference to the new forms (.NET Standard) project.
6. Copy the source files from the old forms project to the new forms project.
7. Delete the old PCL project.
8. Add the Microsoft.NETCore.Portable.Compatability via NuGet to the new Forms Project.

The last step will allow adding PCL standard frameworks to link with the new .NET 2.0 Standard [.NET Standard support](https://blog.xamarin.com/net-standard-library-support-for-xamarin/). So the goal with the NuGet packages is to use the .NET 2.0 Standard library if one exists. If not, the PCL library 'may' function, but not always.

### Packages added via NuGet to all Projects (except the tests projects)

The following packages where added to the projects in addition to the packages added by the new project wizard. Note adding these packages will also added the required referenced packages.

- `Microsoft.NETCore.Portable.Compatability` - see above
- `Microsoft.AppCenter` - Used for base Visual Studio app center functionality
- `Microsoft.AppCenter.Analytics` - AppCenter Analytics
- `Microsoft.AppCenter.Crashes` - AppCenter Crash Reporting
- `Newtonsoft.Json` - JSON marshaling Library
- `sqlite-net.pcl (1.4.118)` - local db persistence. Note the version number and case, there are a number of sqlite frameworks in NuGet. Verify you are obtaining the Frank A Krueger version.
- `Xam.Plugin.Connectivity` - API to obtain network information such as connection availability.

### Additional Packages added to the Android and iOS projects

- `Microsoft.IdentityModel.Clients.ActiveDirectory` - Framework to allow authentication with Azure

### Packages for the RefAppUITTest Project

- `NUnit` - Unit testing framework
- `Xamarin.UITest` - UITesting framework. Note this is added when creating a UITest project.
- Reference to the RefApp project.
- Reference to the RefApp dll (see the .Net Assembly tab in Edit References)

### Packages for the RefApp.NUnitTests Project

- `NUnit` - Unit testing framework
- `Xamarin.Forms` - Need for some of the unit tests to compile.
- Reference to the RefApp project

## Project Structure

The main project as well as the iOS and Android project follow this pattern. 95 percent of the source code is found in the `RefApp` project. This project is broken up into the following folder structure:

- `Configuration` - This contains the `Settings.cs` file. All configurable variables such as AppServiceURL are located in this folder.
- `DAO` - Data access classes are located in this folder.
- `Services` - Services such as the server api, local persistence etc, are located in this folder
- `Models` - Model objects such as the Item
- `ViewModels` - The view models used in the views. The project is following the [MVVM](https://developer.xamarin.com/guides/xamarin-forms/xaml/xaml-basics/data_bindings_to_mvvm/) pattern.
- `Views` - The Forms xaml and the code behind classes

### Android Project

The structure of the Android project has a few changes and additions to the generated code.

- `MainActivity.cs` contians the following code for authentication:

  ```
  protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
  {
      base.OnActivityResult(requestCode, resultCode, data);
      AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
  }
  ```

- `Services` - The implementation for the FileManager and the AuthenticationServices interfaces that are invoked in the Forms project.

### iOS Project

The iOS project has a few small additions similar to the android project:

- `Services` - The implementation for the FileManager and the AuthenticationServices interfaces.
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

After the user has successfully completed the login process, the login dialog is dismissed and the _authResult_ contains our token that we embedded in the APIs calls. The following line of code pulls out the accessToken from the _authResult_ object:

```
 AccessToken = authResult.AccessToken
```

Note the _LoginAsync_ method in the `CloudDataStore.cs` class persists the token to the local data store after a successful login. This token is then retrieved from the data store and placed in the header, before each server api call, in the method _UpdateAuthHeaderToken_. The following code where the auth token is being updated before each API call:

```
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
```

### Security Configuration

The settings.cs file contains the configuration for authentication. The following elements need to be configured:

- `TenateId` - This is the ID of the Azure Tenant. It can be found in the Azure ActiveDirectory portal under properties.
- `ResourceId` - The is the id of the server side API project resource. The individual that configures the server API can provide this information.
- `ClientId` - A _client resource_ is configured on azure. Please see the [Security Doc](./ServiceSecurity.md) for more information about this value.
- `ReturnUrl` - This value can be any valid url. It must match the value that's configured on Azure for the _client resource_.

## HTTP requests

The `CloudDataStore.cs` file in the Forms project contains the code to invoke the server API's. Note we are using the standard .net _HTTPClient_ to invoke the server API's. JSON is marshaled via the _Newtonsoft.Json_ library.

### Unmarshaling objects

The following two lines of code demonstrate invoking the 'item' api:

```
  var json = await client.GetStringAsync($"api/item/");
  items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json));
```

The JSON string is unmarshalled into _item_ objects. Also note the item object is a plain C# object.

### Marshaling objects

The following code demonstrates marshaling an item object to JSON and then invoking the _item api_:

```
var serializedItem = JsonConvert.SerializeObject(item);
var response = await client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
```

## Local Persistance

Local persistence in the Android and iOS apps use a local sqlite database. In this application the access token is being persisted to sqlite via the `AccessTokenDAO`. Note the following lines of code persist the token object:

```
...
var db = getDBConnection();
db.BeginTransaction();
db.Insert(token);
db.Commit();
...
```

The _getDBConnection_ method is implemented in the `BaseDAO.cs` class.

```
protected SQLiteConnection getDBConnection()
{
  var path = FileManager.GetDBPath(Settings.DBName);
  var conn = new SQLiteConnection(path);
    return conn;
}
```

In order to create a local sqlite DB, the `FileManager` code needs to be implemented in the native project for iOS and Android. This is due to the fact we are accessing the native file system of each platform. The [_Xamarin Dependency service_](https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/dependency-service/introduction/) is being used to ensure the correct code is being called for each platform.

## Push Notifications

Follow the sections that describe configuring push notifications for each platform. Note we are modifying the native apps given notifications are are native to each platform.

### Android

To add push notifications to android follow the [Android push notifications](https://github.com/MicrosoftDocs/azure-docs/blob/master/articles/notification-hubs/xamarin-notification-hubs-push-notifications-android-gcm.md)

Note after you complete the steps in the link above you need to add the following to the `AndroidManifest.xml` file. The _category:name_ need to match the projects package name.

```
  <receiver
    android:name="com.google.firebase.iid.FirebaseInstanceIdReceiver"
    android:exported="true"
    android:permission="com.google.android.c2dm.permission.SEND" >
    <intent-filter>
          <action android:name="com.google.android.c2dm.intent.RECEIVE" />
          <category android:name="com.cardinal.testpushdan" />
    </intent-filter>
  </receiver>
```

### iOS

To add push notification to iOS follow [iOS push notifications](https://github.com/MicrosoftDocs/azure-docs/blob/master/articles/notification-hubs/xamarin-notification-hubs-ios-push-notification-apns-get-started.md)
