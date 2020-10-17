using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMediaCore.Interfaces;
using SocialMediaCore.Services;
using SocialMediaInfraestructure.Data;
using SocialMediaInfraestructure.Filters;
using SocialMediaInfraestructure.Repositories;
using System;

namespace Practica1
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
            //agregando automapping como servicio
            services.AddAutoMapper( AppDomain.CurrentDomain.GetAssemblies() );

            //Para negar las con validaciones de datos de la api
            services.AddControllers()
                .AddNewtonsoftJson(options => {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;  
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    //options.SuppressModelStateInvalidFilter = true;   //esto es para impedir que apicontroller controle la validacion del controlador
                });

            //para conectar con sqlserver
            services.AddDbContext<SocialMediaContext>( options => 
                options.UseSqlServer(Configuration.GetConnectionString("SocialMedia"))
            );

            //Para resolver inyeccion de dependencias
            //siempre que hayan peticiones a post pues se realizara la injeccion de dependencia
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            //Registrar un filtro de forma global y fluentvalidator
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
