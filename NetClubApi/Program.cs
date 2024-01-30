
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

using NetClubApi.UserModule;
using NetClubApi.Model;
using NetClubApi.Comman;

var builder = WebApplication.CreateBuilder(args);

#region dependency injection

    builder.Services.AddTransient<IUserDataAccess, UserDataAccess>();
    builder.Services.AddTransient<IHelper, Helper>();
#endregion


builder.Services.AddCors(options => {
    options.AddPolicy("customPolicy", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddEndpointsApiExplorer();

#region swagger conf
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
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

#endregion

#region Database Connectivity

builder.Services.AddDbContext<NetClubDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers();
builder.Services.AddSignalR();

#region authentication config


// token validation middleware 
// excuted for every  http request 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidAudience = "user",
    ValidIssuer = "user",
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("apwmdlliendaddnetknz=3mlkd652341")) // Use the same key as in generatetoken

});



#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.

#region swagger middleware
app.UseSwagger();

app.UseSwaggerUI(
    options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = String.Empty;
        options.DocumentTitle = "My Swagger";
        options.ConfigObject.AdditionalItems["presets"] = new[] { "Bearer" };
    });

#endregion


app.UseCors("customPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();
