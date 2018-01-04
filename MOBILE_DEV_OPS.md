# RefApp Mobile Dev Ops Guild

This document describes the dev ops process and features for the iOS and Android RefApps. The dev ops process uses the [Visual Studio App Center](https://www.visualstudio.com/app-center/)

## Continuous Integration

The iOS and Android CI processes are using _AppCenters_ build feature. The build process does not require modification of the project or the code itself.

### Configure the build

Perform the following to configure the build. The summary of the steps are:

1. link to your repository. In this case we are linking to VSTS.
2. Select a branch.
3. Select Build frequency. Normally this is set to build on every push. This will provide a true CI process where code is built, tested and pushed on every checkin. One could configure multiple branches to allow for pushing builds to different groups at different times.
4. Select Sign the build - This will allow distribution of the builds.
5. Select auto increment build number. This will allow identification of each build. Normally this build number is displayed in the app.
6. Select distribute the builds - This will distribute the builds to the a chosen group of users. By default, the users that are tied to the current app are selected.

The exact steps are documented here: [iOS build](https://docs.microsoft.com/en-us/appcenter/build/ios/), [Android Build](https://docs.microsoft.com/en-us/appcenter/build/android/).

Note, we found that for IOS, manual code signing seems to function with the least amount of issues.

## Unit and UI Testing

This section describes setting up the Unit and the UITests for the applications. Once the tests are set up then the build process can run the tests during each build. We can also run the UITests on the Xamarin Test cloud. Both will be described below.

### Unit Testing

To set up the unit test add a new project type of UITestApp. It can be found under the Multiplatform/Tests in the new project wizard. The UITest will added the `NUnit` and `Xamarin.UITest` packages to the new project. Note the reference solution's testing project is `RefAppUITTest`. This project handles both the UI and the Unit tests. You will also need to add reference to the main Forms app, in this case there is a reference to the `RefApp` project.

The `UnitTest.cs` file contains a few sample Unit tests. An interesting test to note is the `SettingIsBusyPropertyShouldRaiseProertyChanged` tests. The gaol of this test to to verify that modifying a property on an object with raise a property changed evert. This verifies that this object will function correctly with binding. For more information on unit testing for Forms see: [Unit Testing Forms Apps](https://developer.xamarin.com/guides/xamarin-forms/enterprise-application-patterns/unit-testing/).

### UITesting

The UITests are configured in the `AppInitializer.cs`. Note both iOS and Android need to be configured. The app must be installed on the simulator or emulator before running the tests. Also note the Device Identifier line for the iOS App. This identifies a specific emulator. You can find the ID of a currently running iOS emulator with the following command:

```
xcrun simctl list | egrep '(Booted)'
```

The `UITests.cs` contain the sample code that implement the UI Tests. Both the UI and Unit tests can be ran within visual studio from the `Unit Test tab`. Note the UITests are able to test the web view login process. For example the following code waits for an input field, enters a users email address, then hits enter:

```
app.WaitForElement(c => c.Css("input#i0116"));
app.EnterText(c => c.Css("input#i0116"), "fred@cardinalsolutions.com");
app.PressEnter();
```

Form more information about testing webviews see: [UITest webviews](https://developer.xamarin.com/guides/testcloud/uitest/working-with/webviews/)

Building UI Tests can be complex. The [UITest](https://developer.xamarin.com/guides/testcloud/uitest/) documentation provides a good place to start coming up to speed on the test syntax.

### Running the tests from the IDE

To run the test from the IDE simple select the _NUnit test view_ and right click on the test you wish to run.

### Running the tests as part of the build.

Select _test on a real device_ will run the UITests on a real device during the build process. Note the NUnit tests are automatically ran during the build process.

## App Center - Test Cloud

The app center test cloud allows running the unit tests in on many different devices in the cloud.
