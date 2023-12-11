namespace ContactsAPI.Services
{
    public static class ServicesDIExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicationService, ApplicationService>();
            return services;
        }
    }
}
