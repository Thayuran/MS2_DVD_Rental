using Microsoft.Data.SqlClient;
using MS2_DVD_API.IRepository;
using MS2_DVD_API.Modals.ResponseModal;

namespace MS2_DVD_API.Repository
{
    public class RequestRepository:IRequestRepository
    {
        private readonly string _connectionString;

        public RequestRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
     
    }
}
