using MS2_DVD_API.IService;

namespace MS2_DVD_API.Service
{
    public class CustomerService: ICustomerService
    {
        private readonly string _connectionString;
        
        public CustomerService(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
