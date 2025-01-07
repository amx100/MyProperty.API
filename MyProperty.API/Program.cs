global using Contract;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Services.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyProperty.API.Core.Domain.Repositories.Common;
using MyProperty.API.Infrastructure.Persistence.Persistence.Repositories.Common;
using Persistence;
using Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);
ConfigureDatabase(builder.Services, builder.Configuration);
ConfigureJwtAuthentication(builder.Services, builder.Configuration);
ConfigureCors(builder.Services);
ConfigureSwagger(builder.Services);

var app = builder.Build();

ConfigureApp(app, builder.Environment);

app.Run();

void ConfigureServices(IServiceCollection services)
{
	services.AddControllers();
	services.AddEndpointsApiExplorer();
	services.AddSwaggerGen();

	services.AddScoped<IRepositoryManager, RepositoryManager>();
	services.AddScoped<IServiceManager, ServiceManager>();
	services.AddScoped<ITokenService, TokenService>();

	services.AddIdentity<Account, AccountRole>(opt =>
	{
		opt.Password.RequiredLength = 7;
		opt.Password.RequireDigit = true;
		opt.Password.RequireUppercase = true;
		opt.SignIn.RequireConfirmedEmail = true;
	})
		.AddEntityFrameworkStores<DataContext>()
		.AddDefaultTokenProviders();
}

void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
{
	var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));
	services.AddDbContextPool<DataContext>(options =>
	{
		options.UseMySql(configuration.GetConnectionString("MainDB"), serverVersion,
			mysqlOptions => { mysqlOptions.EnableRetryOnFailure(1, TimeSpan.FromSeconds(5), null); });
	});
}

void ConfigureJwtAuthentication(IServiceCollection services, IConfiguration configuration)
{
	var jwtSettings = configuration.GetSection("JwtSettings");

	services.AddAuthentication(opt =>
	{
		opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	}).AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = jwtSettings["validIssuer"],
			ValidAudience = jwtSettings["validAudience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["securityKey"]))
		};
	});
}

void ConfigureCors(IServiceCollection services)
{
	services.AddCors(options =>
	{
		options.AddPolicy("Storage", builder =>
		{
			builder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader();
		});
	});
}

void ConfigureSwagger(IServiceCollection services)
{
	services.AddSwaggerGen(c =>
	{
		c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
		{
			Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
			Name = "Authorization",
			In = ParameterLocation.Header,
			Type = SecuritySchemeType.ApiKey,
			Scheme = "Bearer"
		});

		c.AddSecurityRequirement(new OpenApiSecurityRequirement
		{
			{
				new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				},
				Array.Empty<string>()
			}
		});
	});
}

void ConfigureApp(WebApplication app, IHostEnvironment environment)
{
	app.UseCors("Storage");

	if (environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseHttpsRedirection();
	app.UseRouting();
	app.UseAuthentication();
	app.UseAuthorization();
	app.MapControllers();
}