using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firehook.TestClient
{
    class Program
    {
        private static HubConnection _hubConnection = null;

        static void Main(string[] args)
        {
            _hubConnection = new HubConnection("https://firehook85f3.azurewebsites.net/");
            _hubConnection.StateChanged += HubConnection_StateChanged;
            IHubProxy webhookHubProxy = _hubConnection.CreateHubProxy("WebhookHub");
            webhookHubProxy.On<WebhookPayload>("Received", webhook =>
            {
                Console.WriteLine(webhook.Url);
                Console.WriteLine();
                foreach(var header in webhook.Headers)
                {
                    Console.WriteLine(header.Key + "=" + header.Value);
                }
                Console.WriteLine();
                Console.WriteLine(webhook.Body);
            });

            _hubConnection.Start().Wait();
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }

        private static void HubConnection_StateChanged(StateChange obj)
        {
            if (obj.NewState == ConnectionState.Connected)
            {
                Console.WriteLine("Connected via '{0}' protocol", _hubConnection.Transport.Name);
            }
        }
    }
}
