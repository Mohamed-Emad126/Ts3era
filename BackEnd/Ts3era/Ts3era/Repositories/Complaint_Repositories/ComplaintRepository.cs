using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using System.Data;
using Ts3era.Dto.ComplaintsDto;
using Ts3era.Models;
using Ts3era.Models.Data;

namespace Ts3era.Repositories.Complaint_Repositories
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHost;
        private readonly long MaxSize = 1048576;
        private new List<string> allawextentoin= new List<string>() { ".png", ".jpg" };

        public ComplaintRepository(
            ApplicationDbContext context,
            IMapper mapper,
            IWebHostEnvironment webHost
            
            )
        {
            this.context = context;
            this.mapper = mapper;
            this.webHost = webHost;
        }

      

        public async Task<List<ComplaintDetailsDto>> GetAll()
        {
            var complaint =await context.Complaints.ToListAsync();
            var map =mapper.Map<List<ComplaintDetailsDto>>(complaint); 
            return map;
        }

        public async  Task<ComplaintDetailsDto> GetById(int id)
        {
            var complaint = await context.Complaints.FirstOrDefaultAsync(c=>c.Id==id);
            var map = mapper.Map<ComplaintDetailsDto>(complaint);
            return map;
        }  
        
        public async  Task<ComplaintDto> Create(ComplaintDto complaintdto)
        {
            if (!allawextentoin.Contains(Path.GetExtension(complaintdto.AddAtachment.FileName).ToLower()))
                throw new ArgumentException("Only .png and jpg images are allowed !");
            if (complaintdto.AddAtachment.Length > MaxSize)
                throw new ArgumentException(" Max Allowed file! ");


            var uploadfile = Path.Combine(webHost.WebRootPath, "Images/Complaint");
            var uniquefile = Guid.NewGuid().ToString() + "_" + complaintdto.AddAtachment.FileName;
            var pathfile = Path.Combine(uploadfile, uniquefile);
            using var stream = new  FileStream(pathfile, FileMode.Create);

            var complaint = mapper.Map<Complaints>(complaintdto);
            complaintdto.AddAtachment.CopyTo(stream);
            stream.Close();
            complaint.Attachment = "Images/Complaint/" + uniquefile.ToString();
            context.Complaints.Add(complaint);
            context.SaveChanges();
            return complaintdto;

            /* using  var datastrem =new MemoryStream();
              await complaintdto.AddAtachment.CopyToAsync(datastrem);

              var map = mapper.Map<Complaints>(complaintdto);
            //  map.Attachment=datastrem.ToArray();
              context.Complaints.Add(map);
              context.SaveChanges();
              return complaintdto;*/
        }

        public async Task Delete(int id)
        {
            var complaint =await context.Complaints.FirstOrDefaultAsync(c=>c.Id==id);
            if (complaint == null||id!=complaint.Id)
            {
                throw new Exception($"Invalid Complaint With Id{id}");
            }
            else
            {
                context.Complaints.Remove(complaint);
                context.SaveChanges();
            }


        }

        public async Task<int> GetCountAllComplaint()
        {

            var count = await context.Complaints.CountAsync();
            return count;
        }
    }
}
