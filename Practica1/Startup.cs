using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMediaCore.Entidades.CustomEntities;
using SocialMediaCore.Interfaces;
using SocialMediaCore.Services;
using SocialMediaInfraestructure.Data;
using SocialMediaInfraestructure.Filters;
using SocialMediaInfraestructure.Interfaces;
using SocialMediaInfraestructure.Repositories;
using SocialMediaInfraestructure.Services;
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Para negar las con validaciones de datos de la api
            services.AddControllers(options =>
                {
                    options.Filters.Add<GlobalExceptionFilter>();
                })
                .AddNewtonsoftJson(options => {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    //options.SuppressModelStateInvalidFilter = true;   //esto es para impedir que apicontroller controle la validacion del controlador
                });

            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));

            //para conectar con sqlserver
            services.AddDbContext<SocialMediaContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("SocialMedia"))
            );

            //Para resolver inyeccion de dependencias
            //siempre que hayan peticiones a post pues se realizara la injeccion de dependencia
            services.AddTransient<IPostService, PostService>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton <IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absolutedUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absolutedUri);
            });

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
