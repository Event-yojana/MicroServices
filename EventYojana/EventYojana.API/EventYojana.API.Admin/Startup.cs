using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EventYojana.Infrastructure.Core;
using EventYojana.Infrastructure.Core.Common;
using EventYojana.Infrastructure.Core.Filters;
using EventYojana.Infrastructure.Core.Filters.SwaggerFilters;
using EventYojana.Infrastructure.Core.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace EventYojana.API.Admin
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
            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            //    options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ValidIssuer = Configuration["Jwt:Issuer"],
            //            ValidAudience = Configuration["Jwt:Issuer"],
            //            ClockSkew = TimeSpan.Zero,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            //        };
            //    });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddJsonOptions(o => {
                o.JsonSerializerOptions.PropertyNamingPolicy = null;
                o.JsonSerializerOptions.DictionaryKeyPolicy = null;
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                });
            });
            services.AddApplicationInsightsTelemetry();
            services.AddControllers();

            //Call service injection
            IocConfig.ConfigureServices(ref services, Configuration);
            Infrastructure.Repository.IocConfig.ConfigureServices(ref services);
            DataAccess.IocConfig.ConfigureServices(ref services);
            BusinessLayer.IocConfig.ConfigureServices(ref services);

            services.Configure<AppKeyConfig>(Configuration.GetSection("AppKeyConfig"));

            services.AddHttpContextAccessor();

            //Register swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Event Yojana Admin Service", Version = "v1" });

                const string idSecurity = "AppKey";
                c.AddSecurityDefinition(idSecurity, new OpenApiSecurityScheme
                {
                    Name = Configuration.GetValue<string>("AppKeyConfig:HeaderName"),
                    In = ParameterLocation.Header,
                    Description = "Individual Application Key.",
                    Type = SecuritySchemeType.ApiKey
                });

                var xmlFileWeb = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileWeb), true);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = idSecurity}
                        },
                        new string[] { }
                    }
                });

                c.OperationFilter<CustomeBindingFilters>();
                c.OperationFilter<UnSecureHeaderFilter>();
                c.OperationFilter<GenericResponseFilter>();
                c.OperationFilter<FileOperationFilter>();
                c.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string swaggerEndpoint = "/swagger/v1/swagger.json";
            string pathBase = Configuration.GetValue<string>("PathBase");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<JwtMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseSwagger(c => {
                c.SerializeAsV2 = true;
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) => {
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{pathBase}" } };
                });
            });
            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            );
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint(swaggerEndpoint, "Event Yojana Service V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMiddleware<CheckAppKeyMiddleware>();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
