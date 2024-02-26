using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Ts3era.HandleResponseApi
{
    public class ValidationError : ApiException
    {
        public ValidationError() : base(400)
        {
        }

        public IEnumerable<string>Errors { get; set; } 
    }
}
