
using MS2_DVD_API.Data;
using MS2_DVD_API.IRepository;
using MS2_DVD_API.IService;
using MS2_DVD_API.Repository;
using MS2_DVD_API.Service;

namespace MS2_DVD_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("Data");

            builder.Services.AddSingleton<ICustomerRepository>(provider => new CustomerRepository(connectionString));
            builder.Services.AddSingleton<ICustomerService>(provider=>new CustomerService(connectionString));


            var dbInitializer = new DatabaseInitializer(connectionString);
            dbInitializer.Initialize();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
