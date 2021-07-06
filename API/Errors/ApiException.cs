namespace API.Errors
{
    // this is what we will return to the client
    public class ApiException
    {
    public ApiException(int statusCode, string message=null, string details=null)
    {
      StatusCode = statusCode;
      Message = message;
      Details = details;
    }

    public int StatusCode { get; set; }

        public string Message { get; set; }
        // it will be the stack trace we get 
        public string Details { get; set; }
        
    }
}