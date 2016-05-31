using Firehook.Hubs;
using Firehook.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Firehook.Controllers
{
    [Route("webhook")]
    public class WebhookController : ApiController
    {
        [HttpPost]
        public async Task Push()
        {
            string contentType = Request.Content.Headers.ContentType.MediaType;
            var payload = await Request.Content.ReadAsStringAsync();

            var headers = new Dictionary<string, string>();
            foreach(var header in Request.Headers)
            {
                headers[header.Key] = String.Join(", ", header.Value.ToArray());
            }

            // webhook payload
            var webhook = new WebhookPayload
            {
                Url = Request.RequestUri.ToString(),
                ContentType = contentType,
                Headers = headers,
                Body = payload
            };

            // notify all connected clients
            GlobalHost.ConnectionManager.GetHubContext<WebhookHub>().Clients.All.Received(webhook);
        }
    }
}
