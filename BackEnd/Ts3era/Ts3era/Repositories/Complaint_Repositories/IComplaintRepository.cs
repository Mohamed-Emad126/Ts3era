using Ts3era.Dto.ComplaintsDto;

namespace Ts3era.Repositories.Complaint_Repositories
{
    public interface IComplaintRepository
    {
        Task<List<ComplaintDto>>GetAll();
        Task<int> GetCountAllComplaint();
        Task<ComplaintDto> GetById(int id);

        Task<ComplaintDto> Create(ComplaintDto complaintdto);

        Task  Delete(int id);
    }
}
