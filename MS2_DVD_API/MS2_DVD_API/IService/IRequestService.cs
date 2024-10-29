using MS2_DVD_API.Modals.ResponseModal;

namespace MS2_DVD_API.IService
{
    public interface IRequestService
    {
        Task<List<RequestRespone>> GetAllRequestsAsync();
        Task<RequestRespone> GetRequestByIdAsync(int id);
        Task<RequestRespone> AddRequestAsync(RequestRespone requestRequest);
        Task UpdateRequestAsync(int id, RequestRespone requestRequest);
        Task DeleteRequestAsync(int id);
        Task<bool> ActivateRequestAsync(int requestId);
        Task<bool> DeactivateRequestAsync(int requestId);
    }
}
