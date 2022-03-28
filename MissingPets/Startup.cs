using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MissingPets.Models;
using MissingPets.MongoDB;
using MissingPets.Services;
using MissingPets.Settings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissingPets
{
    public class Startup
    {
        private const string AllowedOriginSetting = "AllowedOrigin";
        private ServiceSettings _serviceSettings;
        readonly string MiCors = "MiCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            _serviceSettings = Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

            services.AddCors(options => {
                options.AddPolicy(name: MiCors,
                                    builder =>
                                    {
                                        builder.WithMethods("*");
                                        builder.WithHeaders("*");
                                        builder.WithOrigins("*");
                                    });
            });


            services.AddMongo()
                .AddMongoRepository<Pet>("pets")
                .AddMongoRepository<User>("user");

            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });
            //.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // CONFIGURE JWT
            var appSettings = Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            var llave = Encoding.ASCII.GetBytes(appSettings.Secret);

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
                    IssuerSigningKey = new SymmetricSecurityKey(llave),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            services.AddControllers();
            services.AddScoped<IUserService, UserService>();
                
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MissingPets", Version = "v1" });
            });
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MissingPets v1"));
                app.UseCors(builder =>
                {
                    builder.WithOrigins(Configuration[AllowedOriginSetting])
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
