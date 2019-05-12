using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if ((tbusername.Text == "") || (tbpsw.Text == ""))
        {
            Response.Write(@"<script>alert('用户名与密码不能为空!');</script>");
        }
        else
        {
            SqlConnection con = db.CreateConnection();
            con.Open();
            string strSql = "select password  from Logindata where username='" + tbusername.Text + "'";
            SqlCommand cmd = new SqlCommand(strSql, con);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(strSql, con);
            da.Fill(ds, "uername");
            try
            {
                if (tbpsw.Text == ds.Tables[0].Rows[0].ItemArray[0].ToString().Trim())
                {
                    string curuser = tbusername.Text;
                    Response.Write(@"<script>alert('登录成功,欢迎你!');</script>");
                    Response.Redirect("HelloWorld.html");
                }
                else
                {
                    Response.Write(@"<script>alert('用户名或者密码错误!');</script>");
                }
            }
            catch
            {
                Response.Write(@"<script>alert('Sorry!你输入的用户名不存在!');</script>");


            }
            con.Close();
        }
    }
    protected void Reset_Click(object sender, EventArgs e)
    {
        Reset.Attributes.Add("onclick", "{javascript:form1.reset();return false;}");
    }
}
