using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

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
			.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
			{
				options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
				options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
			});

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddSession(options =>
			{
				options.Cookie.IsEssential = true;
				options.Cookie.SameSite = SameSiteMode.None;
				options.Cookie.HttpOnly = true;
				options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
				options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust the timeout as needed
			});

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

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}