using Microsoft.Owin;
using NoteTaker.Web;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace NoteTaker.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
