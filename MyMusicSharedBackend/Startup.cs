using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using AutoMapper;
using KylyOrderPicking.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyMusicSharedBackend.Core;
using MyMusicSharedBackend.Infrastructure;
using MyMusicSharedBackend.Services;

namespace MyMusicSharedBackend
{
    /// <summary>
    /// Class for Startup of the application
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="configuration">Configuration file Settings</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration file Settings
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Services to add to the container</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });

            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "My Music Shared - API",
                    Description = "API for My Music Shared",
                    Contact = new OpenApiContact
                    {
                        Name = "Maicon Gabriel Friedel",
                        Email = "maicon.friedel@gmail.com",
                        Url = new Uri("https://github.com/maiconfriedel"),
                    },
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //Registrar as politicas de acesso
            services.AddAuthorization(options =>
            {
                options.AddPolicy("users.read",
                    policy => policy.Requirements.Add(new HasScopeRequirement("users.read", "MyMusicShared")));

                options.AddPolicy("users.write",
                    policy => policy.Requirements.Add(new HasScopeRequirement("users.write", "MyMusicShared")));
            });

            // register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("Security").GetSection("JWTSecurityKey").Value);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidIssuers = new List<string> { "MyMusicShared" },
                };
            });

            // add the Infrastructure and Core layers services
            services.AddMyMusicSharedBackendCore();
            services.AddMyMusicSharedBackendInfrastructure();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The App Builder</param>
        /// <param name="env">Environment details</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            _ = app.UseExceptionHandler(
               builder =>
               {
                   builder.Run(
                   async context =>
                   {
                       context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                       context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                       context.Response.Headers.Add("Content-Type", "application/json");

                       var error = context.Features.Get<IExceptionHandlerFeature>();

                       if (error != null)
                       {
                           var exception = error.Error;

                           List<string> errors = new List<string>();

                           errors.Add(exception.Message);
                           while (exception.InnerException != null)
                           {
                               exception = exception.InnerException;
                               errors.Add(exception.Message);
                           }

                           var response = new
                           {
                               Success = false,
                               Message = "Erro interno",
                               Errors = errors
                           };

                           JsonSerializerOptions serializerOptions = new JsonSerializerOptions
                           {
                               WriteIndented = true,
                               PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                           };
                           await context.Response.WriteAsync(JsonSerializer.Serialize(response, serializerOptions)).ConfigureAwait(false);
                       }
                   });
               });

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Music Shared - API - v1");
                c.RoutePrefix = string.Empty;
            });

            // migrates the database on the start of the application
            app.MyMusicSharedMigrateDatabase();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}