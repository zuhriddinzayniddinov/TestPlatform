﻿using System;
using System.Diagnostics;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TestPlatform.Infrastructure.Authentication;
using TestPlatform.Infrastructure.Contexts;
using TestPlatform.Infrastructure.Repositories.Tokens;
using TestPlatform.Infrastructure.Repositories.Users;
using TestPlatform.Services.Mappers;
using TestPlatform.Services.UserServices;

namespace TestPlatform.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<Stopwatch>();
        services.AddSwaggerService();

        return services;
    }
    public static IServiceCollection AddDomain(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
    public static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<Services.Authentication.IAuthenticationService, Services.Authentication.AuthenticationService>();
        services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserService,UserService>();

        return services;
    }
    public static IServiceCollection AddDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresSQL");

        services.AddDbContextPool<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString, sqlServerOptions =>
            {
                sqlServerOptions.EnableRetryOnFailure();
            });
        });

        return services;
    }
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtOption>(configuration
            .GetSection("JwtSettings"));

        services.AddScoped<IRefreshTokenRepository,RefreshTokenRepository>();
        services.AddScoped<ITokenRepository,TokenRepository>();
        services.AddScoped<IUserRepository,UserRepository>();
        services.AddTransient<IPasswordHasher,PasswordHasher>();
        services.AddTransient<IJwtTokenHandler,JwtTokenHandler>();

        return services;
    }
    public static IServiceCollection AddTelegramBot(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }

    private static void AddSwaggerService(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestPlatform.Api", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
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
                    new string[] { }
                }
            });
        });
    }
}