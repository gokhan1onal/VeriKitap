<%@ Page Title="Check-in" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CheckIn.aspx.cs" Inherits="VeriKitap.CheckIn" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <form runat="server">
        <asp:Panel runat="server">

            <center>
                <h3>Check-In</h3>
                <table style="margin-top: 15px;">
                    <tr>
                        <td>Teslim Alan:  
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtTeslimAlan" onkeypress="return isText(event)" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Cep Telefonu:  
                        </td>
                        <td>
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtCepTel" placeholder="0-541-555-44-33" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Teslim Tarihi:  
                        </td>
                        <td>
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtTeslimTarihi" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Gerçek Teslim:  
                        </td>
                        <td>
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtGercekTeslim" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Ceza Tutarı:  
                        </td>
                        <td>
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtCeza" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding-top: 10px;">
                            <asp:Button runat="server" Text="Kaydet" ID="btnKaydet" OnClick="btnKaydet_Click" /></td>
                    </tr>
                </table>
            </center>
        </asp:Panel>
    </form>
</asp:Content>

