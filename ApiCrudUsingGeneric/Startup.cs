using ApiCrudUsingGeneric.IService;
using ApiCrudUsingGeneric.Models;
using ApiCrudUsingGeneric.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric
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
            services.AddControllers();

            /*api versioning-start*/
            services.AddApiVersioning(x => {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                /*With the AssumeDefaultVersionWhenUnspecified and DefaultApiVersion properties, we are accepting version 1.0 if a client doesn’t specify the version of the API*/
                x.ReportApiVersions = true;
                x.ApiVersionReader = new Microsoft.AspNetCore.Mvc.Versioning.HeaderApiVersionReader("x-frapper-api-version");
            });
            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";
                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });
            /*api versioning-end*/

            #region swagger
            //services.AddSwaggerGen();
            /*Register the Swagger generator-start*/
            //services.AddSwaggerGen();
            services.AddSwaggerGen(
                (options) =>
                {
                    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
                    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v2" });
                }
                );
            /*Register the Swagger generator-end*/
            #endregion


            /*var connection = Configuration.GetConnectionString("DatabaseConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
            services.Configure<AppSettings>(Configuration.GetSection("ApplicationSettings"));
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnStr")));*/
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));


            services.AddScoped<IGenericService<Student>, StudentService>();
            services.AddScoped<IGenericService<Teacher>, TeacherService>();

            services.AddTransient<IMoviesQueries, MoviesQueries>();
            services.AddTransient<IUnitOfWorkEntityFramework, UnitOfWorkEntityFramework>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IGenericQueries<Employee>, GenericQueries<Employee>>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ApplicationDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            db.Database.EnsureCreated();
            app.UseSwagger();
            /*app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API v2");

                c.RoutePrefix = string.Empty;
            });*/
            /*specify the Swagger JSON endpoint-start*/
            app.UseSwaggerUI(c =>
            {
                //c.DefaultModelsExpandDepth(-1);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API v2");
                c.RoutePrefix = string.Empty;
            });
            /*specify the Swagger JSON endpoint-end*/
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
