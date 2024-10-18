using Azure.Core;
using DVDRental.Entities;

namespace DVDRental.Repositories
{
    public interface IRequestRepository
    {
        Task<List<Rent_Request>> GetAllRequestsAsync();
        Task<Rent_Request> GetRequestByIdAsync(string id);
        Task<Rent_Request> AddRequestAsync(Rent_Request request);
        Task UpdateRequestAsync(Rent_Request request);
        Task DeleteRequestAsync(string id);
    }
}
