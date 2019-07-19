using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//dữ liệu, CSDL...
using System.Data.SqlClient;
using System.Data;
//MD5
using System.Security.Cryptography;

namespace QuanLyCuaHangDienTu
{
    public partial class DANGNHAP : System.Web.UI.Page
    {
        string conString;
        protected void Page_Load(object sender, EventArgs e)
        {
            conString = WebClass.getConnectionStringByName("sqlSConString");

        }


        protected void btDN_Click(object sender, EventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection();
            SqlCommand sqlcom2 = new SqlCommand();
            try
            {
                sqlcon.ConnectionString = conString;
                sqlcon.Open();
                if (sqlcon.State == System.Data.ConnectionState.Open)
                {
                    sqlcom2.Connection = sqlcon;
                    sqlcom2.CommandType = System.Data.CommandType.Text;
                    sqlcom2.CommandText = "SELECT id, hoLot, ten, matKhau, vaiTro FROM NguoiDung WHERE tenDangNhap = @login";
                    sqlcom2.Parameters.AddWithValue("@login", tbTenDangNhap.Text.Trim());
                    SqlDataReader sqlreader = sqlcom2.ExecuteReader();
                
                    Session["role"] = "";
                    MD5 md5 = MD5.Create();
                    if (sqlreader.HasRows)
                    {
                        while (sqlreader.Read())
                        {
                            
                            if (CommonCode.MD5Hash.VerifyMd5Hash(md5, tbMatKhau.Text.Trim(), sqlreader.GetString(3)))
                            {
                                Session["id"] = sqlreader.GetInt32(0);
                                Session["hoLot"] = sqlreader.GetString(1);
                                Session["ten"] = sqlreader.GetString(2);
                                Session["role"] = sqlreader.GetString(4);
                                //Chuyển đến các chức năng theo vai trò
                                switch ((string)Session["role"])
                                {
                                    case "QL":
                                        Response.Redirect("QL2.aspx");
                                        break;
                                    case "NV":
                                        Response.Redirect("NV1.aspx");
                                        break;
                                    default:
                                        ;
                                        break;
                                }
                            }
                        }
                    }
                    if ((string)Session["role"] == "")
                    {
                        lThongBao.Text = "Đăng nhập KHÔNG thành công";
                    }

                }
            }
            catch (Exception exc)
            {
                Response.Write(String.Format("Lỗi: {0}. Chi tiết: {1}", exc.Message, exc.StackTrace));
            }
            finally
            {
                sqlcon.Close();
                sqlcom2.Dispose();
            }
        }
    }
}