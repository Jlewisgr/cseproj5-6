using System;
using System.Data;
using System.IO;
using System.Xml;

public static class TaskManager
{
    private static XmlDocument LoadOrCreateDoc(string filePath)
    {
        var doc = new XmlDocument();
        if (File.Exists(filePath))
        {
            doc.Load(filePath);
        }
        else
        {
            var dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            var root = doc.CreateElement("Tasks");
            doc.AppendChild(dec);
            doc.AppendChild(root);
            doc.Save(filePath);
        }
        return doc;
    }

    public static void AddTask(string filePath, string taskDesc)
    {
        var doc = LoadOrCreateDoc(filePath);
        var task = doc.CreateElement("Task");

        var desc = doc.CreateElement("Description");
        desc.InnerText = EncryptionHelper.Encrypt(taskDesc);  // Or just taskDesc if EncryptionHelper isn't ready

        var isDone = doc.CreateElement("IsDone");
        isDone.InnerText = "false";

        var ts = doc.CreateElement("Timestamp");
        ts.InnerText = DateTime.Now.ToString("o");  // Replaced TimeService with local time

        task.AppendChild(desc);
        task.AppendChild(isDone);
        task.AppendChild(ts);
        doc.DocumentElement.AppendChild(task);
        doc.Save(filePath);
    }

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

    public static void UpdateTask(string filePath, int index, string newDesc, bool isDone)
    {
        var doc = LoadOrCreateDoc(filePath);
        var nodes = doc.SelectNodes("/Tasks/Task");
        if (index >= 0 && index < nodes.Count)
        {
            var node = nodes[index];
            node["Description"].InnerText = EncryptionHelper.Encrypt(newDesc);  // Or just newDesc
            node["IsDone"].InnerText = isDone.ToString().ToLower();
            node["Timestamp"].InnerText = DateTime.Now.ToString("o");  // Replaced service call
            doc.Save(filePath);
        }
    }

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
            row["Description"] = EncryptionHelper.Decrypt(node["Description"].InnerText);  // Or just node["Description"].InnerText
            row["IsDone"] = bool.Parse(node["IsDone"].InnerText);
            row["Timestamp"] = DateTime.Parse(node["Timestamp"].InnerText);
            table.Rows.Add(row);
        }
        return table;
    }
}
