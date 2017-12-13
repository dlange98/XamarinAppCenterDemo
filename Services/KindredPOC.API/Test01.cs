using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using Newtonsoft.Json.Linq;

namespace KindredPOC.API
{
    public static class Test01
    {
        [FunctionName("Test01")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            try
            {
                return req.CreateResponse(HttpStatusCode.OK, new { id = Guid.NewGuid(), name = "TestName", Headers = req.Headers, data = "Test Data" });
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                req.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);

            }
            return req.CreateResponse(HttpStatusCode.BadRequest, "Something was bad");
        }
    }
}
