using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
public partial class Default3 : System.Web.UI.Page
{
    OleDbConnection con;
    OleDbCommand cm;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btndangky_Click(object sender, EventArgs e)
    {
        if (txtemail.Text != "")
        {
            if ((txtemail.Text.LastIndexOf("@") != -1)&&(txtemail.Text.LastIndexOf("."))!=-1)
            {
                
                    if (txtpass.Text != "")
                    {
                        if (txtpass.Text == TextBox3.Text)
                        {
                            con = new OleDbConnection();
                            con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ToString();
                            con.Open();
                            cm = new OleDbCommand("insert into tbluser values ('" + txtemail.Text + "','" + txtpass.Text + "','admin','" + txthoteb.Text + "','" + txtdc.Text + "','" + txtdt.Text + "','" + txtcmt.Text + "','" + txtnganhang.Text + "','" + txtmabuudien.Text + "')", con);
                            cm.ExecuteNonQuery();
                            cm.Dispose();
                            con.Close();
                            Lblthongbao.Text = "Bạn đã đăng ký thành công ";
                        }
                        else Lblthongbao.Text = "Xác nhận mật khẩu chưa đúng";
                   
                }
                else Lblthongbao.Text = "Bạn chưa nhập vào mật khẩu";
            }
            else Lblthongbao.Text = "Email nhập chưa đúng";
        }
        else Lblthongbao.Text = "Bạn chưa nhập Email";
    }
}
