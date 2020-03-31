using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NoiThatAdmin.Startup))]
namespace NoiThatAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
