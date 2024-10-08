
using API.Core;
using API.Core.Filters;
using API.Core.Middleware;
using API.Core.SharedResource;
using API.Infrastructure;
using API.Infrastructure.Data;
using API.Infrastructure.Seeder;
using API.Service;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Text.Json.Serialization;

namespace EcommerceAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
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



            //to use it:
            //[Authorize(Roles ="User")]
            //[ServiceFilter(typeof(AuthFilter))]
            builder.Services.AddTransient<AuthFilter>();



            #region DEPENDENCY INJECTION

            builder.Services.AddServiceRegistration(builder.Configuration)
                            .AddInfrastructureDependencies()
                            .AddServiceDependencies()
                            .AddCoreDependencies()
                            .AddSingleton<IStringLocalizer, StringLocalizer<SharedResources>>();

            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });


            #endregion


            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            builder.Services.AddTransient<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
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


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });


            #endregion



            var app = builder.Build();

            app.UseCors("AllowAll");


            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                await UserSeeder.SeedAsync(userManager);
                await RoleSeeder.SeedAsync(roleManager);
            }



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

            app.UseStaticFiles();


            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
