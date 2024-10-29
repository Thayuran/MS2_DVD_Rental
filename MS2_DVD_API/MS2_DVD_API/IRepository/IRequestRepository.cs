using MS2_DVD_API.Modals.ResponseModal;

namespace MS2_DVD_API.IRepository
{
    public interface IRequestRepository
    {
        Task<RequestRespone> AddRequest(RequestRespone request);
        Task<List<RequestRespone>> GetAllRequests();
        Task<RequestRespone> GetRequestById(int requestId);
        Task<RequestRespone> UpdateRequest(RequestRespone request);
        Task<bool> DeleteRequest(int requestId);
        Task<bool> ActivateRequest(int requestId);
        Task<bool> DeactivateRequest(int requestId);
    }
}
