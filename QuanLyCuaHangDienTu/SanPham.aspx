<%@ Page Title="" Language="C#" MasterPageFile="~/NhanVien.master" AutoEventWireup="true" CodeBehind="SanPham.aspx.cs" Inherits="QuanLyCuaHangDienTu.SanPham" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="height: 213px; width: 100%;background-color:#353866;color:white">
        <tr>
            <td>
                Mã Sản Phẩm
            </td>
            <td style="width: 203px">
                <asp:TextBox ID="tbMaSP" runat="server" Width="160px"></asp:TextBox>
            </td>
            <td></td>
            <td>Tên Sản Phẩm</td>
            <td>
                <asp:TextBox ID="tbTenSP" runat="server"></asp:TextBox>
            </td>
            <td></td>
            <td>Giá</td>
            <td>
                <asp:TextBox ID="tbGia" runat="server"></asp:TextBox>
            </td>
            <td></td>
            <td>Hãng</td>
            <td>
                <asp:DropDownList ID="ddlHang" runat="server"></asp:DropDownList>
            </td>

        </tr>
      <tr>
          <td>Hình ảnh:</td><td>
            
            <asp:FileUpload ID="FileUploadControl" runat="server" />
      </tr>
        <tr>
            <td>
                <asp:Button ID="btThem" runat="server" Text="Thêm"  Width="73px" OnClick="btThem_Click"  />
            </td>
        </tr>
        <br /> 

        <tr>
            <td>
                <asp:Label ID="lThongBao" runat="server" ></asp:Label>
            </td>
        </tr>
    

    </table>

    
    <br />
    <asp:RegularExpressionValidator ID="regexValidator" runat="server"
     ControlToValidate="FileUploadControl"
     ErrorMessage="Only JPEG images are allowed" 
     ValidationExpression="(.*\.([Jj][Pp][Gg])|.*\.([Jj][Pp][Ee][Gg])$)">
</asp:RegularExpressionValidator>
                <!--End: Đoạn này giúp chọn file hình ảnh có đuôi JPG -->
            </td>
        </tr>
   
    <br />
    <p>
        <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
        
        <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" Width="1500px" RepeatColumns="3" Height="350px">
            <HeaderTemplate >
                SẢN PHẨM
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Table ID="Table1" runat="server" Width="400px">
                    <asp:TableRow runat="server">
                        <asp:TableCell runat="server">
                    ID: <asp:Label ID="lId" runat="server" Text='<%# Eval("MaSP") %>'></asp:Label><br />
                    Tên:        <asp:Label ID="lTenHang" runat="server" Text='<%# Eval("TenSP") %>'></asp:Label><br />
                    Loại:        <asp:Label ID="lLoaiHang" runat="server" Text='<%# Eval("Hang") %>'></asp:Label><br />
                    Giá:         <asp:Label ID="lGia" runat="server" Text='<%# Eval("Gia") %>'></asp:Label><br />
                        </asp:TableCell>
                        <asp:TableCell runat="server">
                            <asp:Image ID="hinhAnh" runat="server" Height="120" Width="160" ImageUrl='<%#Bind("hinhAnh","~/files/img/{0}")%>' />

                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </ItemTemplate>
        </asp:DataList>
    </p>
    <br />
        <h4 style="text-align:right"><a href="DANGXUAT.aspx">ĐĂNG XUẤT</a></h4>
</asp:Content>
