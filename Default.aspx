<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>To-Do List App</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>To-Do List</h2>

        <asp:TextBox ID="TaskInput" runat="server" Width="300" />
        <asp:Button ID="AddButton" runat="server" Text="Add Task" OnClick="AddButton_Click" />

        <br /><br />

        <asp:GridView ID="TasksGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="TasksGrid_RowCommand">
            <Columns>
                <asp:BoundField DataField="Description" HeaderText="Task" />
                <asp:CheckBoxField DataField="IsDone" HeaderText="Done" />
                <asp:ButtonField Text="Delete" CommandName="Delete" ButtonType="Button" />
            </Columns>
        </asp:GridView>

        <hr />

        <h3>TryIt: Call Web Service</h3>
        <asp:Button ID="btnGetTime" runat="server" Text="Get Server Time" OnClick="btnGetTime_Click" />
        <asp:Label ID="lblTime" runat="server" Text="" Style="margin-left:10px" />
    </form>
</body>
</html>
