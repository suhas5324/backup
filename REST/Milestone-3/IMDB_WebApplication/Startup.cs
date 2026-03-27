using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Implementations;
using IMDB_WebApplication.Repositories.Interfaces;
using IMDB_WebApplication.Services.Implementations;
using IMDB_WebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace IMDB_WebApplication
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
            var secretKey = Configuration["JWT:Secret"];
            var issuer = Configuration["JWT:ValidIssuer"];
            var audience = Configuration["JWT:ValidAudience"];

            var key = Encoding.UTF8.GetBytes(secretKey);

            services.AddControllers();
            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IMDB_WebApplication", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton<IActorService, ActorService>();
            services.AddSingleton<IActorRepository, ActorRepository>();
            services.AddSingleton<IGenreService, GenreService>();
            services.AddSingleton<IGenreRepository, GenreRepository>();
            services.AddSingleton<IMovieService, MovieService>();
            services.AddSingleton<IMovieRepository, MovieRepository>();
            services.AddSingleton<IProducerService, ProducerService>();
            services.AddSingleton<IProducerRepository, ProducerRepository>();
            services.AddSingleton<IReviewService, ReviewService>();
            services.AddSingleton<IReviewRepository, ReviewRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IMDB_WebApplication v1"));
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
