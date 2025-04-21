<%@ Application Language="C#" %>

<script runat="server">
    void Application_Start(object sender, EventArgs e)
    {
        System.IO.File.AppendAllText(Server.MapPath("~/App_Data/log.txt"),
            string.Format("App started: {0}\n", DateTime.Now));
    }

    void Session_Start(object sender, EventArgs e)
    {
        Session["TaskCount"] = 0;
    }

    void Application_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        System.IO.File.AppendAllText(Server.MapPath("~/App_Data/log.txt"),
            string.Format("Error at {0}: {1}\n", DateTime.Now, ex.Message));
    }
</script>
