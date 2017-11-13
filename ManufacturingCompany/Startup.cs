using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ManufacturingCompany.Startup))]
namespace ManufacturingCompany
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
