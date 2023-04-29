﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;
using Todo.Domain.Utils;
using Todo.Infra.Contexts;
using Todo.Infra.Mappers;
using Todo.Infra.Repositories;
using Todo.Infra.Utils;

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