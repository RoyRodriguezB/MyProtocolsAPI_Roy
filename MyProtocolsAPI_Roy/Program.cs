using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyProtocolsAPI_Roy.Models;

namespace MyProtocolsAPI_Roy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //SEMANA3
            //leer la etiqueta CNNSTR de appsetting,jsons para configurar la conexion
            //a la BD
            var CnnStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("CNNSTR"));

            CnnStrBuilder.Password = "123456";

            string cnnStr = CnnStrBuilder.ConnectionString;

            //conectamos el proyecto a la BD usando cnnstr
            builder.Services.AddDbContext<MyProtocolsDBContext>(options => options.UseSqlServer(cnnStr));


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthorization();

            
            app.MapControllers();

            app.Run();
        }
    }
}