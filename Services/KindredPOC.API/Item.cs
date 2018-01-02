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
                    return await BL.SaveItem(req, log, Repo, perfTimer);
                    break;

                case "DELETE":
                    return await BL.DeleteItem(req, log, Repo, perfTimer);
                    break;

                case "PUT":
                    return await BL.UpdateItem(req, log, Repo, perfTimer);
                    break;

                default:
                    return req.CreateResponse(HttpStatusCode.InternalServerError, "Action Not Identified");
                    break;
            }
        }

        
    }
}