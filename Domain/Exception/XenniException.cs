namespace Domain.Exception
{

    // for global custom exceptions
    // this status Response should be 400
    public class XenniException(string _message) : System.Exception(_message)
    {     
    }

    public class XenniNotFoundException() : System.Exception("No Data Found")
    {
    }

    // For activity to force user to re-login
    // this status Response should be 491
    public class ReloginException(string _message) : System.Exception(_message)
    {
    }
}
