using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POC.Core;
using POC.Core.Customers.CreateCustomer;
using POC.Core.Customers.GetCustomers;
using POC.Infrastructure;
using POC.Infrastructure.Commands;
using POC.Infrastructure.Requests;
using Swashbuckle.AspNetCore.Swagger;

namespace POC.API
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<DbContext, POCContext>(options => options.UseSqlServer(Configuration.GetConnectionString("POCViskoTeepak")));

            services.AddTransient<IRequestDispatcher, RequestDispatcher>();
            services.RegisterDerivedTypes(typeof(IRequestHandler<,>), typeof(GetCustomersRequestHandler).Assembly);

            services.AddTransient<ICommandDispatcher, CommandDispatcher>();
            services.RegisterDerivedTypes(typeof(ICommandHandler<>), typeof(CreateCustomerCommandHandler).Assembly);

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder
                            .WithOrigins(Configuration["AllowedOrigins"])
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "POC API", Version = "v1" });
            });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "POC API V1");
            });

            app.UseCors(MyAllowSpecificOrigins);

            app.UseMvc();
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static void RegisterDerivedTypes(this IServiceCollection services, Type baseType, Assembly assembly, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            foreach (var type in assembly.DefinedTypes)
            {
                foreach (var baseTypeImplementation in type.GetGenericInterfaceImplementations(baseType))
                {
                    var registrationType = baseType.MakeGenericType(baseTypeImplementation.GenericTypeArguments);
                    services.Add(new ServiceDescriptor(registrationType, type, lifetime));
                }
            }
        }

        public static List<Type> GetGenericInterfaceImplementations(this Type type, Type baseType)
        {
            return type.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == baseType).ToList();
        }
    }
}
