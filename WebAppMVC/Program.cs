using BAL.AutoMapperProfile;
using BAL.Services.Implements;
using BAL.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using WebAppMVC.Services;

namespace WebAppMVC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
			}).AddCookie()
			.AddGoogle(options =>
			{
				IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");
				options.ClientId = googleAuthNSection["ClientId"];
				options.ClientSecret = googleAuthNSection["ClientSecret"];
				options.CallbackPath = "/Auth/GoogleResponse";
				options.SaveTokens = true;
			});
			builder.Services.AddHttpClient();
			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddAutoMapper(typeof(MappingProfile));
			builder.Services.AddSession(options =>
			{
				options.Cookie.IsEssential = true;
				options.Cookie.SameSite = SameSiteMode.None;
				options.Cookie.HttpOnly = true;
				options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
				options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust the timeout as needed
			});

			builder.Services.AddScoped<IVnPayService, VnPayService>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseSession();
			app.UseCors();

			app.UseAuthorization();
			app.UseAuthentication();
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
			app.Run();
		}
	}
}