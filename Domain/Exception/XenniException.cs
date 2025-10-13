namespace Domain.Exception
{

    // for global custom exceptions
    // this status Response should be 400
    public class XenniException(string message) : System.Exception(message)
    {
    }

    // For activity to force user to re-login
    // this status Response should be 491
    public class ReloginException(string message) : System.Exception(message)
    {
    }
}
