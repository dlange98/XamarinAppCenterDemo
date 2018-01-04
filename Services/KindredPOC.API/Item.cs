using KindredPOC.API.Code;
using KindredPOC.API.Models;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
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
                    return await BL.GetItems(req, log, Repo, perfTimer);
                    break;

                case "POST":
                    try
                    {
                        string data = await req.Content.ReadAsStringAsync();
                        if (data == null || data.Length == 0) throw new ArgumentNullException("No item was supplied for Save");
                        Models.Item item = JsonConvert.DeserializeObject<Models.Item>(data);
                        var saveditem =  await BL.SaveItem(item, Repo, perfTimer);
                        if (saveditem!=null)
                        {
                            return req.CreateResponse(HttpStatusCode.OK, saveditem);
                        }
                        else
                        {
                            return req.CreateResponse(HttpStatusCode.BadRequest, item);
                        }
                    }
                    catch(Newtonsoft.Json.JsonSerializationException jsonex)
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
                        bool Success =  await BL.UpdateItem(item, Repo, perfTimer);
                        if(Success)
                        {
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

        
    }
}