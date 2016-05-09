using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mugshots.Startup))]
namespace mugshots
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
