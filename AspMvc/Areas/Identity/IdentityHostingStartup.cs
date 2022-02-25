using System;
using AspMvc.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(AspMvc.Areas.Identity.IdentityHostingStartup))]
namespace AspMvc.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AspMvcIdentityDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AspMvcIdentityDbContextConnection")));

                /*services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<AspMvcIdentityDbContext>();*/
            });
        }
    }
}