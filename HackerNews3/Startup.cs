using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HackerNews3.Startup))]
namespace HackerNews3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
