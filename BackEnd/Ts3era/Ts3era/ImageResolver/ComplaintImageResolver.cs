using AutoMapper;
using Ts3era.Dto.ComplaintsDto;
using Ts3era.MappingProfile;
using Ts3era.Models;

namespace Ts3era.ImageResolver
{
    public class ComplaintImageResolver : IValueResolver<Complaints, ComplaintDetailsDto, string>
    {
        private readonly IConfiguration confiq;

        public ComplaintImageResolver(IConfiguration confiq )
        {
            this.confiq = confiq;
        }
        public string Resolve(Complaints source, ComplaintDetailsDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Attachment))
                return
                    confiq["baseurl"] +source.Attachment;

            return null;
        }
    }
}
