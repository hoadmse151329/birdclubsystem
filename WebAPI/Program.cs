
using BAL.AutoMapperProfile;
using BAL.Services.Implements;
using BAL.Services.Interfaces;
using DAL.Infrastructure;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            builder.Services.AddDbContext<BirdClubContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddHttpContextAccessor();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Documentation",
                    Version = "v1",
                });
                // Configuring JWT Validation for Swagger
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Bearer token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securityScheme, new[] { "Bearer" } }
                };
                c.AddSecurityRequirement(securityRequirement);
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                        builder.Configuration.GetSection("AppSettings")["SecretKey"]
                        //config["AppSettings:SecretKey"]
                        )),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddRazorPages().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            });

            builder.Services.AddCors(options => 
            { 
                options.AddDefaultPolicy(policy => 
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IMemberService,MemberService>();
            builder.Services.AddScoped<ILocationService,LocationService>();
            builder.Services.AddTransient<IEmailService,EmailService>();
            builder.Services.AddScoped<IJWTService,JWTService>();
            builder.Services.AddScoped<IMeetingService,MeetingService>();
            builder.Services.AddScoped<IMeetingMediaService,MeetingMediaService>();
            builder.Services.AddScoped<IMeetingParticipantService,MeetingParticipantService>();
            builder.Services.AddScoped<IFieldTripService, FieldTripService>();
            builder.Services.AddScoped<IFieldTripParticipantService, FieldTripParticipantService>();
            builder.Services.AddScoped<IFieldTripDayByDayService,FieldTripDayByDayService>();
            builder.Services.AddScoped<IFieldTripInclusionService, FieldTripInclusionService>();
            builder.Services.AddScoped<IFieldTripAdditionalDetailService, FieldTripAdditionalDetailService>();
            builder.Services.AddScoped<IContestService, ContestService>();
            builder.Services.AddScoped<IContestParticipantService, ContestParticipantService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<IMediaService, MediaService>();
            builder.Services.AddScoped<IBirdService, BirdService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectAPI v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();


            app.MapControllers();

            app.Run();
        }
    }
}