using KindredPOC.API.Models;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.NotificationHubs.Messaging;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace KindredPOC.API
{
    public static class Item
    {
        private static string key = TelemetryConfiguration.Active.InstrumentationKey = System.Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY", EnvironmentVariableTarget.Process);
        private static TelemetryClient telemetry = new TelemetryClient() { InstrumentationKey = key };

        [FunctionName("Item")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "GET", "POST", "DELETE", "PUT", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            telemetry.Context.Operation.Id = req.GetCorrelationId().ToString();
            telemetry.Context.Operation.Name = "KindredMobile Function API";
            IDataRepository Repo = new DataRepository(); //TODO: setup DI
            var BL = new Code.BusinessLayer(Repo);
            System.Diagnostics.Stopwatch perfTimer = new System.Diagnostics.Stopwatch();
            perfTimer.Start();
            DateTime start = DateTime.Now;
            log.Info($"KindredMobile Function API {req.GetActionDescriptor()}");
            telemetry.TrackEvent("Item Function API called");
            telemetry.TrackMetric("Event timeline", perfTimer.ElapsedMilliseconds);

            switch (req.Method.Method)
            {
                case "GET":
                    try
                    {
                        string take = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "take", true) == 0).Value;
                        string skip = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "skip", true) == 0).Value;
                        // Get request body
                        dynamic data = await req.Content.ReadAsAsync<object>();

                        // Set name to query string or body data
                        take = take ?? data?.take;
                        skip = skip ?? data?.skip;
                        int takeint = 0;
                        int skipint = 0;
                        int.TryParse(take, out takeint);
                        int.TryParse(skip, out skipint);

                        System.Collections.Generic.IEnumerable<Models.Item> items = await BL.GetItems(Repo, perfTimer, takeint, skipint);
                        if (items != null)
                        {
                            return req.CreateResponse(HttpStatusCode.OK, items, "application/json");
                        }
                        else
                        {
                            return req.CreateResponse(HttpStatusCode.NoContent);
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Info($"Fail get");
                        telemetry.TrackException(ex);
                        return req.CreateErrorResponse(HttpStatusCode.BadGateway, ex.Message);
                    }

                    break;

                case "POST":
                    try
                    {
                        string data = await req.Content.ReadAsStringAsync();
                        if (data == null || data.Length == 0) throw new ArgumentNullException("No item was supplied for Save");
                        Models.Item item = JsonConvert.DeserializeObject<Models.Item>(data);
                        var saveditem = await BL.SaveItem(item, Repo, perfTimer);
                        if (saveditem != null)
                        {
                            await SendNotification($"New item added:{saveditem.Text}");
                            return req.CreateResponse(HttpStatusCode.OK, saveditem);
                        }
                        else
                        {
                            return req.CreateResponse(HttpStatusCode.BadRequest, item);
                        }
                    }
                    catch (Newtonsoft.Json.JsonSerializationException jsonex)
                    {
                        telemetry.TrackException(jsonex);
                        return req.CreateResponse(HttpStatusCode.BadRequest, "Could not read submitted Item");
                    }
                    catch (Exception ex)
                    {
                        telemetry.TrackException(ex);
                        return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                    }

                case "DELETE":
                    try
                    {
                        string Id = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "Id", true) == 0).Value;
                        dynamic data = await req.Content.ReadAsAsync<object>();
                        Id = Id ?? data?.Id;
                        if (Id == null)
                        {
                            return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass an Id on the query string or in the request body");
                        }
                        bool Success = await BL.DeleteItem(Id, Repo, perfTimer);
                        if (Success)
                        {
                            await SendNotification($"Item Removed:{Id}");
                            return req.CreateResponse(HttpStatusCode.OK, Id);
                        }
                        else
                        {
                            return req.CreateResponse(HttpStatusCode.BadRequest, Id);
                        }
                    }
                    catch (Exception ex)
                    {
                        return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                    }
                case "PUT":
                    try
                    {
                        string data = await req.Content.ReadAsStringAsync();
                        Models.Item item = JsonConvert.DeserializeObject<Models.Item>(data);
                        bool Success = await BL.UpdateItem(item, Repo, perfTimer);
                        if (Success)
                        {
                            await SendNotification($"Item updated:{item.Text}");
                            return req.CreateResponse(HttpStatusCode.OK, item);
                        }
                        else
                        {
                            return req.CreateResponse(HttpStatusCode.BadRequest, item);
                        }
                    }
                    catch (Exception ex)
                    {
                        return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                    }
                default:
                    return req.CreateResponse(HttpStatusCode.InternalServerError, "Action Not Identified");
            }
        }

        public static async Task SendNotification(string Message)
        {
            try
            {
                var HubCon = ConfigurationManager.ConnectionStrings["NotificationHubCnx"].ConnectionString;
                var HubName = System.Environment.GetEnvironmentVariable("NotificationHubName", EnvironmentVariableTarget.Process);
                NotificationHubClient hubClient = NotificationHubClient.CreateClientFromConnectionString(HubCon, HubName);

                NotificationOutcome outcome = null;
                NotificationOutcome outcome2 = null;

                string[] tags = new string[0]; // no tags - just broadcast
                GCM_Msg msg = new GCM_Msg { notification= new Notification { body= Message } };
                //Andriod
                outcome = await hubClient.SendGcmNativeNotificationAsync(JsonConvert.SerializeObject(msg));
                
                //Apple APNS {"aps":{"alert":"Notification Hub test notification"}}
                //string aplnotif = "{ \"aps\" : {\"alert\":\"" + string.Format("{0}", Message) + "\"}}";
                //Apple
                //outcome2 = await hubClient.SendAppleNativeNotificationAsync(aplnotif); //tmp removed until portal setup for apple

                //manage outcomes if it's a usecase
            }
            catch (MessagingException ex1)
            {
                // When an error occurs, return a failure status.
                telemetry.TrackException(ex1);
            }
            catch (Exception ex)
            {
                // dont break on Notification Failures
                telemetry.TrackException(ex);
            }
        }
        internal class GCM_Msg
        {
            public string to { get; set; }
            public string priority { get; set; }
            public Notification notification { get; set; }
            public Data data { get; set; }
        }
        internal class Data
        {
            public string volume { get; set; }
            public string contents { get; set; }
        }
        internal class Notification
        {
            public string body { get; set; }
            public string title { get; set; }
            public string icon { get; set; }
        }
    }
}