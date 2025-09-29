using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Connect2Us3._01.Startup))]
namespace Connect2Us3._01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}