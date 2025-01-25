using CleanSharpArchitecture.Application.Interfaces.Services;
using CleanSharpArchitecture.Application.Repositories.Interfaces;
using CleanSharpArchitecture.Application.Services.Interfaces;
using CleanSharpArchitecture.Infrastructure.Data;
using CleanSharpArchitecture.Infrastructure.Interfaces.Services;
using CleanSharpArchitecture.Infrastructure.Repositories;
using CleanSharpArchitecture.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
    )
);

builder.Logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Debug);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
        .EnableTokenAcquisitionToCallDownstreamApi()
            .AddMicrosoftGraph(builder.Configuration.GetSection("MicrosoftGraph"))
            .AddInMemoryTokenCaches();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<BlobSettings>(builder.Configuration.GetSection("BlobSettings"));
builder.Services.AddSingleton<IBlobClientFactory, BlobClientFactory>();
builder.Services.AddScoped<IBlobService, BlobService>();



builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<BlobService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

