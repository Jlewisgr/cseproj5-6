using System;
using System.ServiceModel;

// define a service contract
[ServiceContract]
public interface IDateService
{
    // declare operation to get current date time
    [OperationContract]
    string GetCurrentDateTime();
}

// implement date service
public class DateService : IDateService
{
    // return current date time as formatted string
    public string GetCurrentDateTime()
    {
        return DateTime.Now.ToString("F");
    }
}
