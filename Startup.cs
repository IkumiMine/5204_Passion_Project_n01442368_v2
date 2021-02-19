using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_5204_Passion_Project_n01442368_v2.Startup))]
namespace _5204_Passion_Project_n01442368_v2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
