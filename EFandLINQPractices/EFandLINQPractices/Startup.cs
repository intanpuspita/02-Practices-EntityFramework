using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EFandLINQPractices.Startup))]
namespace EFandLINQPractices
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
