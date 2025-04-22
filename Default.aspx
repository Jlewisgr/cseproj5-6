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

        <asp:GridView ID="TasksGrid" runat="server" AutoGenerateColumns="False" OnRowCommand="TasksGrid_RowCommand">
            <Columns>
                <asp:BoundField DataField="Description" HeaderText="Task" />

                <asp:TemplateField HeaderText="Done">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkDone" runat="server"
                            Checked='<%# Convert.ToBoolean(Eval("IsDone")) %>'
                            AutoPostBack="true"
                            OnCheckedChanged="chkDone_CheckedChanged"
                            CommandArgument='<%# Container.DataItemIndex %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:Button ID="DeleteButton" runat="server" Text="Delete"
                            CommandName="CustomDelete"
                            CommandArgument="<%# Container.DataItemIndex %>" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
