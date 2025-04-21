using System;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class TimeService : WebService
{
    [WebMethod]
    public DateTime GetServerTime()
    {
        return DateTime.Now;
    }
}
