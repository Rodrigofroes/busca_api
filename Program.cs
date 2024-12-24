using BackAppPersonal.Application.Services;
using BackAppPersonal.Domain.Intefaces;
using BackAppPersonal.Infrastructure.Data;
using BackAppPersonal.Infrastructure.Hashing;
using BackAppPersonal.Infrastructure.Http;
using BackAppPersonal.Infrastructure.Repository;
using BackAppPersonal.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAcademiaRepository, AcademiaRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddHttpClient<IOpenStreetMap, OpenStreetMap>();
builder.Services.AddScoped<IOpenStreetMap, OpenStreetMap>();
builder.Services.AddScoped<IPersonalRepository, PersonalRespository>();
builder.Services.AddScoped<IUsuarioRespository, UsuarioRepository>();
builder.Services.AddScoped<IAcademiaPersonalRepository, AcademiaPersonalRepository>();
builder.Services.AddScoped<ISenhaHash, SenhaHash>();
builder.Services.AddScoped<ValidadorUtils>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<AcademiaService>();
builder.Services.AddScoped<PersonalService>();

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
