using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SIS.Api.Middleware;
using SIS.Application;
using SIS.Application.Interfaces;
using SIS.Infrastructure;
using SIS.Infrastructure.Chat;
using SIS.Infrastructure.Identity;
using SIS.Infrastructure.Persistence.SqlServer;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

var jwtSettings = builder.Configuration
    .GetSection("JwtSettings")
    .Get<JwtSettings>();

builder.Services.AddAuthentication(options =>
{

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
 {
    
     options.TokenValidationParameters = new TokenValidationParameters
     {
       
         ValidateIssuer = true,
         ValidIssuer = jwtSettings!.Issuer,

         ValidateAudience = true,
         ValidAudience = jwtSettings.Audience,

         
         ValidateLifetime = true,

         
         ValidateIssuerSigningKey = true,
         IssuerSigningKey = new SymmetricSecurityKey(
             Encoding.UTF8.GetBytes(jwtSettings.Secret))
     };
     options.Events = new JwtBearerEvents
     {
         OnMessageReceived = context =>
         {
             var accessToken = context.Request.Query["access_token"];
             var path = context.HttpContext.Request.Path;

             if (!string.IsNullOrEmpty(accessToken) &&
                 path.StartsWithSegments("/chatHub"))
             {
                 context.Token = accessToken;
             }

             return Task.CompletedTask;
         }
     };
 });

builder.Services.AddAuthorization();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Student Information System API",
        Version = "v1",
        Description = "SIS layihəsi üçün REST API"
    });

    
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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






builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:5500", "http://127.0.0.1:5500")
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials();
    });
});

builder.Services.AddSignalR();


builder.Services.AddScoped<IChatNotifier, SignalRChatNotifier>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SIS API v1");
        
    });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();



app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();

app.UseMiddleware<PasswordChangeEnforcementMiddleware>();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DataSeeder.SeedAsync(context);
}
app.MapHub<ChatHub>("/chatHub");

app.Run();
