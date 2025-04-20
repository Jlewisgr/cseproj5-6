using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    private string filePath = HttpContext.Current.Server.MapPath("~/App_Data/tasks.xml");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTasks();
        }
    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        TaskManager.AddTask(filePath, TaskInput.Text);
        TaskInput.Text = "";
        BindTasks();
    }

    protected void TasksGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            TaskManager.RemoveTask(filePath, index);
            BindTasks();
        }
    }

    private void BindTasks()
    {
        TasksGrid.DataSource = TaskManager.GetTasks(filePath);
        TasksGrid.DataBind();
    }
}
