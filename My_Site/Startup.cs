using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(My_Site.Startup))]
namespace My_Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
