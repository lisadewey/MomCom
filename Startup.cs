using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MomCom.Startup))]
namespace MomCom
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
