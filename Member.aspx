<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Member.aspx.cs" Inherits="Member" %>
<%@ Register TagPrefix="uc" TagName="TaskEditor" Src="~/TaskEditor.ascx" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Member Area</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Member Area</h2>
        <uc:TaskEditor ID="Editor" runat="server" />
        <p>
            <asp:Button 
                ID="BackBtn" 
                runat="server" 
                Text="← Back" 
                PostBackUrl="~/Default.aspx" />
        </p>
    </form>
</body>
</html>

