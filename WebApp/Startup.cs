using BLL;
using BLL.Contracts;
using DAL;
using DAL.Contracts;
using dm = DAL.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<dm.ToDoContext>(options => options.UseInMemoryDatabase());
            services.AddScoped<ICategoryProcess, CategoryProcess>();
            services.AddScoped<ICategoryData, CategoryData>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            //var context = app.ApplicationServices.GetService<dm.ToDoContext>();
            //AddTestData(context);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        //private static void AddTestData(dm.ToDoContext context)
        //{
        //    var c1 = new dm.Category()
        //    {
        //        Id = 1,
        //        Name = "Category 1"
        //    };

        //    context.Categories.Add(c1);

        //    var c2 = new dm.Category()
        //    {
        //        Id = 2,
        //        Name = "Category 2"
        //    };

        //    context.Categories.Add(c2);

        //    context.SaveChanges();
        //}
    }
}
