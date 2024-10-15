
using DVDRental.Data;
using DVDRental.Repositories;

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



            var app = builder.Build();

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
