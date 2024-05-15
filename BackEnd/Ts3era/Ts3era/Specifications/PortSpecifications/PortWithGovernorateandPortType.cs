using System.Linq.Expressions;
using Ts3era.Models;

namespace Ts3era.Specifications.PortSpecifications
{
    public class PortWithGovernorateandPortType : BaseSpecification<Ports>
    {
        public PortWithGovernorateandPortType(PortSpecification specification) : 
            base
            (
                c=>
                (!specification.PortType.HasValue||c.PortType_Id==specification.PortType)&&
                (!specification.governorate.HasValue||c.Governorate_Id==specification.governorate)
            )
        {
            AddInclude(c => c.Governorates);
            AddInclude(c => c.PortTypes);
        }  
        
        
        public PortWithGovernorateandPortType(int id ):base (c=>c.Id==id)
        {
            AddInclude(c => c.Governorates);
            AddInclude(c => c.PortTypes);
        }
    }
}
