using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Account
{
    public partial class Register : System.Web.UI.Page
    {
        DataSet1TableAdapters.TB_USUARIOSTableAdapter ta_usuarios = new DataSet1TableAdapters.TB_USUARIOSTableAdapter();
        DataSet1.TB_USUARIOSDataTable tb_usuarios = new DataSet1.TB_USUARIOSDataTable();
        int idusuario = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);

            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            if (String.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }
            try{            
                idusuario = (int)ta_usuarios.SELECT_IDUSUARIO();
            }catch(Exception ex){
                
            }
            ta_usuarios.Insert(idusuario + 1, RegisterUser.UserName, RegisterUser.Password, "", RegisterUser.Email);
            Response.Redirect(continueUrl);
        }

    }
}
