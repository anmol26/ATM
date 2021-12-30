using ATM.Repository;
using ATM.Repository.Models;
using ATM.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.CLI
{
    public static class DIContainerBuilder
    {
        public static IServiceProvider Build()
        {
            ServiceCollection services = new ServiceCollection();

            services
                .AddSingleton<ICustomerService, CustomerService>()
                .AddSingleton<IStaffService, StaffService>()
                .AddSingleton<ICommonService, CommonServices>()
                .AddSingleton<IStaffRepository, StaffRepository>()
                .AddSingleton<ICustomerRepository, CustomerRepository>()
                .AddDbContext<ATMContext>(options => options.UseSqlServer("Server= ANMOL\\SQLEXPRESS;Database=Banking;Trusted_Connection=True;"));

            return services.BuildServiceProvider();

        }
    }
}
