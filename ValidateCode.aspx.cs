using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ValidateCode_ValidateCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        validatedCode v = new validatedCode();
        string code = v.CreateVerifyCode();
        v.CreateImageOnPage(code, this.Context);
        Session["CheckCode"] = code;
    }
}
