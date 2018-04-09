using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Microsoft.AspNetCore.Http;

namespace WebApp
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
            services.AddMvc();

            var connection = @"Server=DESKTOP-QJ6RQTH;Database=ShopMobile;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<ShopDbContext>(options => options.UseSqlServer(connection));

            //services.AddDbContext<ShopDbContext>(options =>
            // options.UseSqlServer(Configuration.GetConnectionString("ShopDatabase")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {           
                app.UseDeveloperExceptionPage();
            }          

            app.UseStaticFiles();

            app.UseMvc();

            app.Run(async(context) => {
                await context.Response.WriteAsync("Hello Word!");
            });
        }
    }
}
