using Microsoft.Data.SqlClient;
using MS2_DVD_API.Entity;
using MS2_DVD_API.IRepository;

namespace MS2_DVD_API.Repository
{
    public class MovieRepository:ImovieRepository
    {

        private readonly string _connectionString;

        public MovieRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
       
    }
}
