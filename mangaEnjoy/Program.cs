using System.Text.Json.Serialization;
using mangaEnjoy.Interfaces;
using mangaEnjoy.Repositories;
using EntityFrameworkCore.UnitOfWork.Extensions;
using mangaEnjoy.Helpers;
using mangaEnjoy.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace mangaEnjoy;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IJwtProvider, JwtProvider>();
        
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        
        MappingConfig.RegisterMappings();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")),
                optionBuilder => optionBuilder.EnableStringComparisonTranslations());
        });
        
        builder.Services.AddScoped<DbContext>(provider => provider.GetService<AppDbContext>()!);
        builder.Services.AddUnitOfWork();
        builder.Services.AddUnitOfWork<AppDbContext>();
        
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey("supermegasigmaultrapupersecretkey"u8.ToArray())
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["token"];
                        return Task.CompletedTask;
                    }
                };
            });
        
        builder.Services.AddAuthorization();

        var app = builder.Build();
        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.UseCookiePolicy(new CookiePolicyOptions
        {
            MinimumSameSitePolicy = SameSiteMode.Strict,
            HttpOnly = HttpOnlyPolicy.Always,
            Secure = CookieSecurePolicy.Always
        });

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();

        app.Run();
    }
}