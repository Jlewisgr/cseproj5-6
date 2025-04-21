using System;
using System.Web.UI.WebControls;

public partial class TaskEditor : System.Web.UI.UserControl
{
    private string FilePath => Server.MapPath("~/App_Data/tasks.xml");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) Bind();
    }

    void Bind()
    {
        TasksGrid.DataSource = TaskManager.GetTasks(FilePath);
        TasksGrid.DataBind();
    }

    protected void AddBtn_Click(object sender, EventArgs e)
    {
        TaskManager.AddTask(FilePath, NewTaskBox.Text.Trim());
        NewTaskBox.Text = "";
        Bind();
    }

    protected void TasksGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        TasksGrid.EditIndex = e.NewEditIndex;
        Bind();
    }

    protected void TasksGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        TasksGrid.EditIndex = -1;
        Bind();
    }

    protected void TasksGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int idx = e.RowIndex;
        var row = TasksGrid.Rows[idx];
        string desc   = ((TextBox)row.FindControl("DescBox")).Text;
        bool done     = ((CheckBox)row.FindControl("DoneBox")).Checked;
        TaskManager.UpdateTask(FilePath, idx, desc, done);
        TasksGrid.EditIndex = -1;
        Bind();
    }

    protected void TasksGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int idx = Convert.ToInt32(e.CommandArgument);
            TaskManager.RemoveTask(FilePath, idx);
            Bind();
        }
    }
}
