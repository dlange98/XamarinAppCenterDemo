using System;
namespace Refapp.Configuration
{
    public class Settings
    {
        public static string DBName = "reference.db3";

        /* URL for our rest services */
        public static string AppServiceURL = "https://oauthbackend.azurewebsites.net";

        /* Settins for authenticating against asure ad */
        public static string TenantId = "https://login.windows.net/414efc33-68fe-4520-802f-aea4401192d0";
        public static string ResourceId = "1c96f60e-5d95-47b0-b113-a1837b99f623";
        public static string ClientId = "8d08c14d-1fab-4cbc-8766-b02816e8589a";
        public static string ReturnUrl = $"https://localhost";

        /* Settings for App Center */

    }
}
