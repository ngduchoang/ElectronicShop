using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyCuaHangDienTu
{
    public partial class DANGXUAT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Xoá tất cả thông tin Session liên quan
            Session["id"] = "";
            Session["hoLot"] = "";
            Session["ten"] = "";
            Session["role"] = "";
            Response.Write("<script>alert('Bạn đã đăng xuất thành công!')</script>");
            Response.Redirect("TrangChuu.aspx");
        }
    }
}