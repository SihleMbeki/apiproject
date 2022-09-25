using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        private  IConfiguration _confi { get; }
        public Startup(IConfiguration confi)
        {
            _confi = confi;
        }
        readonly string allowSpecificOrigins = "_allowSpecificOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
           //  services.AddCors();

           services.AddCors(options =>
            {
                options.AddPolicy(allowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });
services.AddCors(c =>  
            {  
                c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://localhost"));  
            });
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<ITokenService,TokenService>();
;            services.AddDbContext<DataContext>(options=>{
                options.UseSqlite(_confi.GetConnectionString("DefaultConnection"));
            });
           
            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options
                =>{
                    options.TokenValidationParameters=new TokenValidationParameters{
                        ValidateIssuerSigningKey=true,
                        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confi["TokenKey"])),
                            ValidateIssuer=false,
                            ValidateAudience=false,
                    };
                }
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*app.UseCors(builder => builder
        .WithOrigins("http://localhost/view", "http://localhost:5001")
        .AllowAnyMethod()
        .AllowCredentials()
        .WithHeaders("Accept", "Content-Type", "Origin", "X-My-Header"));*/
        app.UseCors(allowSpecificOrigins);
        app.UseCors(options=>options.WithOrigins("http://localhostdd"));  
        //app.UseCors(options => options.AllowAnyOrigin());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost"));
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
