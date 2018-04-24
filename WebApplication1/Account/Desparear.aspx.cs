using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.Web.Security;
using System.Configuration;

namespace WebApplication1.Account
{
    public partial class Desparear : System.Web.UI.Page
    {
        LatchSDK.Latch instanciaLatch;

        protected void Page_Load(object sender, EventArgs e)
        {
            instanciaLatch = new LatchSDK.Latch(ConfigurationManager.AppSettings["latchAppId"], ConfigurationManager.AppSettings["latchAppSecret"]);
        }

        protected void bt_desparear_Click(object sender, EventArgs e)
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = Context.Request.Cookies[cookieName];

            if (authCookie == null)
            {
                return;
            }
            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch
            {
                return;
            }
            if (authTicket == null)
            {
                return;
            }
            string[] roles = authTicket.UserData.Split(new char[] { '|' });
            FormsIdentity id = new FormsIdentity(authTicket);
            GenericPrincipal principal = new GenericPrincipal(id, roles);


            //LatchSDK.LatchResponse unpairResponse = instanciaLatch.Unpair(HttpContext.Current.User.Identity.Name);

            LatchSDK.LatchResponse unpairResponse = instanciaLatch.Unpair(ConfigurationManager.AppSettings["latchAccountId"]);
            if (unpairResponse.Error == null)
            {
                lb_error_account_id.Text = "La aplicación se ha despareado.";                
            }
            else
            {
                lb_error_account_id.Text = "Se ha producido un error en el proceso de despareado.";
            }

        }
    }
}