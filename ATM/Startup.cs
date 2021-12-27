using ATM.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ATM.CLI
{
    public class Startup
    {
        //public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        //{

        //}
        void ConfigureServices(IServiceCollection services)
        {
            
            services.AddScoped<IStaffService, StaffService>();

            //services.AddRazorPages();
        }
    }
}
