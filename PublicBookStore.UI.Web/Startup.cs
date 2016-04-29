using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PublicBookStore.UI.Web.Startup))]
namespace PublicBookStore.UI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
