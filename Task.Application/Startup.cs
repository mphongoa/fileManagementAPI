using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Task.Infrastructure.Database;
using Task.Infrastructure.Services.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
 
using Task.Infrastructure.Services.Files;
using Task.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Task.Application
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

            services.AddControllers().AddNewtonsoftJson();

            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("AppDatabase")));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFileService, FileService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(OptionsBuilderConfigurationExtensions =>
            {
                OptionsBuilderConfigurationExtensions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                    .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "TASK API", Version = "v1" , Description=" api end points"}); });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();
               
        }
        


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task API V1"); });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseAuthentication();
            app.UseCors("AllowAllHeaders");
            
        
        }
    }
}
