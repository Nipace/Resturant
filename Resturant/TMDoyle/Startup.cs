using Microsoft.Owin;
using Owin;
using Unity;

using TMDoyle.Service;
using TMDoyle.CrossCutting.Infrastructure;

[assembly: OwinStartupAttribute(typeof(TMDoyle.Startup))]
namespace TMDoyle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            var container = new UnityContainer();
            UnityConfig.RegisterTypes(container);

            //Automapper Initialization
            AutoMapperConfiguration.Configure();
            Log.InitializeLog4Net();
        }
    }
}
