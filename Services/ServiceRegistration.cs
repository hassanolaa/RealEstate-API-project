using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using realstate.DAL.Repositories.Implementations;
using realstate.DAL.Repositories.Interfaces;
using realstate.Profiles;
using realstate.Services.Repositories.Implementations;
using realstate.Services.Repositories.Interfaces;


namespace realstate.Services
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // AutoMapper  
               services.AddAutoMapper(config => config.AddMaps(typeof(MappingProfile).Assembly));
           
            // Repositories & Unit of Work  
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();


            // Application Services  
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<INotificationService, NotificationService>();

            return services;
        }
    }
}
    