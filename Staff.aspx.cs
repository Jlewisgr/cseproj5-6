using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Staff : System.Web.UI.Page
{
    string UsersPath => Server.MapPath("~/App_Data/users.xml");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) BindUsers();
    }

    void BindUsers()
    {
        if (!File.Exists(UsersPath))
        {
            var d = new XmlDocument();
            var dec = d.CreateXmlDeclaration("1.0","utf-8",null);
            d.AppendChild(dec);
            d.AppendChild(d.CreateElement("Users"));
            d.Save(UsersPath);
        }
        var dt = new DataTable();
        dt.Columns.Add("Username");
        var doc = new XmlDocument();
        doc.Load(UsersPath);
        var nodes = doc.SelectNodes("/Users/User/Username");
        foreach (XmlNode n in nodes)
        {
            var r = dt.NewRow();
            r["Username"] = n.InnerText;
            dt.Rows.Add(r);
        }
        UsersGrid.DataSource = dt;
        UsersGrid.DataBind();
    }

    protected void AddUserBtn_Click(object sender, EventArgs e)
    {
        var user = NewUserBox.Text.Trim();
        var pass = NewPassBox.Text;
        var doc = new XmlDocument();
        doc.Load(UsersPath);
        if (doc.SelectSingleNode($"/Users/User[Username='{user}']") != null) return;
        var u = doc.CreateElement("User");
        var un = doc.CreateElement("Username"); un.InnerText = user;
        var pw = doc.CreateElement("Password"); pw.InnerText = EncryptionHelper.Encrypt(pass);
        u.AppendChild(un);
        u.AppendChild(pw);
        doc.DocumentElement.AppendChild(u);
        doc.Save(UsersPath);
        BindUsers();
    }

    protected void UsersGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int idx = Convert.ToInt32(e.CommandArgument);
            var doc = new XmlDocument();
            doc.Load(UsersPath);
            var users = doc.SelectNodes("/Users/User");
            if (idx >= 0 && idx < users.Count)
            {
                doc.DocumentElement.RemoveChild(users[idx]);
                doc.Save(UsersPath);
                BindUsers();
            }
        }
    }
}
