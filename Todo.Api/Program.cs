using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Todo.Infra.Contexts;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Todo.Api.Utils;
using Todo.Api.Contracts;
using Todo.Domain.Repositories;
using Todo.Infra.Repositories;
using Todo.Domain.Utils;
using Todo.Infra.Utils;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var secret = builder.Configuration.GetValue("SecretKey", "") ?? Guid.NewGuid().ToString();
var key = Encoding.ASCII.GetBytes(secret);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TodoDBContext>(x => x.UseNpgsql(builder.Configuration.GetValue("ConnectionString", "")));
builder.Services.AddTransient<ITokenService, TokenService>(x => new TokenService(key));
builder.Services.AddTransient<IHasher, Hasher>(x => new Hasher(secret));

builder.Services.AddTransient<IBoardRepository, BoardRepository>();
builder.Services.AddTransient<IColumnRepository, ColumnRepository>();
builder.Services.AddTransient<ITodoItemRepostory, TodoItemRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[]{}
        }
    });
});

builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
)
.AddJwtBearer(
    options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    }
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
