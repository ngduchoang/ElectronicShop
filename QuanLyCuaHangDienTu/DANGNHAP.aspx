<%@ Page Title="" Language="C#" MasterPageFile="~/TAIKHOAN.Master" AutoEventWireup="true" CodeBehind="DANGNHAP.aspx.cs" Inherits="QuanLyCuaHangDienTu.DANGNHAP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="body"></div>
		<div class="grad"></div>
		<div class="header">
			<div>HOANG<span>shop</span></div>
		</div>
		<br>
		<div class="login">
                <asp:TextBox ID="tbTenDangNhap" runat="server" placeholder="Tên đăng nhập"></asp:TextBox>
                <asp:TextBox ID="tbMatKhau" runat="server" TextMode="Password" placeholder="Mật khẩu"></asp:TextBox>
                <div>     <asp:Button ID="btDN" runat="server" Text="Login" CssClass="dn" Width="264px" OnClick="btDN_Click" /></div>
            <asp:Label ID="lThongBao" runat="server" ></asp:Label>
	
		</div>
  <script src='http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js'></script>
</asp:Content>
