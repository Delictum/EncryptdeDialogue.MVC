using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EncryptedDialogue.MVC.Startup))]
namespace EncryptedDialogue.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
