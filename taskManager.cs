using System.Data;
using System.Xml;
using System.IO;

public static class TaskManager
{
    public static void AddTask(string filePath, string taskDesc)
    {
        XmlDocument doc = LoadOrCreateDoc(filePath);
        XmlElement task = doc.CreateElement("Task");

        XmlElement desc = doc.CreateElement("Description");
        desc.InnerText = taskDesc;

        XmlElement isDone = doc.CreateElement("IsDone");
        isDone.InnerText = "false";

        task.AppendChild(desc);
        task.AppendChild(isDone);

        doc.DocumentElement.AppendChild(task);
        doc.Save(filePath);
    }

    public static void RemoveTask(string filePath, int index)
    {
        XmlDocument doc = LoadOrCreateDoc(filePath);
        XmlNodeList tasks = doc.SelectNodes("/Tasks/Task");

        if (index >= 0 && index < tasks.Count)
        {
            doc.DocumentElement.RemoveChild(tasks[index]);
            doc.Save(filePath);
        }
    }

    public static DataTable GetTasks(string filePath)
    {
        XmlDocument doc = LoadOrCreateDoc(filePath);
        DataTable table = new DataTable();
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("Description");
        table.Columns.Add("IsDone", typeof(bool));

        XmlNodeList nodes = doc.SelectNodes("/Tasks/Task");
        int i = 0;
        foreach (XmlNode node in nodes)
        {
            DataRow row = table.NewRow();
            row["Id"] = i++;
            row["Description"] = node["Description"].InnerText;
            row["IsDone"] = bool.Parse(node["IsDone"].InnerText);
            table.Rows.Add(row);
        }

        return table;
    }

    private static XmlDocument LoadOrCreateDoc(string filePath)
    {
        XmlDocument doc = new XmlDocument();

        if (File.Exists(filePath))
        {
            doc.Load(filePath);
        }
        else
        {
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);

            XmlElement root = doc.CreateElement("Tasks");
            doc.AppendChild(root);
            doc.Save(filePath);
        }

        return doc;
    }
}
