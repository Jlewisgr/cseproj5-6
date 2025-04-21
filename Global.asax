<%@ Application Language="C#" %>

<script runat="server">
    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        System.IO.File.AppendAllText(Server.MapPath("~/App_Data/log.txt"), 
            $"Application started at {DateTime.Now}\n");
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started (optional)
        Session["TaskCount"] = 0;
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code to handle global errors (optional)
        Exception ex = Server.GetLastError();
        System.IO.File.AppendAllText(Server.MapPath("~/App_Data/log.txt"), 
            $"Error at {DateTime.Now}: {ex.Message}\n");
    }
</script>
