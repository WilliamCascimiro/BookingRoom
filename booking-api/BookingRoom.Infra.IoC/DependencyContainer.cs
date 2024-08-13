using BookingRoom.Application.Services;
using BookingRoom.Domain.Interfaces;
using BookingRoom.Infra.Data.Context;
using BookingRoom.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingRoom.Infra.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependencyInjectionRepository();
            services.AddDependencyInjectionService();
            services.AddAutoMapperConfig();
            services.AddContext(configuration);

            return services;
        }

        private static IServiceCollection AddDependencyInjectionRepository(this IServiceCollection services)
        {
            services.AddScoped<BookingDbContext>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IRoomTimeSlotRepository, RoomTimeSlotRepository>();
            services.AddScoped<IUserRepository, UsuarioRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static IServiceCollection AddDependencyInjectionService(this IServiceCollection services)
        {
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomTimeSlotService, RoomTimeSlotService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        private static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }

        private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookingDbContext>(options => 
                options.UseSqlServer(
                    configuration.GetConnectionString("BookingDb"))
            );

            return services;
        }
    }
}
