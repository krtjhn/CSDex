// Import namespace: Microsoft.AspNetCore.Authentication.JwtBearer
using Microsoft.AspNetCore.Authentication.JwtBearer;
// Import namespace: Microsoft.EntityFrameworkCore
using Microsoft.EntityFrameworkCore;
// Import namespace: Microsoft.IdentityModel.Tokens
using Microsoft.IdentityModel.Tokens;
// Import namespace: Oopdex.Api.Data
using Oopdex.Api.Data;
// Import namespace: Oopdex.Api.Services
using Oopdex.Api.Services;
// Import namespace: System.Text
using System.Text;
// Empty line

// Variable declaration and assignment: builder = WebApplication.CreateBuilder(args)
var builder = WebApplication.CreateBuilder(args);
// Empty line

// Existing comment: Add services to the container.
// Add services to the container.
// Execute line: builder.Services.AddControllers()
builder.Services.AddControllers()
    // Execute line: .AddJsonOptions(options =>
    .AddJsonOptions(options =>
    // Start of block scope
    {
        // Execute line: options.JsonSerializerOptions.ReferenceHandler = System.T...
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    // Execute line: });
    });
// Execute line: builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpointsApiExplorer();
// Execute line: builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen();
// Empty line

// Existing comment: Configure EF Core with Pomelo MySQL
// Configure EF Core with Pomelo MySQL
// Variable declaration and assignment: connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Execute line: builder.Services.AddDbContext<OopdexDbContext>(options =>
builder.Services.AddDbContext<OopdexDbContext>(options =>
    // Execute line: options.UseMySql(connectionString, ServerVersion.AutoDete...
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
// Empty line

// Existing comment: Configure DI for Services
// Configure DI for Services
// Execute line: builder.Services.AddScoped<IJwtTokenService, JwtTokenServ...
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
// Execute line: builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserService, UserService>();
// Execute line: builder.Services.AddScoped<IPokemonService, PokemonServic...
builder.Services.AddScoped<IPokemonService, PokemonService>();
// Execute line: builder.Services.AddScoped<ICaughtPokemonService, CaughtP...
builder.Services.AddScoped<ICaughtPokemonService, CaughtPokemonService>();
// Empty line

// Existing comment: Configure JWT Authentication
// Configure JWT Authentication
// Variable declaration and assignment: jwtSecret = builder.Configuration["Jwt:Secret"]
var jwtSecret = builder.Configuration["Jwt:Secret"];
// Variable declaration and assignment: key = Encoding.UTF8.GetBytes(jwtSecret)
var key = Encoding.UTF8.GetBytes(jwtSecret);
// Empty line

// Execute line: builder.Services.AddAuthentication(JwtBearerDefaults.Auth...
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    // Execute line: .AddJwtBearer(options =>
    .AddJwtBearer(options =>
    // Start of block scope
    {
        // Execute line: options.MapInboundClaims = false;
        options.MapInboundClaims = false;
        // Execute line: options.RequireHttpsMetadata = false;
        options.RequireHttpsMetadata = false;
        // Execute line: options.SaveToken = true;
        options.SaveToken = true;
        // Execute line: options.TokenValidationParameters = new TokenValidationPa...
        options.TokenValidationParameters = new TokenValidationParameters
        // Start of block scope
        {
            // Execute line: ValidateIssuerSigningKey = true,
            ValidateIssuerSigningKey = true,
            // Execute line: IssuerSigningKey = new SymmetricSecurityKey(key),
            IssuerSigningKey = new SymmetricSecurityKey(key),
            // Execute line: ValidateIssuer = false,
            ValidateIssuer = false,
            // Execute line: ValidateAudience = false,
            ValidateAudience = false,
            // Execute line: ValidateLifetime = true,
            ValidateLifetime = true,
            // Execute line: RoleClaimType = "role",
            RoleClaimType = "role",
            // Execute line: NameClaimType = "sub"
            NameClaimType = "sub"
        // Execute line: };
        };
    // Execute line: });
    });
// Empty line

// Existing comment: Configure CORS
// Configure CORS
// Execute line: builder.Services.AddCors(options =>
builder.Services.AddCors(options =>
// Start of block scope
{
    // Execute line: options.AddPolicy("AllowAll", builder =>
    options.AddPolicy("AllowAll", builder =>
    // Start of block scope
    {
        // Execute line: builder.AllowAnyOrigin()
        builder.AllowAnyOrigin()
               // Execute line: .AllowAnyMethod()
               .AllowAnyMethod()
               // Execute line: .AllowAnyHeader();
               .AllowAnyHeader();
    // Execute line: });
    });
// Execute line: });
});
// Empty line

// Variable declaration and assignment: app = builder.Build()
var app = builder.Build();
// Empty line

// Existing comment: Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
// Control Flow: check condition 'if (app.Environment.IsDevelopment())'
if (app.Environment.IsDevelopment())
// Start of block scope
{
    // Execute line: app.UseSwagger();
    app.UseSwagger();
    // Execute line: app.UseSwaggerUI();
    app.UseSwaggerUI();
// End of block scope
}
// Empty line

// Execute line: app.UseCors("AllowAll");
app.UseCors("AllowAll");
// Empty line

// Existing comment: Serve static files for the uploads folder
// Serve static files for the uploads folder
// Variable declaration and assignment: uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads")
var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
// Control Flow: check condition 'if (!Directory.Exists(uploadsPath))'
if (!Directory.Exists(uploadsPath))
// Start of block scope
{
    // Execute line: Directory.CreateDirectory(uploadsPath);
    Directory.CreateDirectory(uploadsPath);
// End of block scope
}
// Execute line: app.UseStaticFiles(new StaticFileOptions
app.UseStaticFiles(new StaticFileOptions
// Start of block scope
{
    // Execute line: FileProvider = new Microsoft.Extensions.FileProviders.Phy...
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(uploadsPath),
    // Execute line: RequestPath = "/uploads"
    RequestPath = "/uploads"
// Execute line: });
});
// Empty line

// Existing comment: Use default static files for assets if needed
// Use default static files for assets if needed
// Execute line: app.UseStaticFiles();
app.UseStaticFiles();
// Empty line

// Execute line: app.UseAuthentication();
app.UseAuthentication();
// Execute line: app.UseAuthorization();
app.UseAuthorization();
// Empty line

// Execute line: app.MapControllers();
app.MapControllers();
// Empty line

// Execute line: app.Run();
app.Run();
