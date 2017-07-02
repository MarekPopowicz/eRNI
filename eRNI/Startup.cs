using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eRNI.Startup))]
namespace eRNI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
