using FluentValidation;
using Multiple_Desk.Repository.Implementation;
using Multiple_Desk.Repository.Interface;
using Multiple_Desk.Services.Implementation;
using Multiple_Desk.Services.Interfaces;
using Multiple_Desk.Settings;
using System.Reflection;

public static class DependencyInjection
{
    public static IServiceCollection ApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        //dbcontext
       services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        services.AddSingleton<ApplicationDbContext>();


        //session
        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // optional
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        //fluent validation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        //repository
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        
        //services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<FileService>();

        return services;
    }
}

