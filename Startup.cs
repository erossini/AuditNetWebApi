﻿using System;
using Projects.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Projects.Providers.Database;

namespace Projects
{
    public class Startup
    {
        private string connString = "Data Source=nl1-wsq1.a2hosting.com;Initial Catalog=forumpur_fortests;Integrated Security=False;User ID=forumpur_tests;Password=Enrico!1975*;Connect Timeout=60;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultipleActiveResultSets=true;MultiSubnetFailover=False";
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<IValuesProvider, ValuesProvider>();

            // TODO: Configure your context connection
            services.AddDbContext<MyContext>(_ => _.UseSqlServer(connString));
            services
                .ConfigureAudit()
                .AddMvc(options => 
                { 
                    options.AddAudit();
                    options.EnableEndpointRouting = false;
                });

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor contextAccessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) => {
                context.Request.EnableBuffering();
                await next();
            });

            app.UseAuditMiddleware();
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
            app.UseAuditCorrelationId(contextAccessor);
        }
    }
}
