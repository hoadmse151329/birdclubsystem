using BAL.AutoMapperProfile;
using BAL.Services.Implements;
using BAL.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAppMVC.Services.HostedServices;
using WebAppMVC.Services.Implements;
using WebAppMVC.Services.Interfaces;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace WebAppMVC
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            // Configure services
            ConfigureServices(builder.Services, builder.Configuration);

			var app = builder.Build();

            // Configure the HTTP request pipeline
            ConfigureMiddleware(app);

			app.Run();
		}

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Authentication setup
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = configuration.GetSection(Constants.Constants.GOOGLE_CLIENT_ID).Value;
                options.ClientSecret = configuration.GetSection(Constants.Constants.GOOGLE_CLIENT_SECRET).Value;
                options.CallbackPath = Constants.Constants.GOOGLE_REDIRECT_URI_PATH;
                options.AccessDeniedPath = Constants.Constants.LOGIN_URL;
                options.SaveTokens = true;
                options.Scope.Add("profile");
                options.Scope.Add("email");
            })/*.AddJwtBearer(options =>
            {
                *//*options.Authority = "your_authority_url"; // The URL of the authority that issues the JWT tokens
                options.Audience = "your_api_audience"; // The expected audience of the JWT tokens*//*
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.GetSection(Constants.Constants.JWT_VALID_ISSUER).Value,
                    ValidAudience = configuration.GetSection(Constants.Constants.JWT_VALID_AUDIENCE).Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection(Constants.Constants.JWT_SECRET_KEY).Value))
                };
            })*/;

            // Add HttpClient
            services.AddHttpClient();

            // Add controllers with views
            services.AddControllersWithViews();

            // Add AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            // Add session configuration
            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.IdleTimeout = TimeSpan.FromMinutes(25); // Adjust the timeout as needed
            });
            // Add logging services for debugs
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

            // Add scoped services
            services.AddScoped<ISystemLoginService, SystemLoginService>();
            services.AddScoped<IVnPayService, VnPayService>();

            // Add Hosted services
            services.AddHostedService<MembershipExpiryService>();
            services.AddHostedService<MeetingStatusUpdateService>();
            services.AddHostedService<ContestStatusUpdateService>();
            services.AddHostedService<FieldTripStatusUpdateService>();
        }
        private static void ConfigureMiddleware(WebApplication app)
        {
            // Configure exception handling
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts(); // HSTS middleware
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable HTTPS redirection and static files
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Enable routing
            app.UseRouting();

            // Enable session management
            app.UseSession();

            // Enable CORS
            app.UseCors();

            // Enable authentication and authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Configure endpoint routing
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}