using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
                options.AddPolicy("read",
                    policy => policy.Requirements.Add(new HasScopeRequirement("read", "MyMusicShared")));

                options.AddPolicy("write",
                    policy => policy.Requirements.Add(new HasScopeRequirement("write", "MyMusicShared")));
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
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddDbContext<Database.MyMusicSharedDbContext>();
            services.AddSingleton<TokenService>();
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

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Music Shared - API - v1");
                c.RoutePrefix = string.Empty;
            });

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