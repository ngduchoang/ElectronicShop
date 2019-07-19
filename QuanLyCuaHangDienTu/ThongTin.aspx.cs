using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;

namespace QuanLyCuaHangDienTu
{
    public partial class ThongTin : System.Web.UI.Page
    {
        string connString;
        protected void Page_Load(object sender, EventArgs e)
        {
            connString = WebClass.getConnectionStringByName("sqlSConString");
            if (IsPostBack) return;

             
            BindDataToGridview();
            //gHangHoa.HeaderRow.Cells[0].Text = "Mã Sản Phầm";
            //gHangHoa.HeaderRow.Cells[1].Text = "Tên Sản Phầm";
            //gHangHoa.HeaderRow.Cells[2].Text = "Hãng ";
            //gHangHoa.HeaderRow.Cells[3].Text = "Xuất Xứ";
        }

       

      

     
        protected void gHangHoa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindDataToGridview(e.NewPageIndex);
        }

        // IndependentMethods
        /// <summary>
        /// BindDataToGridview dùng chung để đọc dữ liệu từ CSDL theo lệnh 
        /// SELECT và đưa lên dataGridview
        /// </summary>
        /// <param name="pageIndex">số thứ tự (chỉ mục) của trang dữ liệu được chọn, mặc định là 0</param>
        private void BindDataToGridView(int pageIndex, string sortExpression, string sortDirection = "ASC")
        {
            SqlConnection sqlSconn = new SqlConnection();
            SqlCommand sqlScomm4 = new SqlCommand();
            try
            {

                sqlSconn.ConnectionString = connString;
                sqlSconn.Open();
                if (sqlSconn.State == System.Data.ConnectionState.Open)
                {
                    //Doc cac ban ghi tu CSDL va the hien tren luoi du lieu
                    gHangHoa.DataSource = null;
                    DataTable dt = new DataTable();
                    sqlScomm4.Connection = sqlSconn;
                    sqlScomm4.CommandType = CommandType.Text;
                    sqlScomm4.CommandText = "SELECT        SanPham.MaSP,SanPham.TenSP,HangXS.Hang, HangXS.XuatXu "
                        + "FROM HangXS INNER JOIN "
                        + "SanPham ON HangXS.Hang = SanPham.Hang ORDER BY SanPham.MaSP DESC";
                    SqlDataAdapter da = new SqlDataAdapter(sqlScomm4);
                    da.Fill(dt);
                    //dt.Columns[0].Caption = "Tên hàng";
                    //dt.Columns["maTheLoai"].Caption = "Thể loại";
                    gHangHoa.DataSource = dt;
                    //sắp xếp
                    dt.DefaultView.Sort = String.Format("{0} {1}", sortExpression, sortDirection);
                    //dung cai nay de phan trang
                    if (pageIndex >= 0)
                        gHangHoa.PageIndex = pageIndex;

                    gHangHoa.DataBind();
                    //Đổi tiêu đề của cột
                    //GridView1.Columns[1].HeaderText = "Tên hàng"; //Cách này sẽ làm mất link dùng để sắp xếp
                    //GridView1.Columns[2].HeaderText = "Thể loại";
                    //Response.Write( GridView1.Columns.Count.ToString());
                    ((LinkButton)(gHangHoa.HeaderRow.Cells[0].Controls[0])).Text = "Mã Sản Phẩm";
                    ((LinkButton)(gHangHoa.HeaderRow.Cells[1].Controls[0])).Text = "Tên Sản Phẩm";
                    //gHangHoa.HeaderRow.Cells[1].Text = "Tên hàng";
                    ((LinkButton)(gHangHoa.HeaderRow.Cells[2].Controls[0])).Text = "Hãng";
                    ((LinkButton)(gHangHoa.HeaderRow.Cells[3].Controls[0])).Text = "Xuất Xứ";
                }
            }
            catch (Exception exc)
            {
                //MessageBox.Show
                Response.Write(String.Format("Loi: {0}\n{1}", exc.Message, exc.StackTrace));
            }
            finally
            {
                sqlSconn.Close();
                sqlScomm4.Dispose();
            }
        }
        public void BindDataToGridview(int pageIndex = 0)
        {
            // Thuc thi truy van va dua len dataGridView
            SqlConnection sqlconn3 = new SqlConnection();
            SqlCommand sqlcomm3 = new SqlCommand();

            try
            {
                sqlconn3.ConnectionString = connString;
                sqlconn3.Open();
                if (sqlconn3.State == System.Data.ConnectionState.Open)
                {
                    //Response.Write("</br>Kết nối thành công! </br>");
                    sqlcomm3.Connection = sqlconn3;
                    sqlcomm3.CommandType = System.Data.CommandType.Text;
                    sqlcomm3.CommandText = "SELECT        SanPham.MaSP,SanPham.TenSP,HangXS.Hang, HangXS.XuatXu "
                        + "FROM HangXS INNER JOIN "
                        + "SanPham ON HangXS.Hang = SanPham.Hang ";
                    DataTable dt = new DataTable("DSHang");
                    SqlDataAdapter da3 = new SqlDataAdapter(sqlcomm3);
                    //dt.Rows.
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

        protected void gHangHoa_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                string sortDirection;    //Mặc định ASC
                ///Dùng ViewState để chuyển dữ liệu giữa 2 lần gọi cùng một trang web
                /// cụ thể ở đây là thông tin dùng để sắp xếp, gồm hướng sắp xếp
                /// Chú ý, có thể dùng Context để chuyển dữ liệu giữa các lần gọi các trang web khác nhau
                /// Xem thêm về context, session: https://msdn.microsoft.com/en-us/library/swe97x0b.aspx
                if (ViewState["sortDirection"] != null) sortDirection = ViewState["sortDirection"] as string;
                else sortDirection = "ASC";
                //Đưa thông báo dưới dạng javascript alert
                Response.Write(String.Format("<script>alert('Sắp xếp {1} theo cột {0}')</script>", e.SortExpression, sortDirection));

                //Đọc dữ liệu, sắp xếp theo cách mới và đưa lên gridview
                BindDataToGridView(gHangHoa.PageIndex, e.SortExpression, sortDirection);
                //Đảo chiều sắp xếp sau mỗi lần click
                if (sortDirection == "ASC") sortDirection = "DESC";
                else sortDirection = "ASC";
                //Lưu cách sắp xếp vào ViewState để có thể sử dụng khi chuyển sang lần gọi sau 
                ViewState["sortDirection"] = sortDirection;
            }
            catch (Exception exc)
            {
                Response.Write(String.Format("Lỗi: {0}<br />{1}", exc.Message, exc.StackTrace));
            }
        }
    }
}