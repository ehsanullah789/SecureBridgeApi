
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using secureBridge_Services.Data;
using secureBridge_Services.Repositories.CommunityRepository;
using secureBridge_Services.Repositories.OpportunityRepository;
using secureBridge_Services.Repositories.OrganizationRepository;
using secureBridge_Services.Repositories.UserRepository;
using secureBridge_Services.Services.BrainTreeService;
using secureBridge_Services.Services.EncryptionServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(
    opt=>opt.UseSqlServer(connectionString: "server=.;Database=SecureBridgeDb;Trusted_Connection=true;")
    );

//Cors
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("myAllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:8080")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed((host) => true)
            .AllowCredentials();
        });
});
//Repositories
builder.Services.AddTransient<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IOpportunityRepository, OpportunityRepository>();
builder.Services.AddTransient<ICommunityRepository, CommunityRepository>();
//Services
builder.Services.AddTransient<IEncryptionService, EncryptionService>();
builder.Services.AddTransient<IBraintreeService, BraintreeService>();

builder.Services.AddControllers();

//Jwt Authentication
var token = builder.Configuration["JWT:Key"];
var issuer = builder.Configuration["JWT:Issuer"];
var audience = builder.Configuration["JWT:Audience"];
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.Events = new JwtBearerEvents { };
    opt.RequireHttpsMetadata = false;
    opt.SaveToken = false;
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(ASCIIEncoding.UTF8.GetBytes(token))
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("myAllowSpecificOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
