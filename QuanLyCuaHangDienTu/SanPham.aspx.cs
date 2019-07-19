using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// 
/// </summary>
using System.Data;
using System.Data.SqlClient;


namespace QuanLyCuaHangDienTu
{
    public partial class SanPham : System.Web.UI.Page
    {
        String conString;
        protected void Page_Load(object sender, EventArgs e)
        {
            conString = WebClass.getConnectionStringByName("sqlSConString");
            //Response.Write("conString: "+conString+"<br />");
            if (!Page.IsPostBack)
            {
                #region Đọc dữ liệu và đưa lên danh sách dropdownlist
                //ddlTheLoai.Items.Clear();
                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "Chọn thể loại";
                ddlHang.Items.Add(item0);

                try
                {
                    CommonCode.DataClasses.DataTool dataTool = new CommonCode.DataClasses.DataTool();
                    string sql = "SELECT Hang, XuatXu FROM HangXS";
                    SqlDataReader sqlreader = dataTool.execReader(conString, sql, null);
                    if (sqlreader != null && sqlreader.HasRows)
                    {
                        while (sqlreader.Read())
                        {
                            ListItem item = new ListItem();
                            item.Value = sqlreader.GetSqlString(0).ToString(); //hang
                            item.Text = sqlreader.GetSqlString(0).ToString();//xuat xu
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
                #endregion
                //đưa dữ liệu lên Datalist
                BindDataToDataList();
            }
        }

        protected void btThem_Click(object sender, EventArgs e)
        {
            lThongBao.Text = "";
            try
            {
                if (tbMaSP.Text.Trim() == "" || tbTenSP.Text.Trim() == ""
                    || ddlHang.SelectedIndex == 0 || !FileUploadControl.HasFile)
                {
                    lThongBao.Text = "Phải nhập đủ dữ liệu!";
                    return;
                }
                


                
                string sql = "INSERT INTO SanPham(MaSP, TenSP, Hang,Gia, hinhAnh) VALUES(@id, @ten, @loai,@gia, @tenFile)";
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                sqlParams.Add(new SqlParameter("id", tbMaSP.Text.Trim()));
                sqlParams.Add(new SqlParameter("ten", tbTenSP.Text.Trim()));
                sqlParams.Add(new SqlParameter("loai", ddlHang.SelectedValue));
                sqlParams.Add(new SqlParameter("gia", tbGia.Text.Trim()));
                sqlParams.Add(new SqlParameter("tenFile", string.Format("hangHoa_{0}.jpg", tbMaSP.Text.Trim())));
                CommonCode.DataClasses.DataTool dataTool = new CommonCode.DataClasses.DataTool();
                int cnt = dataTool.execInsUpdDel(conString, sql, sqlParams);
                lThongBao.Text = cnt.ToString() + " sản phẩm đã được thêm thành công!";

               
                FileUploadControl.SaveAs(string.Format("{0}/files/img/hangHoa_{1}.jpg",
                    Server.MapPath("~"), tbMaSP.Text.Trim()));

                BindDataToDataList();
            }
            catch (Exception exc)
            {
                lThongBao.Text = "Kết nối CSDL thất bại! Lỗi: " + exc.Message + ". " + exc.StackTrace;

            }
            finally
            {
                
            }

        }
        protected void FileUploadControl_Load(object sender, EventArgs e)
        {
            //FileUploadControl.PostedFile.ContentType
        }

        private void BindDataToDataList()
        {

            try
            {
                string sql = "SELECT MaSP, TenSP, Hang, Gia, hinhAnh FROM SanPham ORDER BY MaSP DESC";
               
                DataList1.DataSource = (new CommonCode.DataClasses.DataTool()).execSelect(conString, sql, null);
                DataList1.DataBind();
            }
            catch (Exception exc)
            {
                //MessageBox.Show
                Response.Write(String.Format("Loi: {0}<br />{1}", exc.Message, exc.StackTrace));
            }
            finally
            {
            }
        }
    }
}