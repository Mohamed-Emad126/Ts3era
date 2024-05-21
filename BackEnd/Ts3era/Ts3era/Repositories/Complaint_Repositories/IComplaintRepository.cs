using Ts3era.Dto.ComplaintsDto;

namespace Ts3era.Repositories.Complaint_Repositories
{
    public interface IComplaintRepository
    {
        Task<List<ComplaintDetailsDto>>GetAll();
        Task<int> GetCountAllComplaint();
        Task<ComplaintDetailsDto> GetById(int id);

        Task<ComplaintDto> Create(ComplaintDto complaintdto);

        Task  Delete(int id);
    }
}
