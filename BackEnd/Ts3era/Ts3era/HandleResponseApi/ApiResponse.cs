using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Ts3era.HandleResponseApi
{
    public class ApiResponse
    {

        public ApiResponse(int statuscode ,string message=null  )
        {
            this.StatusCode = statuscode;
            this.Message = message??GetMessagcode(statuscode);

        }
        public int StatusCode { get; set; }
        public string Message { get; set; }




        private string GetMessagcode(int code) =>
            code switch
            {
                400 => "BadRequest",
                401 => "Not Authrized..!",
                404 => "Not Found..!",
                500 => "Internal Servar Error !",
                _ => null
            };
    }
}
