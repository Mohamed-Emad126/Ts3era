namespace Ts3era.HandleResponseApi
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statuscode, string message = null,string datails=null ) : base(statuscode, message)
        {
            this.Details = datails;
        }

        public string Details { get; set; }
    }
}
