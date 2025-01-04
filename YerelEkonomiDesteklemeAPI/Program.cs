using Microsoft.EntityFrameworkCore;
using LocalEconomyApi.DataAccess.Abstract;
using LocalEconomyApi.DataAccess.Concrete;
using LocalEconomyApi.Data;
using System;
using LocalEconomyApi.Abstract.business;
using LocalEconomyApi.Concrete.business;
using LocalEconomyApi.Concrete;
using LocalEconomyApi.Abstract;
using LocalEconomyApi.DataAccess.Concrete.EntityFramework;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // 1. DbContext Ayar�
        builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


        // 2. Business Servis ve Repository Ba��ml�l�klar�
        builder.Services.AddScoped<IBusinessService, BusinessService>();
        builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();



        // 3. CORS (�ste�e Ba�l�)
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod());
        });

        // 4. Controller'lar� Ekleyin
        builder.Services.AddControllers();

        // 5. Swagger (API Dok�mantasyonu)
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // 6. CORS Kullan�m�
        app.UseCors("AllowAll");

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}