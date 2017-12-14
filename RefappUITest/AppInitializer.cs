using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace RefappUITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            // TODO: If the iOS or Android app being tested is included in the solution 
            // then open the Unit Tests window, right click Test Apps, select Add App Project
            // and select the app projects that should be tested.
            //
            // The iOS project should have the Xamarin.TestCloud.Agent NuGet package
            // installed. To start the Test Cloud Agent the following code should be
            // added to the FinishedLaunching method of the AppDelegate:
            //
            //    #if ENABLE_TEST_CLOUD
            //    Xamarin.Calabash.Start();
            //    #endif
            //if (platform == Platform.Android)
            //{
            //    return ConfigureApp
            //        .Android
            //        // TODO: Update this path to point to your Android app and uncomment the
                    // code if the app is not included in the solution.
            //        .ApkFile ("../../../Droid/bin/Debug/xamarinforms.apk")
            //        .StartApp();
            //}

            return ConfigureApp
                .iOS
                // TODO: Update this path to point to your iOS app and uncomment the
                // code if the app is not included in the solution.
                // NOTE: Josh A, you must change the file location to match what is on your local machine
                .AppBundle ("../../../iOS/bin/iPhoneSimulator/Debug/device-builds/iphone9.1-10.3.1/Refapp.iOS.app") ///Users/jaudibert/Documents/Cardinal/MobileRefXamarin/iOS/bin/iPhoneSimulator/Debug/device-builds/iphone9.1-10.3.1/Refapp.iOS.app
                //.DeviceIdentifier("919CF061-A933-4ED9-99F4-1472CA364225")
                //.EnableLocalScreenshots()
                //.Debug()
                .StartApp();
            
        }
    }
}
