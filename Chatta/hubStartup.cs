using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Chatta.hubStartup))]
namespace Chatta
{
    public partial class hubStartup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
