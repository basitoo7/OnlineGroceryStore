using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GroceryStore.Startup))]
namespace GroceryStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
