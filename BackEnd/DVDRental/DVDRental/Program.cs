
using DVDRental.Data;
using DVDRental.Repositories;
using DVDRental.Services;

namespace DVDRental
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


            var connectionString = builder.Configuration.GetConnectionString("connect");

            builder.Services.AddSingleton<IAdminDvdRepository>(provider => new AdminDvdRepository(connectionString));
            builder.Services.AddSingleton<ICustomerRepository>(provider => new CustomerRepository(connectionString));
            builder.Services.AddSingleton<IAdminCategoriesRepository>(provider => new AdminCategoriesRepository(connectionString));
            builder.Services.AddSingleton<IRequestRepository>(provider => new RequestRepository(connectionString));
            builder.Services.AddSingleton<IRentRepository>(provider => new RentRepository(connectionString));



            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IAdminDvdService, AdminDvdService>();
            builder.Services.AddScoped<IAdminCategoriesService, AdminCategoriesService>();
            builder.Services.AddScoped<IRentService, RentService>();
            builder.Services.AddScoped<IRequestService, RequestService>();



            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });


            var app = builder.Build();

            app.UseCors();
            var dbInitializer = new DataBaseInitializer(connectionString);
            dbInitializer.Initialize();



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
