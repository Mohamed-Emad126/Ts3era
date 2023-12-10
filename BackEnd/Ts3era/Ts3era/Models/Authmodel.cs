using System.Globalization;

namespace Ts3era.Models
{
    public class Authmodel
    {
        public string UserName { get; set; }=string.Empty;
        public DateTime Expireon { get; set; }
         public  bool  IsAuthanticated  { get; set; }
        public string Email { get; set; } = string.Empty;
        public List<string> Role {  get; set; }=new List<string>();
        public string Token { get; set; } = string.Empty;
        public string Massage { get; set; } = string.Empty;


    }
}
