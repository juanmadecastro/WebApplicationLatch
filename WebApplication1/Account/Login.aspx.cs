using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

namespace WebApplication1.Account
{
    public partial class Login : System.Web.UI.Page
    {
        LatchSDK.Latch instanciaLatch;

        protected void Page_Load(object sender, EventArgs e)
        {
            instanciaLatch = new LatchSDK.Latch(ConfigurationManager.AppSettings["appId"], ConfigurationManager.AppSettings["appSecret"]);

            string username = "";
            if (this.Context.User.Identity.IsAuthenticated)
                username = this.Context.User.Identity.Name;

            if (IsLatchOperationOpen(username, ConfigurationManager.AppSettings["Login"]))
            {
                lb_login.Text = "El LATCH de la operación LOGIN está abierto";
                lb_login.ForeColor = System.Drawing.Color.Blue;
                lb_login.Font.Bold = true;
                this.LoginUser.Enabled = true;
            }
            else
            {
                lb_login.Text = "El LATCH de la operación LOGIN está cerrado";
                lb_login.ForeColor = System.Drawing.Color.Red;
                lb_login.Font.Bold = true;
                this.LoginUser.Enabled = false;
            }

            RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        protected void bt_login_Click(object sender, EventArgs e)
        {
            DataSet1TableAdapters.TB_USUARIOSTableAdapter ta = new DataSet1TableAdapters.TB_USUARIOSTableAdapter();
            DataSet1.TB_USUARIOSDataTable tb = new DataSet1.TB_USUARIOSDataTable();
            ta.FillUsuarioByPassword(tb, LoginUser.UserName, LoginUser.Password);
            if (tb.Rows.Count > 0)
            {
                LoginUser.FailureText = "Usuario validado correctamente.";
                
            }
            else {
                LoginUser.FailureText = "Usuario o password incorrecta.";
            }
        }

        private bool IsLatchOperationOpen(string username, string operationId)
        {
            bool isOpen = true;            
            try
            {
                if (string.IsNullOrEmpty(lb_account_id.Text))
                {
                    lb_account_id.Text = ConfigurationManager.AppSettings["latchAccountId"];               
                    LatchSDK.Latch latchComm = new LatchSDK.Latch(ConfigurationManager.AppSettings["latchAppId"], ConfigurationManager.AppSettings["latchAppSecret"]);
                    LatchSDK.LatchResponse response = latchComm.OperationStatus(lb_account_id.Text, operationId);
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
