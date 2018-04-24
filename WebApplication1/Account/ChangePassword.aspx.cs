using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;
using System.Security.Principal;
using System.IO;

namespace WebApplication1.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        LatchSDK.Latch instanciaLatch;
        LatchSDK.LatchResponse respLogin;
        LatchSDK.LatchResponse respChangePassword;
        LatchSDK.LatchResponse pairResponse;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
               //{

            instanciaLatch = new LatchSDK.Latch(ConfigurationManager.AppSettings["latchAppId"], ConfigurationManager.AppSettings["latchAppSecret"]);
                //instanciaLatch = new LatchSDK.Latch(ConfigurationManager.AppSettings["appId"], ConfigurationManager.AppSettings["appSecret"]);

                lb_account_id.Text = ConfigurationManager.AppSettings["latchAccountId"];



                string username = "";
                if (this.Context.User.Identity.IsAuthenticated)
                    username = this.Context.User.Identity.Name;

                if (IsLatchOperationOpen(username, ConfigurationManager.AppSettings["ChangePassword"]))
                {
                    lb_changepassword.Text = "El LATCH de la operación Cambio de contraseña está abierto";
                    lb_changepassword.ForeColor = System.Drawing.Color.Blue;
                    lb_changepassword.Font.Bold = true;
                    this.ChangeUserPassword.Enabled = true;
                }
                else
                {
                    lb_changepassword.Text = "El LATCH de la operación Cambio de contraseña está cerrado";
                    lb_changepassword.ForeColor = System.Drawing.Color.Red;
                    lb_changepassword.Font.Bold = true;
                    this.ChangeUserPassword.Enabled = false;
                }
        }

        private bool IsLatchOperationOpen(string username, string operationId)
        {
            bool isOpen = true;
            try
            {
                string accountId = lb_account_id.Text;//StorageManager.ReadAccount(username);
                if (!string.IsNullOrEmpty(accountId))
                {
                    LatchSDK.Latch latchComm = new LatchSDK.Latch(ConfigurationManager.AppSettings["latchAppId"], ConfigurationManager.AppSettings["latchAppSecret"]);
                    LatchSDK.LatchResponse response = latchComm.OperationStatus(accountId, operationId);
                    //isOpen = response.Error == null && ((string)(((Dictionary<string, object>)((Dictionary<string, object>)response.Data["operations"])[operationId])["status"])).Equals("on", StringComparison.InvariantCultureIgnoreCase);

                    if (response.Error == null && response.Data["operations"] is Dictionary<string, object>)
                    {
                        Dictionary<string, object> operations = response.Data["operations"] as Dictionary<string, object>;
                        if (operations[operationId] is Dictionary<string, object>)
                        {
                            Dictionary<string, object> currentOperation = operations[operationId] as Dictionary<string, object>;
                            isOpen = (currentOperation["status"] as string).Equals("on", StringComparison.InvariantCultureIgnoreCase);
                        }
                    }

                }

            }
            catch (Exception)
            {
            }
            return isOpen;
        }
    }
}
