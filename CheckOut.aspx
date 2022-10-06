<%@ Page Title="Check-out" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CheckOut.aspx.cs" Inherits="VeriKitap.CheckOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://unpkg.com/imask"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            IMask(
                document.getElementById('txtCepTel'), {
                mask: '0 000 000 00 00'
            });
        });
    </script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function isText(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return true;
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <form runat="server">
        <asp:Panel runat="server">

            <center>
                <h3>Check-Out</h3>
                <table style="margin-top: 15px;">
                    <tr>
                        <td>Teslim Alan:  
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtTeslimAlan" onkeypress="return isText(event)" required></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Cep Telefonu:  
                        </td>
                        <td>
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtCepTel" placeholder="0-541-555-44-33" required></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>TCKN:  
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtTckn" onkeypress="return isNumberKey(event)" MaxLength="11" required></asp:TextBox>
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
