using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(News_MVC.Startup))]
namespace News_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
