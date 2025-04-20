<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>To-Do List</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>To-Do List</h2>

        <asp:TextBox ID="TaskInput" runat="server" Width="300" />
        <asp:Button ID="AddButton" runat="server" Text="Add Task" OnClick="AddButton_Click" />

        <asp:GridView ID="TasksGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Description" HeaderText="Task" />
                <asp:CheckBoxField DataField="IsDone" HeaderText="Done" />
                <asp:ButtonField Text="Delete" ButtonType="Button" CommandName="Delete" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
