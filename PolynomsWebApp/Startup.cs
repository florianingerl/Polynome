using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PolynomsWebApp.Startup))]
namespace PolynomsWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
