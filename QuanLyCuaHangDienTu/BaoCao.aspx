<%@ Page Title="" Language="C#" MasterPageFile="~/NhanVien.master" AutoEventWireup="true" CodeBehind="BaoCao.aspx.cs" Inherits="QuanLyCuaHangDienTu.BaoCao" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
     <div>
    <asp:Label ID="Label1" runat="server" Text="Location:  "></asp:Label>
    <asp:TextBox ID="tbLocation" runat="server"></asp:TextBox>
    <asp:Button ID="bXemBaoCao" runat="server" 
        Text="Xem báo cáo" />
         <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" GroupTreeImagesFolderUrl="" Height="1394px" ReportSourceID="rsbaoCao" ToolbarImagesFolderUrl="" ToolPanelWidth="200px" Width="1517px" />
         <CR:CrystalReportSource ID="rsbaoCao" runat="server">
             <Report FileName="crBaoCaoBanHang.rpt">
             </Report>
         </CR:CrystalReportSource>
</div>
   
</asp:Content>
