using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace WebApplication1.Account
{
    public partial class Parear : System.Web.UI.Page
    {
        LatchSDK.Latch instanciaLatch;
        LatchSDK.LatchResponse respLogin;
        LatchSDK.LatchResponse respChangePassword;
        LatchSDK.LatchResponse pairResponse;

        protected void Page_Load(object sender, EventArgs e)
        {
            instanciaLatch = new LatchSDK.Latch(ConfigurationManager.AppSettings["latchAppId"], ConfigurationManager.AppSettings["latchAppSecret"]);
        }

        protected void bt_parear_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_token.Text))
            {
                pairResponse = instanciaLatch.Pair(tb_token.Text);

                if (pairResponse.Error == null && pairResponse.Data.ContainsKey("accountId"))
                {
                    var accountId = pairResponse.Data["accountId"] as string;
                    ConfigurationManager.AppSettings["latchAccountId"] = accountId;
                    respChangePassword = instanciaLatch.CreateOperation(accountId, "ChangePassword", LatchSDK.Latch.FeatureMode.DISABLED, LatchSDK.Latch.FeatureMode.DISABLED);
                    respLogin = instanciaLatch.CreateOperation(accountId, "Login", LatchSDK.Latch.FeatureMode.DISABLED, LatchSDK.Latch.FeatureMode.DISABLED);
                    //storaremanager.saveaccount(User.Identity.Name, accountId);
                    //return RedirectToAction("Manage", "LatchPairSuccess");                    
                    //bt_parear.Visible = false;                    
                    lb_error_account_id.Text = "";
                    lb_account_id.Text = Environment.NewLine + DateTime.Now.ToString() + " Emparejamiento: " + accountId + Environment.NewLine;                    
                }
                else
                {
                    //ModelState.AddModelError("", pairResponse.Error.Message);
                    //return View("Manage");
                    lb_error_account_id.Text = "Error al parear la aplicación.";
                }
            }

        }
    }
}