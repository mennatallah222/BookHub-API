
using API.Core;
using API.Core.Middleware;
using API.Infrastructure;
using API.Infrastructure.Data;
using API.Service;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace EcommerceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //db
            builder.Services.AddDbContext<ApplicationDBContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));
            });



            #region DEPENDENCY INJECTION

            builder.Services.AddInfrastructureDependencies()
                            .AddServiceDependencies()
                            .AddCoreDependencies()
                            .AddServiceRegistration();

            #endregion


            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.MaxDepth = 64; // Adjust the max depth as needed
            });


            #region Localization

            builder.Services.AddControllersWithViews();
            builder.Services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "";
            });

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                List<CultureInfo> supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar-EG"),
                    new CultureInfo("tr-TR")
                };
                options.DefaultRequestCulture = new RequestCulture("ar-EG");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            var currentCulture = CultureInfo.CurrentCulture.Name;
            var currentUICulture = CultureInfo.CurrentUICulture.Name;
            Console.WriteLine($"Current Culture: {currentCulture}, Current UI Culture: {currentUICulture}");



            #endregion



            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            #region Localization Middleware
            var opts = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            if (opts != null)
            {
                app.UseRequestLocalization(opts.Value);
            }
            else
            {
                // Handle the case where opts is null (e.g., log an error)
                throw new InvalidOperationException("RequestLocalizationOptions is not configured properly.");
            }
            app.UseMiddleware<ErrorHandlerMiddleware>();
            #endregion




            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
