using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using MS2_DVD_API.Entity;
using MS2_DVD_API.IRepository;
using MS2_DVD_API.Modals.RequestModal;

namespace MS2_DVD_API.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        

    }
}
