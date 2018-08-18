using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApiCore.Constraints;
using WebApiCore.Data;
using WebApiCore.Filters;

namespace WebApiCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //database baglantisi
            services.AddDbContext<DataContext>
            (x => x.UseSqlServer(Configuration.
                GetConnectionString("DefaultConnection")));

            //constraint tanımlaması
            services.Configure<RouteOptions>(routeOptions =>
            {
                routeOptions.ConstraintMap.Add("lastletter", typeof(LastLetter));
            });

            services.AddMvc(options =>
                {
                    options.Filters.Add(new ModelValidationAttribute());
                }).
                AddJsonOptions(options =>
               {
                   //loop referans
                   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                   // raw olarakta düzgün gözükmesi için ekledik.
                   options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
               }).
                AddXmlDataContractSerializerFormatters();//xml desteğide vermek için kullandık,bunun için modelin üstünde annotation eklemek gerekiyor.(döngüsel refereans için)

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
