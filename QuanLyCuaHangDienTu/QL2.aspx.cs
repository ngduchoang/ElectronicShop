using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;

namespace QuanLyCuaHangDienTu
{
    public partial class QL2 : System.Web.UI.Page
    {
        string connString;
        protected void Page_Load(object sender, EventArgs e)
        {
            connString = WebClass.getConnectionStringByName("sqlSConString");
            if (!Page.IsPostBack)
            {
                ddlHang.Items.Clear();
                ListItem li = new ListItem();
                li.Value = "-1";
                li.Text = "Chọn Hãng";
                ddlHang.Items.Add(li);

                try
                {
                    CommonCode.DataClasses.DataTool dataTool = new CommonCode.DataClasses.DataTool();
                    string sql = "SELECT Hang, XuatXu FROM HangXS";
                    SqlDataReader sqlreader = dataTool.execReader(connString, sql, null);
                    if (sqlreader != null && sqlreader.HasRows)
                    {
                        while (sqlreader.Read())
                        {
                            ListItem item = new ListItem();
                            item.Value = sqlreader.GetSqlString(0).ToString(); //ma hang
                            item.Text = sqlreader.GetSqlString(0).ToString();//ten hang
                            ddlHang.Items.Add(item);
                        }
                    }
                }
                catch (Exception exc)
                {
                    Response.Write(String.Format("Lỗi: {0}. Chi tiết: {1}", exc.Message, exc.StackTrace));
                }
                finally
                {
                }
           
                //đưa dữ liệu lên lưới dữ liệu gridview
               

            BindDataToGridview();
                gHangHoa.HeaderRow.Cells[0].Text = "Mã Sản Phầm";
                gHangHoa.HeaderRow.Cells[1].Text = "Tên Sản Phầm";
                gHangHoa.HeaderRow.Cells[2].Text = "Hãng ";
                gHangHoa.HeaderRow.Cells[3].Text = "Xuất Xứ";
            }
        }

        protected void btThem_Click(object sender, EventArgs e)
        {
         


           
            try
            {

                if (tbMaSP.Text.Trim() == "" || tbTenSP.Text.Trim() == "" || ddlHang.SelectedIndex == 0)
                {
                    lThongBao.Text = "Phải Nhập Dữ Liệu!";
                    return;
                }
                string sql = "INSERT INTO SanPham(MaSP, TenSP, Hang) VALUES(@id, @ten, @loai)";
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                sqlParams.Add(new SqlParameter("id", tbMaSP.Text.Trim()));
                sqlParams.Add(new SqlParameter("ten", tbTenSP.Text.Trim()));
                sqlParams.Add(new SqlParameter("loai", ddlHang.SelectedValue));
                CommonCode.DataClasses.DataTool dataTool = new CommonCode.DataClasses.DataTool();
                int cnt = dataTool.execInsUpdDel(connString, sql, sqlParams);
                lThongBao.Text = cnt.ToString() + " đã được thêm thành công!";
                //Đọc lại dữ liệu và đưa lên gridview
                BindDataToGridview();
            }
            catch (Exception exc)
            {
                lThongBao.Text= String.Format("<br/>Lỗi: {0}. <br/>Code: {1}",
                    exc.Message, exc.StackTrace);
            }
            finally
            {

            }
        }

        protected void btSua_Click(object sender, EventArgs e)
        {

            SqlConnection sqlcon = new SqlConnection(connString);
            SqlCommand sqlcom3 = new SqlCommand();
            try
            {
                if (tbMaSP.Text.Trim() == "" || tbTenSP.Text.Trim() == "" || ddlHang.SelectedIndex == 0)
                {
                    lThongBao.Text = "Phải nhập đủ dữ liệu!";
                    return;
                }
                sqlcon.Open();
                if (sqlcon.State == System.Data.ConnectionState.Open)
                {
                    sqlcom3.Connection = sqlcon;
                    sqlcom3.CommandText = "UPDATE SanPham SET TenSP = @pTenSP, Hang = @pHang WHERE MaSP = @pMaSP";
                    sqlcom3.Parameters.Add("pTenSP", System.Data.SqlDbType.NVarChar);
                    sqlcom3.Parameters["pTenSP"].Value = tbTenSP.Text.Trim();
                    sqlcom3.Parameters.Add("pHang", System.Data.SqlDbType.NVarChar);
                    sqlcom3.Parameters["pHang"].Value = ddlHang.SelectedValue;
                    sqlcom3.Parameters.Add("pMaSP", System.Data.SqlDbType.NVarChar);
                    sqlcom3.Parameters["pMaSP"].Value = tbMaSP.Text.Trim();
                    int cnt = sqlcom3.ExecuteNonQuery();
                    lThongBao.Text = String.Format("{0} sản phẩm đã được cập nhật thành công!", cnt);
                }
            }
            catch (Exception exc)
            {
                lThongBao.Text = String.Format("Lỗi: {0}. Chi tiết: {1}", exc.Message, exc.StackTrace);
            }
            finally
            {
                sqlcon.Close();
                sqlcom3.Dispose();
            }

        }
        protected void btXoa_Click(object sender, EventArgs e)
        {

            SqlConnection sqlcon = new SqlConnection(connString);
            SqlCommand sqlcom = new SqlCommand();

            lThongBao.Text = "";
            try
            {

                if (tbMaSP.Text.Trim() == "")
                {
                    lThongBao.Text = "Phải nhập đủ dữ liệu!";
                    return;
                }
                sqlcon.ConnectionString = connString;
                sqlcon.Open();
                if (sqlcon.State == System.Data.ConnectionState.Open)
                {
                    sqlcom.Connection = sqlcon;
                    sqlcom.CommandType = System.Data.CommandType.Text;
                    sqlcom.CommandText = "delete from SanPham where MaSP = @pMaSP";
                    sqlcom.Parameters.Add("pMaSP", System.Data.SqlDbType.NVarChar);
                    sqlcom.Parameters["pMaSP"].Value = tbMaSP.Text.Trim();
                    

                    int cnt = sqlcom.ExecuteNonQuery();
                    lThongBao.Text = String.Format("{0} sản phẩm đã được xóa thành công!", cnt);

                }
                else lThongBao.Text = "Kết nối thất bại!";
            }
            catch (Exception exc)
            {
                lThongBao.Text = "Kết nối thất bại! Lỗi: " + exc.Message + ". " + exc.StackTrace;

            }
            finally
            {
                if (sqlcon.State == System.Data.ConnectionState.Open)
                {
                    sqlcon.Close();
                    sqlcom.Dispose();
                }

            }
        
        }
        protected void gHangHoa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindDataToGridview(e.NewPageIndex);
        }


        public void BindDataToGridview(int pageIndex = 0)
        {
            //Dua du lieu  len GridView
            SqlConnection sqlconn3 = new SqlConnection();
            SqlCommand sqlcomm3 = new SqlCommand();

            try
            {
                sqlconn3.ConnectionString = connString;
                sqlconn3.Open();
                if (sqlconn3.State == System.Data.ConnectionState.Open)
                {
                    sqlcomm3.Connection = sqlconn3;
                    sqlcomm3.CommandType = System.Data.CommandType.Text;
                    sqlcomm3.CommandText = "SELECT        SanPham.MaSP,SanPham.TenSP,HangXS.Hang, HangXS.XuatXu "
                        + "FROM HangXS INNER JOIN "
                        + "SanPham ON HangXS.Hang = SanPham.Hang ";
                    DataTable dt = new DataTable("DSHang");
                    SqlDataAdapter da3 = new SqlDataAdapter(sqlcomm3);
                    da3.Fill(dt);
                    gHangHoa.DataSource = dt;
                    gHangHoa.PageIndex = pageIndex;
                    gHangHoa.DataBind();
                }
                else
                {
                }
            }
            catch (Exception exc)
            {
                Response.Write(string.Format("<br/>Lỗi: {0}. <br/>Code: {1}",
                    exc.Message, exc.StackTrace));
            }
            finally
            {

                sqlcomm3.Dispose();
                if (sqlconn3.State == System.Data.ConnectionState.Open)
                    sqlconn3.Close();
            }
        }


        protected void btTimKiem_Click(object sender, EventArgs e)
        {
            if (tbMaSP.Text.Trim() == ""
                && tbTenSP.Text.Trim() == ""
                && ddlHang.SelectedIndex == 0)
            {
                Response.Write("<br/><br/>Hãy nhập tối thiểu 1 điều kiện tìm kiếm!!! <br/><br/>");
                return;

            }

            string where = " WHERE 1=1 ";
            if (tbMaSP.Text.Trim() != "")
                where = string.Format(" {0} AND MaSP like N'%{1}%' ", where, tbMaSP.Text.Trim());
            if (tbTenSP.Text.Trim() != "")
                where = string.Format(" {0} AND TenSP like N'%{1}%'", where, tbTenSP.Text.Trim());
            if (ddlHang.SelectedIndex != 0)
                where = string.Format(" {0} AND SanPham.Hang like N'%{1}%' ", where, ddlHang.SelectedValue);


            //dua du lieu len GridView
            SqlConnection sqlconn4 = new SqlConnection();
            SqlCommand sqlcomm4 = new SqlCommand();

            try
            {
                sqlconn4.ConnectionString = connString;
                sqlconn4.Open();
                if (sqlconn4.State == System.Data.ConnectionState.Open)
                {
                    sqlcomm4.Connection = sqlconn4;
                    sqlcomm4.CommandType = System.Data.CommandType.Text;
                    sqlcomm4.CommandText = string.Format("SELECT        SanPham.MaSP,SanPham.TenSP,HangXS.Hang, HangXS.XuatXu  "
                        + "FROM HangXS INNER JOIN "
                        + "SanPham ON HangXS.Hang = SanPham.Hang {0} ", where);

                    DataTable dt = new DataTable("DSHang");
                    SqlDataAdapter da3 = new SqlDataAdapter(sqlcomm4);
                    da3.Fill(dt);
                    gHangHoa.DataSource = dt;
                    gHangHoa.DataBind();
                }
                else
                {
                }
            }
            catch (Exception exc)
            {
                Response.Write(string.Format("<br/>Lỗi: {0}. <br/>Code: {1}",
                    exc.Message, exc.StackTrace));
            }
            finally
            {

                sqlcomm4.Dispose();
                if (sqlconn4.State == System.Data.ConnectionState.Open)
                    sqlconn4.Close();
            }
        }

       
    }
}