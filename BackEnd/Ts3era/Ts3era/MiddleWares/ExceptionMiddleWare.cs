using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Ts3era.HandleResponseApi;

namespace Ts3era.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly ILogger<ExceptionMiddleWare> logger;
        private readonly RequestDelegate next ;
        private readonly IHostEnvironment environment ;


        public ExceptionMiddleWare(ILogger<ExceptionMiddleWare> logger,RequestDelegate next ,IHostEnvironment environment)
        {
            this.logger = logger;
            this.next = next;
            this.environment = environment;
        }

        public async Task Invoke (HttpContext context)
        {

            try
            {
                await next(context);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = environment
                    .IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);


            }



        }





    }
}
