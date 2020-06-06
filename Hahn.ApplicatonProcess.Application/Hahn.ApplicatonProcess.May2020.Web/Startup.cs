using System;
using System.IO;
using System.Reflection;
using Hahn.ApplicatonProcess.May2020.Data;
using Hahn.ApplicatonProcess.May2020.Domain;
using Hahn.ApplicatonProcess.May2020.Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Hahn.ApplicatonProcess.May2020.Web
{
    /// <summary>
    /// The startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Create an instance of the object
        /// </summary>
        /// <param name="configuration">Configuration object</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Contains configuration data
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container. 
        /// </summary>
        /// <param name="services">Collection of services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMemoryCache();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowCors", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            // I tried the following but it didn't work.
            // Therefore, added manual validation on the controller.
            //.AddFluentValidation(o => o.RunDefaultMvcValidationAfterFluentValidationExecutes = false);

            // Usefull when the app is running behind a reverse proxy/ingress controller
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddDbContext<DatabaseContext>(o => o.UseInMemoryDatabase("ApplicantDb"));
            services.AddScoped<IApplicantRepository, ApplicantRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Applicant API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                
                c.IncludeXmlComments(xmlPath);

                xmlPath = Path.Combine(AppContext.BaseDirectory, "Hahn.ApplicatonProcess.May2020.Domain.xml");
                c.IncludeXmlComments(xmlPath);
            });

            services.AddHttpClient<CountryService>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
        /// </summary>
        /// <param name="app">App builder object</param>
        /// <param name="env">Hosting environment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowCors");
            app.UseRouting();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Applicant API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
