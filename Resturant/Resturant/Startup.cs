using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Resturant.Startup))]
namespace Resturant
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
