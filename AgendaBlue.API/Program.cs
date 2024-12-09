
using AgendaBlue.Manager.Interfaces;
using AgendaBlue.Manager;
using AgendaBlue.Domain.DAO;
using AgendaBlue.Domain.Entities;
using AgendaBlue.Repository;
using AgendaBlue.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Injen��o de Dependencia
builder.Services.AddScoped<IDAO<User>, UserDAO>();
builder.Services.AddScoped<IDAO<Scheduling>, BaseDAO<Scheduling>>();
builder.Services.AddScoped<IDAO<Barber>, BaseDAO<Barber>>();
builder.Services.AddScoped<IDAO<Sessions>, BaseDAO<Sessions>>();
builder.Services.AddScoped<IDAO<Shavy>, BaseDAO<Shavy>>();
builder.Services.AddScoped<IDAO<Horary>, BaseDAO<Horary>>();
builder.Services.AddScoped<IDAO<Service>, BaseDAO<Service>>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var OpenCors = "_openCors";

// Configuring cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: OpenCors,
                        builder =>
                        {
                            builder.AllowAnyOrigin();
                            builder.WithMethods("PUT", "DELETE", "GET", "POST");
                            builder.AllowAnyHeader();
                        });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthorization();
app.UseCors(OpenCors);
app.MapControllers();

app.Run();
