using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Web;

public partial class _Default : System.Web.UI.Page
{
    // set file path for tasks xml file
    private string filePath = HttpContext.Current.Server.MapPath("~/tasks.xml");

    // handle page load event
    protected void Page_Load(object sender, EventArgs e)
    {
        // check if this is first time loading page
        if (!IsPostBack)
        {
            // load tasks into grid
            BindTasks();
        }
    }

    // handle click on add button
    protected void AddButton_Click(object sender, EventArgs e)
    {
        // get task text from input and trim spaces
        string task = TaskInput.Text.Trim();
        // check if task text is not empty
        if (!string.IsNullOrEmpty(task))
        {
            // add new task to xml file
            TaskManager.AddTask(filePath, task);
            // clear input field
            TaskInput.Text = "";
            // refresh task grid
            BindTasks();
        }
    }

    // handle commands in tasks grid
    protected void TasksGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        // check if delete command was clicked
        if (e.CommandName == "Delete")
        {
            // convert argument to row index
            int index = Convert.ToInt32(e.CommandArgument);
            // remove task at that index
            TaskManager.RemoveTask(filePath, index);
            // refresh task grid
            BindTasks();
        }
    }

    // bind tasks data to grid control
    private void BindTasks()
    {
        // get list of tasks from xml file
        TasksGrid.DataSource = TaskManager.GetTasks(filePath);
        // bind data source to grid
        TasksGrid.DataBind();
    }

    // handle click on get time button
    protected void btnGetTime_Click(object sender, EventArgs e)
    {
        try
        {
            // create instance of date service
            DateService service = new DateService();
            // call service method to get current date time
            lblTime.Text = service.GetCurrentDateTime();
        }
        catch (Exception ex)
        {
            // show error message to user
            lblTime.Text = "Error: " + ex.Message;
        }
    }
}
