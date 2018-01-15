
using System;
using Android.App;
using Firebase.Iid;
using Android.Util;
using WindowsAzure.Messaging;
using System.Collections.Generic;
using Refapp.Configuration;
namespace Refapp.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FirebaseIIDService : FirebaseInstanceIdService
    {
        const string TAG = "FirebaseIIDService";
        NotificationHub hub;

        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "FCM token: " + refreshedToken);
            SendRegistrationToServer(refreshedToken);
        }

        void SendRegistrationToServer(string token)
        {
            // Register with Notification Hubs
            //hub = new NotificationHub(Settings.NotificationHubName,
                                      //Settings.ListenConnectionString, this);

            hub = new NotificationHub(Settings.NotificationHubName,
                           Settings.ListenConnectionString, this);


            var tags = new List<string>() { };
            var regID = hub.Register(token, tags.ToArray()).RegistrationId;

            Log.Debug(TAG, $"Successful registration of ID {regID}");
        }
    }
}
