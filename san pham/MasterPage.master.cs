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

    public partial class MasterPage : System.Web.UI.MasterPage
    {
        TimKiemData tk;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            lblslkhach.Text = Application.Contents["sltruycap"].ToString();
            lbltructuyen.Text = Application.Contents["sltructuyen"].ToString();
          if (Page.IsPostBack==false)
            {
             
              
                ado.ketNoi();
                OleDbDataAdapter da = new OleDbDataAdapter("Select * From tblhang",ado.con);
                DataTable dt = new DataTable();
                da.Fill(dt);                           
                MenuItem tmp;                             
              
    foreach (DataRow dr in dt.Rows)
        
    {
        
        tmp = new MenuItem(dr[1].ToString(), dr[0].ToString(), "~/Images/blue_arrow.gif");
        tmp.NavigateUrl = "sanpham.aspx?mahang=" +dr[0].ToString();
        tmp.SeparatorImageUrl = "~/Images/nganmenu.gif";
        Menu1.Items.Add(tmp);
       

       
    }
    OleDbDataAdapter da1 = new OleDbDataAdapter("Select * From timkiem", ado.con);
    DataTable dt1 = new DataTable();
    da1.Fill(dt1); MenuItem tmp1;      
    foreach (DataRow dr1 in dt1.Rows)
    {
        tmp1 = new MenuItem(dr1[2].ToString(), dr1[0].ToString(), "~/Images/blue_arrow.gif");
        tmp1.NavigateUrl = "timkiem.aspx?gia=" + dr1[0].ToString()+"&gia2=" + dr1[1].ToString();
        tmp1.SeparatorImageUrl = "~/Images/nganmenu.gif";
        Menu5.Items.Add(tmp1);

    }
    OleDbDataAdapter da2 = new OleDbDataAdapter("Select * From phongcach", ado.con);
    DataTable dt2 = new DataTable();
    da2.Fill(dt2); MenuItem tmp2;
    foreach (DataRow dr2 in dt2.Rows)
    {
        tmp2 = new MenuItem(dr2[1].ToString(), dr2[0].ToString(), "~/Images/blue_arrow.gif");
        tmp2.NavigateUrl = "phongcach.aspx?id=" + dr2[0].ToString();
        tmp2.SeparatorImageUrl = "~/Images/nganmenu.gif";
        Menu6.Items.Add(tmp2);

    }
    OleDbDataAdapter da3 = new OleDbDataAdapter("Select * From kieudang", ado.con);
    DataTable dt3 = new DataTable();
    da3.Fill(dt3); MenuItem tmp3;
    foreach (DataRow dr3 in dt3.Rows)
    {
        tmp3 = new MenuItem(dr3[1].ToString(), dr3[0].ToString(), "~/Images/blue_arrow.gif");
        tmp3.NavigateUrl = "kieudang.aspx?id=" + dr3[0].ToString();
        tmp3.SeparatorImageUrl = "~/Images/nganmenu.gif";
        Menu7.Items.Add(tmp3);

    }
   
            }
        }


        protected void btndangnhap_Click(object sender, EventArgs e)
        {
            
            
            
            ado.ketNoi();
            string query = "select * from tbluser where email='" + txtemail.Text + "' and matkhau='" + txtpass.Text + "'";
            ado.rd = ado.getData(query);
            if (ado.rd.Read())
            {
                FormsAuthentication.SetAuthCookie(txtemail.Text, true);
                Session["email"] = txtemail.Text;
                Session["matkhau"] = ado.rd[1].ToString();
                Session["quyen"] = ado.rd[2].ToString();
                Session["hoten"] = ado.rd[3].ToString();
                Session["diachi"] = ado.rd[4].ToString();
                Session["dienthoai"] = ado.rd[5].ToString();
                Session["soCMND"] = ado.rd[6].ToString();
                Session["mataikhoan"] = ado.rd[7].ToString();
                                   
                if (Session["quyen"].ToString() == "admin")
                    Response.Redirect("themqtri.aspx");
                else
                    Response.Redirect("thongtintaikhoan.aspx", true);
            }
            else
            {
                txtemail.Focus();
            }

            ado.rd.Dispose();
            ado.dongKetNoi();
        }
        
        protected void timKiem(object sender, CommandEventArgs e)
        {       
            {
                try
                {
                    Session["TuKhoa"] = TextBox3.Text;
                    Session["KQTimKiem"] = null;
                    tk = new TimKiemData();
                    DataTable tb;
                    tb = tk.setTable();
                    //string TinhTrang = "";
                    ado.ketNoi();
                    string query = "";
                    if (TextBox3.Text != "") 
                    {
                        query = "Select Anh, TenSP,Gia,soluong,'chitiet.aspx?tensp='+tensp as chitiet from tblSanPham where TenSP like '" + TextBox3.Text + "%'";
                    }              
                    ado.rd = ado.getData(query);
                    while (ado.rd.Read())
                    {
                        tb = tk.dienVaoBang(tb, ado.rd["Anh"].ToString(), ado.rd["TenSP"].ToString(), Convert.ToDouble(ado.rd["Gia"]), Convert.ToDouble(ado.rd["soluong"]), ado.rd["chitiet"].ToString());
                        
                    }
                    ado.dongKetNoi();
                    Session["KQTimKiem"] = tb;
                    Response.Redirect("timten.aspx", true);
                }
                catch (Exception ex)
                {
                    
                }
            }
        }
        
}
