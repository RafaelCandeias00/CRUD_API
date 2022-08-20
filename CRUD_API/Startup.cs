using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CRUD_API.Src.Context;
using CRUD_API.Src.Repository;
using CRUD_API.Src.Repository.Implements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CRUD_API
{
    public class Startup
    { 
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuração do banco de dados
            if (Configuration["Enviroment:Start"] == "PROD")
            {
                services.AddEntityFrameworkNpgsql()
                    .AddDbContext<CrudContexto>(
                    opt =>
                    opt.UseNpgsql(Configuration["ConnectionStringsProd:DefaultConnection"]));
            }
            else
            {
                services.AddDbContext<CrudContexto>(opt => opt.UseSqlServer(Configuration["ConnectionStringsDev:DefaultConnection"]));
            }            

            // Adicionando escopo do repositório
            services.AddScoped<IUsuario, UsuarioRepositorio>();

            // Adicionando Controladores
            services.AddCors();
            services.AddControllers();

            // Configuração Swagger
            services.AddSwaggerGen(
                s => 
                {
                    s.SwaggerDoc("v1", new OpenApiInfo { Title = "CRUD Usuario", Version = "v1"});

                    s.AddSecurityDefinition(
                        "Bearer",
                        new OpenApiSecurityScheme(){
                            Name = "Authorization", 
                            Type = SecuritySchemeType.ApiKey, 
                            Scheme = "Bearer", 
                            BearerFormat = "JWT", 
                            In = ParameterLocation.Header, 
                            Description = "JWT authorization header utiliza: Bearer + JWT Token",
                        }
                    );

                    s.AddSecurityRequirement(
                        new OpenApiSecurityRequirement{
                            {
                                new OpenApiSecurityScheme{
                                    Reference = new OpenApiReference{
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new List<string>()
                            }
                        }
                    );

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; 
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); 
                    s.IncludeXmlComments(xmlPath);
                }
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CrudContexto contexto)
        {
            if (env.IsDevelopment())
            {
                contexto.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();

                app.UseSwagger(); 
                app.UseSwaggerUI(c => { 
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRUD v1"); 
                    c.RoutePrefix = string.Empty; 
                });
            }

            contexto.Database.EnsureCreated();
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRUD v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseCors(c => c 
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
