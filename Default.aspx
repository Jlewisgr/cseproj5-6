<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<html><body>
  <h1>To Do List Manager</h1>
  <p>Welcome! Choose your role:</p>
  <asp:Button ID="MemberBtn" runat="server" Text="Member" PostBackUrl="~/Member.aspx" />
  <asp:Button ID="StaffBtn"  runat="server" Text="Staff"  PostBackUrl="~/Staff.aspx" />
</body></html>

