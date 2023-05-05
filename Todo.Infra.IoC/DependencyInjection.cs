using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;
using Todo.Domain.Utils;
using Todo.Infra.Data.Contexts;
using Todo.Infra.Data.Mappers;
using Todo.Infra.Data.Repositories;
using Todo.Infra.Data.Utils;

namespace Todo.Infra.IoC;

public static class DependencyInjection
{
    // ReSharper disable once UnusedMethodReturnValue.Global
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TodoDBContext>(x =>
        {
            x.UseLazyLoadingProxies();
            x.UseNpgsql(configuration.GetSection("CONNECTION_STRING").Value,
                b => b.MigrationsAssembly(typeof(TodoDBContext).Assembly.FullName));
        });

        // Utils
        services.AddTransient<IHasher, Hasher>();
        services.AddTransient<IMailer, ConsoleMailer>();
        services.AddTransient<ITokenService, TokenService>(x =>
            new TokenService(configuration.GetSection("SECRET_KEY").Value ?? Guid.NewGuid().ToString()));
        services.AddTransient<IFileStorage, LocalFileStorage>();

        // Repositories
        services.AddTransient<IBoardRepository, BoardRepository>();
        services.AddTransient<IColumnRepository, ColumnRepository>();
        services.AddTransient<ITodoItemRepository, TodoItemRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IRecoverCodeRepository, RecoverCodeRepository>();
        services.AddTransient<IInviteRepository, InviteRepository>();

        // Handlers
        services.AddTransient<BoardHandler>();
        services.AddTransient<ColumnHandler>();
        services.AddTransient<UserHandler>();
        services.AddTransient<ItemHandler>();

        // Mappers
        services.AddAutoMapper(typeof(BoardMapper));
        services.AddAutoMapper(typeof(ItemMapper));
        services.AddAutoMapper(typeof(UserMapper));
        services.AddAutoMapper(typeof(ColumnMapper));

        return services;
    }
}