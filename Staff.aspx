<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Staff.aspx.cs" Inherits="Staff" %>
<html><body>
  <h2>Staff Console: User Accounts</h2>
  <asp:GridView ID="UsersGrid" runat="server" AutoGenerateColumns="false"
      OnRowCommand="UsersGrid_RowCommand">
    <Columns>
      <asp:BoundField DataField="Username" HeaderText="Username" ReadOnly="true" />
      <asp:ButtonField ButtonType="Link" Text="Delete" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
    </Columns>
  </asp:GridView>
  <asp:TextBox ID="NewUserBox" runat="server" Placeholder="New username" />
  <asp:TextBox ID="NewPassBox" runat="server" TextMode="Password" Placeholder="Password" />
  <asp:Button ID="AddUserBtn" runat="server" Text="Add User" OnClick="AddUserBtn_Click" />
  <p><asp:Button ID="BackBtn2" runat="server" Text="← Back" PostBackUrl="~/Default.aspx" /></p>
</body></html>
