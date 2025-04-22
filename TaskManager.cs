using System;
using System.Data;
using System.IO;
using System.Xml;

public static class TaskManager
{
    // Try to load the XML file or make a new one if it doesnt exist
    private static XmlDocument LoadOrCreateDoc(string filePath)
    {
        var doc = new XmlDocument();
        if (File.Exists(filePath))
        {
            // File is there load it
            doc.Load(filePath);
        }
        else
        {
            // No file yet set up a fresh XML document
            var dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            var root = doc.CreateElement("Tasks");
            doc.AppendChild(dec);
            doc.AppendChild(root);
            doc.Save(filePath);  // save the new file
        }
        return doc;
    }

    // Add a new task with description into the XML
    public static void AddTask(string filePath, string taskDesc)
    {
        var doc = LoadOrCreateDoc(filePath);
        var task = doc.CreateElement("Task");

        var desc = doc.CreateElement("Description");
        desc.InnerText = EncryptionHelper.Encrypt(taskDesc);  // encrypt or plain text

        var isDone = doc.CreateElement("IsDone");
        isDone.InnerText = "false";  // new tasks start as not done

        var ts = doc.CreateElement("Timestamp");
        ts.InnerText = DateTime.Now.ToString("o");  // use current time

        // stick the pieces together and save
        task.AppendChild(desc);
        task.AppendChild(isDone);
        task.AppendChild(ts);
        doc.DocumentElement.AppendChild(task);
        doc.Save(filePath);
    }

    // Remove a task by its index in the list
    public static void RemoveTask(string filePath, int index)
    {
        var doc = LoadOrCreateDoc(filePath);
        var nodes = doc.SelectNodes("/Tasks/Task");
        if (index >= 0 && index < nodes.Count)
        {
            doc.DocumentElement.RemoveChild(nodes[index]);
            doc.Save(filePath);
        }
    }

    // Change an existing tasks text or done status
    public static void UpdateTask(string filePath, int index, string newDesc, bool isDone)
    {
        var doc = LoadOrCreateDoc(filePath);
        var nodes = doc.SelectNodes("/Tasks/Task");
        if (index >= 0 && index < nodes.Count)
        {
            var node = nodes[index];
            node["Description"].InnerText = EncryptionHelper.Encrypt(newDesc);
            node["IsDone"].InnerText = isDone.ToString().ToLower();
            node["Timestamp"].InnerText = DateTime.Now.ToString("o");  // update time
            doc.Save(filePath);
        }
    }

    // Read all tasks into a DataTable we can bind to a UI
    public static DataTable GetTasks(string filePath)
    {
        var doc = LoadOrCreateDoc(filePath);
        var table = new DataTable();
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("Description", typeof(string));
        table.Columns.Add("IsDone", typeof(bool));
        table.Columns.Add("Timestamp", typeof(DateTime));

        var nodes = doc.SelectNodes("/Tasks/Task");
        int i = 0;
        foreach (XmlNode node in nodes)
        {
            var row = table.NewRow();
            row["Id"] = i++;
            row["Description"] = EncryptionHelper.Decrypt(node["Description"].InnerText);
            row["IsDone"] = bool.Parse(node["IsDone"].InnerText);
            row["Timestamp"] = DateTime.Parse(node["Timestamp"].InnerText);
            table.Rows.Add(row);
        }
        return table;
    }
}

