using System.Text;
using BackAppPersonal.Application.Services;
using BackAppPersonal.Domain.Intefaces;
using BackAppPersonal.Infrastructure.Data;
using BackAppPersonal.Infrastructure.Hashing;
using BackAppPersonal.Infrastructure.Http;
using BackAppPersonal.Infrastructure.Minio;
using BackAppPersonal.Infrastructure.Repository;
using BackAppPersonal.Utils;
using BackAppPromo.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
builder.Services.AddScoped<IAlunoRepository , AlunoRepository>();
builder.Services.AddScoped<IJwtToken, JwtToken>();
builder.Services.AddScoped<ISenhaHash, SenhaHash>();
builder.Services.AddScoped<AlunoService>();
builder.Services.AddScoped<ValidadorUtils>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<AcademiaService>();
builder.Services.AddScoped<PersonalService>();
builder.Services.AddScoped<EnderecoService>();
builder.Services.AddScoped<AcademiaPersonalService>();
builder.Services.AddScoped<AuthService>();
// Configuração do Minio
builder.Services.AddSingleton<IMinioStorage>(provider =>
    new Storage(
        endpoint: builder.Configuration["Minio:Endpoint"],
        accessKey: builder.Configuration["Minio:AccessKey"],
        secretKey: builder.Configuration["Minio:SecretKey"]
    )
);
// Configuração do JWT
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {seu-token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
// Configuração do CORS
string corsPolicyName = "AllowAnyOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configuração da autenticação com JWT
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)
            ),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // Sem tolerância para expiração
        };
    });
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(corsPolicyName);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
