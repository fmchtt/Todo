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
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var secret = builder.Configuration.GetValue("SECRET_KEY", "") ?? Guid.NewGuid().ToString();
var key = Encoding.ASCII.GetBytes(secret);

var staticPath = Path.Join(builder.Environment.ContentRootPath, "wwwroot");
if (!Path.Exists(staticPath))
{
    Directory.CreateDirectory(staticPath);
}

var databasePath = Path.Join(builder.Environment.ContentRootPath, "database");
if (builder.Environment.IsProduction() && !Path.Exists(databasePath))
{
    Directory.CreateDirectory(databasePath);
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TodoDBContext>(x =>
{
    x.UseLazyLoadingProxies();
    if (builder.Environment.IsProduction())
    {
        x.UseSqlite("Data Source=./database/Todo.db");
    }
    else
    {
        x.UseNpgsql(builder.Configuration.GetValue("CONNECTION_STRING", ""));
    }
});

builder.Services.AddTransient<ITokenService, TokenService>(x => new TokenService(key));
builder.Services.AddTransient<IHasher, Hasher>(x => new Hasher(secret));
builder.Services.AddTransient<IFileStorage, LocalFileStorage>(x => new LocalFileStorage(staticPath));

builder.Services.AddTransient<IBoardRepository, BoardRepository>();
builder.Services.AddTransient<IColumnRepository, ColumnRepository>();
builder.Services.AddTransient<ITodoItemRepostory, TodoItemRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IRecoverCodeRepository, RecoverCodeRepository>();

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

app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        staticPath
    ),
    RequestPath = ""
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TodoDBContext>();
    db.Database.Migrate();
}

app.Run();
