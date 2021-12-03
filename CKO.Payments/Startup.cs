using System;
using CKO.Payments.Bank.Client;
using CKO.Payments.Bank.Client.Interface;
using CKO.Payments.Bank.Models.Interfaces;
using CKO.Payments.Bank.Models.Nakatomi;
using CKO.Payments.BL.Services;
using CKO.Payments.BL.Services.Interfaces;
using CKO.Payments.DAL;
using CKO.Payments.DAL.Interfaces;
using CKO.Payments.Data;
using CKO.Payments.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CKO.Payments
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CKO Payments API", Version = "v1" });
            });

            services.AddDbContext<CkoContext>(s => s.UseSqlServer(Configuration.GetConnectionString("CkoContext")));
            services.AddHttpClient<IHttpRequests, HttpRequests>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<INakatomiConfig>((s) =>
            {
                var nakatomiIndex = "Banks:Nakatomi";

                return new NakatomiConfig(
                        Configuration.GetValue<string>($"{nakatomiIndex}:BaseUrl"),
                        Configuration.GetValue<string>($"{nakatomiIndex}:Username"),
                        Configuration.GetValue<string>($"{nakatomiIndex}:Password"));
            });

            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IMerchantsService, MerchantsService>();
            services.AddTransient<ITransactionsService, TransactionsService>();
            services.AddTransient<IBankClient, NakatomiClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CkoContext dbContext)
        {
            dbContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<JwtAuthenticationMiddleware>();

            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "CKO Payments API v1");
                x.RoutePrefix = String.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
