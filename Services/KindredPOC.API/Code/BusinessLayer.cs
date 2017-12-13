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

namespace KindredPOC.API.Code
{
    public class BusinessLayer
    {
        private static string key = TelemetryConfiguration.Active.InstrumentationKey = System.Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY", EnvironmentVariableTarget.Process);
        private static TelemetryClient telemetry = new TelemetryClient() { InstrumentationKey = key };
        public static async Task<HttpResponseMessage> UpdateItem(HttpRequestMessage req, TraceWriter log, IDataRepository Repo, System.Diagnostics.Stopwatch perfTimer)
        {
            try
            {
                string data = await req.Content.ReadAsStringAsync();
                Models.Item item = JsonConvert.DeserializeObject<Models.Item>(data);
                if (item != null)
                {
                    try
                    {
                        var newitem = await Repo.UpdateItemAsync(item);
                        EventTelemetry newevent = new EventTelemetry { Name = "UpdateItem" };
                        newevent.Metrics.Add("UpdateElapsed", perfTimer.ElapsedMilliseconds);
                        newevent.Properties.Add("UpdatedItem", JsonConvert.SerializeObject(newitem, Formatting.Indented));
                        telemetry.TrackEvent(newevent);
                        telemetry.TrackMetric("UpdateElapsed", perfTimer.ElapsedMilliseconds);
                    }
                    catch (ArgumentException Aex)
                    {
                        //Id was not found
                        return req.CreateErrorResponse(HttpStatusCode.BadRequest, Aex);
                    }
                    catch (Exception ex)
                    {
                        return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                    }
                }
                return req.CreateResponse(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                log.Info($"Fail Post {ex.Message}");
                return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        public static async Task<HttpResponseMessage> DeleteItem(HttpRequestMessage req, TraceWriter log, IDataRepository Repo, System.Diagnostics.Stopwatch perfTimer)
        {
            try
            {
                string Id = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "Id", true) == 0).Value;

                // Get request body
                dynamic data = await req.Content.ReadAsAsync<object>();

                // Set name to query string or body data
                Id = Id ?? data?.Id;

                if (Id == null)
                {
                    return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass an Id on the query string or in the request body");
                }
                else
                {
                    Models.Item item = await Repo.GetItemAsync(Id);
                    if (item != null)
                    {
                        bool success = false;
                        try
                        {
                            success = await Repo.DeleteItemAsync(Id);
                            EventTelemetry newevent = new EventTelemetry { Name = "DeleteItem" };
                            newevent.Metrics.Add("SaveElapsed", perfTimer.ElapsedMilliseconds);
                            newevent.Properties.Add("DeletedItem", JsonConvert.SerializeObject(item, Formatting.Indented));
                            telemetry.TrackEvent(newevent);
                            telemetry.TrackMetric("DeleteElapsed", perfTimer.ElapsedMilliseconds);
                        }
                        catch (ArgumentException Aex)
                        {
                            //Id was not found
                            return req.CreateErrorResponse(HttpStatusCode.BadRequest, Aex);
                        }
                        catch (Exception ex)
                        {
                            log.Info($"Bad Item Post Object");
                            return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                        }
                        if (success)
                        {
                            return req.CreateResponse(HttpStatusCode.OK);
                        }
                        else
                        {
                            return req.CreateResponse(HttpStatusCode.InternalServerError, "Delete Not Successful");
                        }
                    }
                    else
                    {
                        return req.CreateResponse(HttpStatusCode.NotFound, $"{Id} could not be found in database");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info($"Fail Delete {ex.Message}");
                return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        public static async Task<HttpResponseMessage> SaveItem(HttpRequestMessage req, TraceWriter log, IDataRepository Repo, System.Diagnostics.Stopwatch perfTimer)
        {
            try
            {
                string data = await req.Content.ReadAsStringAsync();
                Models.Item item = JsonConvert.DeserializeObject<Models.Item>(data);

                if (item != null)
                {
                    var saveditem = await Repo.CreateItemAsync(item);
                    EventTelemetry newevent = new EventTelemetry { Name = "GetItems" };
                    newevent.Metrics.Add("SaveElapsed", perfTimer.ElapsedMilliseconds);
                    newevent.Properties.Add("PostIn", JsonConvert.SerializeObject(item, Formatting.Indented));
                    newevent.Properties.Add("PostReturn", JsonConvert.SerializeObject(saveditem, Formatting.Indented));
                    telemetry.TrackEvent(newevent);
                    telemetry.TrackMetric("SaveElapsed", perfTimer.ElapsedMilliseconds);
                }
                else
                {
                    log.Info($"Bad Item Post Object");

                    return req.CreateResponse(HttpStatusCode.BadRequest, "Could not read submitted Item");
                }

                return req.CreateResponse(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                log.Info($"Fail Post {ex.Message}");
                return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        public static async Task<HttpResponseMessage> GetItem(HttpRequestMessage req, TraceWriter log, IDataRepository Repo, System.Diagnostics.Stopwatch perfTimer)
        {
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
                var items = Repo.GetItems(takeint, skipint);
                EventTelemetry newevent = new EventTelemetry { Name = "GetItems" };
                newevent.Metrics.Add("GetElapsed", perfTimer.ElapsedMilliseconds);
                newevent.Properties.Add("GetReturn", JsonConvert.SerializeObject(items, Formatting.Indented));
                telemetry.TrackEvent(newevent);
                telemetry.TrackMetric("GetElapsed", perfTimer.ElapsedMilliseconds);
                return req.CreateResponse(HttpStatusCode.OK, items, "application/json");
            }
            catch (Exception ex)
            {
                log.Info($"Fail get");
                telemetry.TrackException(ex);//Is this done by framework?  look for duplicate entries in log
                return req.CreateErrorResponse(HttpStatusCode.BadGateway, ex);
            }
        }
    }
}
