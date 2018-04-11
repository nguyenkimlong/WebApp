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
using DAL.Infrastructure;
using Business.IService;
using Business.Service;

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
            var connection = @"Server=.;Database=ShopMobile;user id=sa;password=123456;Trusted_Connection=True;MultipleActiveResultSets=true";
            services.AddDbContext<ShopDbContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<ShopDbContext>(options => options.UseInMemoryDatabase().UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));


            services.AddMvc();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPostCategoryService, PostCategoryService>();

        

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
