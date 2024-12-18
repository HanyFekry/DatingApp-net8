using Asp.Versioning;
using DatingApi.Data;
using DatingApi.Helpers;
using DatingApi.Localization;
using DatingApi.MappingProfiles;
using DatingApi.Repositories;
using DatingApi.Resolvers;
using DatingApi.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Text.Json.Serialization;

namespace DatingApi.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            {
                services.AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                    options.ApiVersionReader = ApiVersionReader.Combine(
                        new UrlSegmentApiVersionReader(),
                        new HeaderApiVersionReader("Api-Version")
                    );
                })
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });
                services.AddCors();

                services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                        options.JsonSerializerOptions.WriteIndented = true;
                    });

                services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

                services.AddStackExchangeRedisCache(opt =>
                {
                    opt.Configuration = configuration.GetValue<string>("Redis");
                });
                //localization
                services.AddLocalization();// (opt => { opt.ResourcesPath = "Resources"; });

                services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
                services.Configure<RequestLocalizationOptions>(
                    options =>
                    {
                        var supportedCultures = new List<CultureInfo>
                        {
                            new CultureInfo("en"),
                            new CultureInfo("ar"),
                            new CultureInfo("de")
                        };

                        options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
                        options.SupportedCultures = supportedCultures;
                        //for views only
                        //options.SupportedUICultures = supportedCultures;
                    });

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
                });
                //services.AddAutoMapper(cfg =>
                //{
                //    //Assembly.GetExecutingAssembly();
                //    using var scope = services.BuildServiceProvider().CreateScope();
                //    var langService = scope.ServiceProvider.GetRequiredService<ILanguageService>();
                //    cfg.AddProfile(new MappingProfile(langService));

                //});

                services.AddScoped<ITokenService, TokenService>();
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IPhotoService, PhotoService>();

                services.AddHttpContextAccessor(); // Add this line
                services.AddScoped<ILanguageService, LanguageService>();
                services.AddAutoMapper(typeof(MappingProfile));
                services.AddTransient<LanguageResolver>(); // Register the resolver

                //services.AddEndpointsApiExplorer();

                return services;
            }
        }
    }
}
