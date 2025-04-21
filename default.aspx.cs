using System;
using System.Web.UI.WebControls;
using System.Data;

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
        string task = TaskInput.Text.Trim();
        if (!string.IsNullOrEmpty(task))
        {
            TaskManager.AddTask(filePath, task);
            TaskInput.Text = "";
            BindTasks();
        }
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

    protected void btnGetTime_Click(object sender, EventArgs e)
    {
        try
        {
            // Replace this with the real service proxy if using Add Service Reference
            DateService service = new DateService();
            lblTime.Text = service.GetCurrentDateTime();
        }
        catch (Exception ex)
        {
            lblTime.Text = "Error: " + ex.Message;
        }
    }
}
