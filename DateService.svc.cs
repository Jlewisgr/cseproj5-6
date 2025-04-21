using System;
using System.ServiceModel;

[ServiceContract]
public interface IDateService
{
    [OperationContract]
    string GetCurrentDateTime();
}

public class DateService : IDateService
{
    public string GetCurrentDateTime()
    {
        return DateTime.Now.ToString("F"); // Example: "Friday, April 19, 2025 10:45 PM"
    }
}
