using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATM.Services;
using ATM.Repository;
using ATM.Repository.Models;
using Microsoft.EntityFrameworkCore;
namespace ATM.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ATMContext>(i=>i.UseSqlServer("Server= ANMOL\\SQLEXPRESS;Database=Banking;Trusted_Connection=True;"));
            services.AddScoped<ICommonService, CommonServices>();
            services.AddScoped<ICustomerService,CustomerService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
