<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VeriKitap.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <form runat="server">

        <script type="text/javascript">
            function checkRadioBtn(radio) {
                try {
                    var gv = document.getElementById("gvBooks");
                    for (var i = 1; i < gv.rows.length; i++) {
                        var radioBtn = gv.rows[i].cells[0].getElementsByClassName("radio");

                        if (radioBtn[0].getAttribute("name") != radio.getAttribute("name"))
                            radioBtn[0].checked = false;
                    }

                    if (radio.value.indexOf("Check-in") > 0) {
                        document.getElementById("btnCheckIn").disabled = true;
                        document.getElementById("btnCheckOut").disabled = false;
                    }
                    else {
                        document.getElementById("btnCheckIn").disabled = false;
                        document.getElementById("btnCheckOut").disabled = true;
                    }

                    document.getElementById("hfSelectedBook").value = radio.getAttribute("name");
                } catch (e) {
                    console.error(e.stack);
                }
            }

            function yonlendir(btn) {
                try {
                    var id = document.getElementById("hfSelectedBook").value;
                    var sayfa = "CheckIn.aspx";
                    if (btn == 'out')
                        sayfa = "CheckOut.aspx";


                    window.location.href = sayfa + "?id=" + id;
                } catch (e) {
                    console.error(e.stack);
                }
            }
        </script>
        <asp:Panel runat="server">
            <div class="center">
                <asp:HiddenField runat="server" ClientIDMode="Static" ID="hfSelectedBook" Value="0" />
                <div style="padding-top: 10px;">
                    <table style="width: 100%;">
                        <tr>
                            <asp:GridView ClientIDMode="Static" ID="gvBooks" runat="server" AllowPaging="True" EmptyDataText="Herhangi bir kitap bulunamadı." AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" PageSize="500" ShowHeaderWhenEmpty="True">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <input type="radio" class="radio" onclick="checkRadioBtn(this);" name="<%# Eval("Id") %>" value="<%# Eval("Durum") %>" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Baslik" HeaderText="Kitap Başlığı" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Isbn" HeaderText="ISBN" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Yil" HeaderText="Yayın Yılı" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Fiyat" HeaderText="Fiyatı" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Durum" HeaderText="Durum" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>

                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                            <center>
                                <button type="button" id="btnCheckIn" onclick="yonlendir('in');" disabled="disabled">Check-in</button>
                                <button type="button" id="btnCheckOut" onclick="yonlendir('out');" disabled="disabled">Check-out</button>
                            </center>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
    </form>
</asp:Content>
