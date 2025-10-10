using Context.DependencyInjectionContext;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json.Serialization;
using TNTAuthenticatorDB.DI;
using TNTAuthService.Configurations;
using TNTAuthService.Configurations.DependencyInjection;
using TNTParking_Backend.GlobalExceptionHandler;
using TNTParking_Backend.Interfaces;
using TNTParking_Backend.Interfaces.Settings;
using TNTParking_Backend.Models;
using TNTParking_Backend.Models.Settings;
using TNTParking_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

var _appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
var _tntParkingAppSettings = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettingsTNTParking>(_tntParkingAppSettings);
var _newAppSettings = _tntParkingAppSettings.Get<AppSettingsTNTParking>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
#region Dependency 

var appSettings = builder.Configuration.GetSection(nameof(AppSettings));
builder.Services.AddOptions();
builder.Services.Configure<ConnectionString>(appSettings.GetSection(nameof(ConnectionString)));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<AppSettingsTNTParking>(builder.Configuration.GetSection("AppSettingsTNTParking"));
builder.Services.AddSingleton<IDatabaseSettings>(x => x.GetRequiredService<IOptions<ConnectionString>>().Value);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IAreasService, AreasService>();
builder.Services.AddScoped<IParkingDaysOffService, ParkingDaysOffService>();
builder.Services.AddScoped<IAreaTypeService, AreaTypeService>();
builder.Services.AddScoped<IParkingRatesService, ParkingRatesService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

builder.Services.AddHttpClient();

DIDBContextTNTAuthenticator.AddDependency(builder.Services, builder.Configuration);
DependencyInjectionContext.AddDependency(builder.Services, builder.Configuration);
DI_TNTAuthService.AddDependency(builder.Services, builder.Configuration);

#endregion

builder.Services.AddMemoryCache();
builder.Services.AddMvc();

const string MainCorsPolicy = "MainCorsPolicy";
const string ExternalCorsPolicy = "ExternalCorsPolicy";
string[] corsOriginUrls = !string.IsNullOrWhiteSpace(builder.Configuration["CORS_ALLOWED_ORIGINS"]) ? builder.Configuration["CORS_ALLOWED_ORIGINS"].Split(';') : _appSettings.JWT.AllowedOrigins;

builder.Services.AddCors(options =>
{
    options.AddPolicy(MainCorsPolicy, builder => builder
        .WithOrigins(corsOriginUrls)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

    options.AddPolicy(ExternalCorsPolicy, builder => builder
    .AllowAnyOrigin()
       .AllowAnyMethod()
       .AllowAnyHeader());
});

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.IgnoreReadOnlyFields = true;
    }
);

builder.Services.Configure<AppSettings>(options => builder.Configuration.GetSection("AppSettings").Bind(options));

var key = Encoding.ASCII.GetBytes(_appSettings.JWT.Secret);

var app = builder.Build();

app.UseMiddleware<TNTAuthService.Configurations.Middleware.TNTAuthService>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.UseExceptionHandler();

app.MapControllers();

app.Run();