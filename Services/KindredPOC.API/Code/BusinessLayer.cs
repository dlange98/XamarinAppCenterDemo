using KindredPOC.API.Models;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace KindredPOC.API.Code
{
    public class BusinessLayer
    {
        private string key;
        private TelemetryClient telemetry;
        private Models.IDataRepository _repo;

        public BusinessLayer(Models.IDataRepository Repository)
        {
            if (Repository == null) throw new ArgumentNullException("Repository must be supplied");
            _repo = Repository;
            key = "NULL";
            telemetry = new TelemetryClient() { InstrumentationKey = key };
        }

        public async Task<bool> UpdateItem(Models.Item item, System.Diagnostics.Stopwatch perfTimer)
        {
            return await UpdateItem(item, _repo, perfTimer);
        }

        public async Task<bool> UpdateItem(Models.Item item, IDataRepository Repo, System.Diagnostics.Stopwatch perfTimer)
        {
            if (item == null)
            {
                //passing in a null item should throw an exception and be handled at a higher level
                ArgumentNullException nex = new ArgumentNullException("Must supply item value");
                telemetry.TrackException(nex);
                throw nex;
            }
            if (Repo == null)
            {
                //passing in a null item should throw an exception and be handled at a higher level
                ArgumentNullException nex = new ArgumentNullException("Must supply repository");
                telemetry.TrackException(nex);
                throw nex;
            }
            if (perfTimer == null)
            {
                //passing in a null item should throw an exception and be handled at a higher level
                ArgumentNullException nex = new ArgumentNullException("Must supply perf");
                telemetry.TrackException(nex);
                throw nex;
            }
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
                telemetry.TrackException(Aex);
                //Id was not found
                return false; // or re throw if error handling at higher level
            }
            catch (Exception ex)
            {
                telemetry.TrackException(ex);
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteItem(string Id, System.Diagnostics.Stopwatch perfTimer)
        {
            return await DeleteItem(Id, _repo, perfTimer);
        }

        public async Task<bool> DeleteItem(string Id, IDataRepository Repo, System.Diagnostics.Stopwatch perfTimer)
        {
            if (Id == null || Id.Length == 0)
            {
                //passing in a null item should throw an exception and be handled at a higher level
                ArgumentNullException nex = new ArgumentNullException("Must supply item value");
                telemetry.TrackException(nex);
                throw nex;
            }
            if (Repo == null)
            {
                //passing in a null item should throw an exception and be handled at a higher level
                ArgumentNullException nex = new ArgumentNullException("Must supply repository");
                telemetry.TrackException(nex);
                throw nex;
            }
            if (perfTimer == null)
            {
                //passing in a null item should throw an exception and be handled at a higher level
                ArgumentNullException nex = new ArgumentNullException("Must supply perf");
                telemetry.TrackException(nex);
                throw nex;
            }
            try
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
                        return false;
                    }
                    catch (Exception ex)
                    {
                        telemetry.TrackException(ex);
                        return false;
                    }
                    if (success)
                    {
                        return true;
                    }
                    else
                    {
                        EventTelemetry newevent = new EventTelemetry { Name = "DeleteFail" };
                        newevent.Properties.Add("ItemId", Id);
                        newevent.Properties.Add("FailReason", "Unknown - Repo reports Failure");
                        telemetry.TrackEvent(newevent);
                        return false;
                    }
                }
                else
                {
                    EventTelemetry newevent = new EventTelemetry { Name = "DeleteFail" };
                    newevent.Properties.Add("ItemId", Id);
                    newevent.Properties.Add("FailReason", $"No item with Id {Id} returned from data store");
                    telemetry.TrackEvent(newevent);
                    return false;
                }
            }
            catch (Exception ex)
            {
                telemetry.TrackException(ex);
                throw ex;
            }
        }

        public async Task<Models.Item> SaveItem(Models.Item item, System.Diagnostics.Stopwatch perfTimer)
        {
            return await SaveItem(item, _repo, perfTimer);
        }

        public async Task<Models.Item> SaveItem(Models.Item item, IDataRepository Repo, System.Diagnostics.Stopwatch perfTimer)
        {
            //Validate inputs
            if (item == null)
            {
                //passing in a null item should throw an exception and be handled at a higher level
                ArgumentNullException nex = new ArgumentNullException("Must supply item value");
                telemetry.TrackException(nex);
                throw nex;
            }
            if (Repo == null)
            {
                //passing in a null item should throw an exception and be handled at a higher level
                ArgumentNullException nex = new ArgumentNullException("Must supply repository");
                telemetry.TrackException(nex);
                throw nex;
            }
            if (perfTimer == null)
            {
                //passing in a null item should throw an exception and be handled at a higher level
                ArgumentNullException nex = new ArgumentNullException("Must supply perf");
                telemetry.TrackException(nex);
                throw nex;
            }
            try
            {
                    var saveditem = await Repo.CreateItemAsync(item);
                    EventTelemetry newevent = new EventTelemetry { Name = "GetItems" };
                    newevent.Metrics.Add("SaveElapsed", perfTimer.ElapsedMilliseconds);
                    newevent.Properties.Add("PostIn", JsonConvert.SerializeObject(item, Formatting.Indented));
                    newevent.Properties.Add("PostReturn", JsonConvert.SerializeObject(saveditem, Formatting.Indented));
                    telemetry.TrackEvent(newevent);
                    telemetry.TrackMetric("SaveElapsed", perfTimer.ElapsedMilliseconds);
                return saveditem;
            }
            catch (Exception ex)
            {
                telemetry.TrackException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// validates inputs to data layer and tests for conditions/exceptions
        /// if take is null or take == 0 then take is ignored.
        /// skip is ignored if take is not > 0
        /// </summary>
        /// <param name="Repo"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Models.Item>> GetDataRepoItems(IDataRepository Repo, int take, int skip)
        {
            if (take < 0) throw new System.ArgumentOutOfRangeException("take must be non-negative");
            if (take > 0 && skip < 0) throw new System.ArgumentOutOfRangeException("skip must be non-negative if take is defined");
            return await Task<IEnumerable<Models.Item>>.Factory.StartNew(() => { return Repo.GetItems(take, skip); });
        }
        /// <summary>
        /// GetItems business layer object.  Repo is injected in constructor of class
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <param name="perfTimer"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Models.Item>> BL_GetItems(System.Diagnostics.Stopwatch perfTimer, int take, int skip)
        {
            return await GetItems(_repo, perfTimer, take, skip);
        }

        /// <summary>
        /// validates request elements and tests for conditions/exceptions
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <param name="Repo"></param>
        /// <param name="perfTimer"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Models.Item>> GetItems(IDataRepository Repo, System.Diagnostics.Stopwatch perfTimer, int take = 0, int skip = 0)
        {
            if (Repo == null)
            {
                //passing in a null item should throw an exception and be handled at a higher level
                ArgumentNullException nex = new ArgumentNullException("Must supply repository");
                telemetry.TrackException(nex);
                throw nex;
            }
            if (perfTimer == null)
            {
                //passing in a null item should throw an exception and be handled at a higher level
                ArgumentNullException nex = new ArgumentNullException("Must supply perf");
                telemetry.TrackException(nex);
                throw nex;
            }
            try
            {
                var items = await GetDataRepoItems(_repo, take, skip);
                EventTelemetry newevent = new EventTelemetry { Name = "GetItems" };
                newevent.Metrics.Add("GetElapsed", perfTimer.ElapsedMilliseconds);
                newevent.Properties.Add("GetReturn", JsonConvert.SerializeObject(items, Formatting.Indented));
                telemetry.TrackEvent(newevent);
                telemetry.TrackMetric("GetElapsed", perfTimer.ElapsedMilliseconds);
                return items;
            }
            catch (Exception ex)
            {
                telemetry.TrackException(ex);
                throw;
            }
        }
    }
}