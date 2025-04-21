<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaskEditor.ascx.cs" Inherits="TaskEditor" %>
<asp:GridView ID="TasksGrid" runat="server"
    AutoGenerateColumns="false"
    OnRowEditing="TasksGrid_RowEditing"
    OnRowUpdating="TasksGrid_RowUpdating"
    OnRowCancelingEdit="TasksGrid_RowCancelingEdit"
    OnRowCommand="TasksGrid_RowCommand">
  <Columns>
    <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="true" />
    <asp:TemplateField HeaderText="Description">
      <ItemTemplate><%# Eval("Description") %></ItemTemplate>
      <EditItemTemplate>
        <asp:TextBox ID="DescBox" runat="server" Text='<%# Eval("Description") %>' />
      </EditItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Done">
      <ItemTemplate><asp:CheckBox runat="server" Checked='<%# Eval("IsDone") %>' Enabled="false" /></ItemTemplate>
      <EditItemTemplate>
        <asp:CheckBox ID="DoneBox" runat="server" Checked='<%# Eval("IsDone") %>' />
      </EditItemTemplate>
    </asp:TemplateField>
    <asp:BoundField DataField="Timestamp" HeaderText="Last Updated" DataFormatString="{0:G}" />
    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
  </Columns>
</asp:GridView>
<asp:TextBox ID="NewTaskBox" runat="server" Width="300px" />
<asp:Button ID="AddBtn" runat="server" Text="Add Task" OnClick="AddBtn_Click" />
