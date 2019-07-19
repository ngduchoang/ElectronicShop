<%@ Page Title="" Language="C#" MasterPageFile="~/TrangChu.master" AutoEventWireup="true" CodeBehind="ThongTin.aspx.cs" Inherits="QuanLyCuaHangDienTu.ThongTin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <br />
    <br />
    <br />
            <h3 style="text-align:center;margin-bottom:10px; height: 22px;width: 292px">DANH SÁCH SẢN PHẨM</h3>
    <br />
        <asp:GridView ID="gHangHoa" runat="server" HorizontalAlign="Center" AllowPaging="True" PageSize="10" EnableViewState="true" OnPageIndexChanging="gHangHoa_PageIndexChanging" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"  EnableModelValidation="true" GridLines="Horizontal" Width="100%" AllowSorting="True" OnSorting="gHangHoa_Sorting" >
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
   
</asp:Content>

