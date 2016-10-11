using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutoStockBroker.Startup))]
namespace AutoStockBroker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
