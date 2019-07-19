<%@ Page Title="" Language="C#" MasterPageFile="~/QuanLy.master" AutoEventWireup="true" CodeBehind="QL2.aspx.cs" Inherits="QuanLyCuaHangDienTu.QL2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

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
            <td>Hãng</td>
            <td>
                <asp:DropDownList ID="ddlHang" runat="server"></asp:DropDownList>
            </td>

        </tr>
      
        <br /> 
        <tr>
            <td>
                <asp:Button ID="btThem" runat="server" Text="Thêm" OnClick="btThem_Click" Width="73px"  />
                <asp:Button ID="btSua" runat="server" Text="Sửa" OnClick="btSua_Click" Width="66px"  />
               
            </td>
            <td style="width: 203px">
                 <asp:Button ID="btXoa" runat="server" Text="Xóa" OnClick="btXoa_Click" Width="74px" />
                <asp:Button ID="btTim" runat="server" Text="Tìm Kiếm" OnClick="btTimKiem_Click" Width="74px"/>
            </td>
         
        </tr>
        <tr>
            <td>
                <asp:Label ID="lThongBao" runat="server" Text="Thông Báo"></asp:Label>
            </td>
        </tr>
    

    </table>
    <asp:GridView ID="gHangHoa" runat="server" HorizontalAlign="Center" AllowPaging="True" PageSize="5" OnPageIndexChanging="gHangHoa_PageIndexChanging" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal"  Width="100%">
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>
    <br />
    <br />
        <h4 style="text-align:right"><a href="DANGXUAT.aspx">ĐĂNG XUẤT</a></h4>

</asp:Content>

