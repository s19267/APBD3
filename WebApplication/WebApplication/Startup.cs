using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApplication.DAL;
using WebApplication.Midlewares;
using WebApplication.Services;

namespace WebApplication
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
            services.AddScoped<IdbService2, EfDbService>();
            services.AddDbContext<s19267Context>(options =>
            {
                options.UseSqlServer("Data Source=db-mssql;Initial Catalog=s19267;Integrated Security=True");
            });
            
            services.AddSingleton<IStudentsDbService, MssqlStudentsDbService>();
            services.AddSingleton<DAL.IDbService,MssqlDbService>();
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseMiddleware<LoggingMiddleware>();

            // app.Use(async (context, next) =>
            // {
            //     if (context.Request.Headers.ContainsKey("Index"))
            //     {
            //         String index = context.Request.Headers["Index"].ToString();
            //         using (SqlConnection con =
            //             new SqlConnection("Data Source=db-mssql;Initial Catalog=s19267;Integrated Security=True"))
            //         using (SqlCommand com = new SqlCommand())
            //         {
            //             com.Connection = con;
            //             con.Open();
            //             com.CommandText = "select * from Student where IndexNumber=@Index";
            //             com.Parameters.AddWithValue("Index", index);
            //             SqlDataReader dr = com.ExecuteReader();
            //             if (!dr.Read())
            //             {
            //                 dr.Close();
            //                 context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //                 await context.Response.WriteAsync("student nie istnieje");
            //                 return;
            //             }
            //
            //             dr.Close();
            //             await next();
            //         }
            //     }
            //
            //     context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //     await context.Response.WriteAsync("brak autoryzacji");
            //     return;
            // });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}