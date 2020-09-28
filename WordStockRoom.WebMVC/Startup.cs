using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WordStockRoom.WebMVC.Startup))]
namespace WordStockRoom.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
