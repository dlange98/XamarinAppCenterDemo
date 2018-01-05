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

This section describes setting up the Unit and the UITests for the applications.

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

At this time you are not able to run unit tests as part of the automated build on App Center. However, you could create a a ate a post build script for example (if project name was RefApp.NUnitTests ):

```
echo "-= Build nUnit test projects: =-"
find . -regex '.*\.NUnitTests\.csproj' -exec msbuild {} \;
echo
echo "-= Found projects to run nUnit tests: =-"
find . -regex '.*bin.*\.NUnitTests\.dll' -exec echo {} \;
echo
echo "-= Running nUnit tests: =-"
find . -regex '.*bin.*\.NUnitTests\.dll' -exec nunit-console {} \;
echo
echo "-= nUnit test result: =-"
find . -name 'TestResult.xml' -exec cat {} \;
```

## App Center - Test Cloud

App Center Test Cloud allows running the unit tests on many different devices. This reduces the need to have the actual hardware in house. The tests are initialed from the command line of a local machine. The results will be displayed in the App Center console.

Perform the following steps to create a test run. See [Preparing Xamarin.UITests for Upload](https://docs.microsoft.com/en-us/appcenter/test-cloud/preparing-for-upload/uitest) for details.

1. Verify you have uncommented the following line in the iOS `AppDelegate.cs` file

  ```
  #if ENABLE_TEST_CLOUD
  Xamarin.Calabash.Start();
  #endif
  ```

2. For iOS [Create the IPA](https://developer.xamarin.com/guides/ios/deployment,_testing,_and_metrics/app_distribution/ipa_support/). For Android [Create the APK](https://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/)

3. Navigate to to Test/Test Runs in the app center project.

4. Select your devices(s) in which you wish to test.

5. On the next screen, Select your Test Framework. In this case it's `Xamarin.UITest`.

6. The next screen displays the documentation to setup your local environment for testing. Note the goal is configure your environment to run the `appcenter` command line tool.
7. Copy and run the command line that is generated by the App Center in the project directory.

For IOS

```
  appcenter test run uitest --app "Roland.Robertson/refapp.kindred.com" --devices 7f669125 --app-path ./iOS/RefApp.ipa  --test-series "launch-tests" --locale "en_US" --build-dir ./RefappUITest/bin/Debug/
```

For Android:

```
appcenter test run uitest --app "Roland.Robertson/droid.refapp.kindred.com" --devices 0401170d --app-path pathToFile.apk  --test-series "launch-tests" --locale "en_US" --build-dir pathToUITestBuildDir
```

1. Note the `--build-dir` in the RefApp is located at _./RefAppUITTest/bin/Debug/_
2. Note the `--app-path` is located at the location where the IPA file was placed when it was created.
3. Once app is uploaded it will take 5 to 20 minutes to test the app. You can close the command line and the testing will continue.

### Adding Screen Shots

During the test runs, screen shots can be taken. This allows debuging of the test process and can display where failing tests failed. The following code adds

```
app.Screenshot("Screen Title");
```

## Analytics
