using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Firehook.App_Start.Startup))]
namespace Firehook.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}