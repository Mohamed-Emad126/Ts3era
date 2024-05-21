namespace Ts3era.Models
{
    public class Complaints
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string National_Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }   
        public string Details { get; set; }

        public string ? Attachment {  get; set; }

    }
}
