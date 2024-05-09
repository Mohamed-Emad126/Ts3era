using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Org.BouncyCastle.Pkix;
using System.Text;
using Ts3era.HandleResponseApi;
using Ts3era.Heler;
using Ts3era.MappingProfile;
using Ts3era.MiddleWares;
using Ts3era.Models;
using Ts3era.Models.Data;
using Ts3era.Repositories.Category_Repositories;
using Ts3era.Repositories.Complaint_Repositories;
using Ts3era.Repositories.FavProductServices;
using Ts3era.Repositories.FeedBack_Repository;
using Ts3era.Repositories.Governoreate_Repositories;
using Ts3era.Repositories.Port_Repositories;
using Ts3era.Repositories.Port_TypeRepositories;
using Ts3era.Repositories.Product_Repositories;
using Ts3era.Repositories.SubCategory_Repositories;
using Ts3era.Services.AuthServices;
using Ts3era.Services.EmailServices;
using Ts3era.Services.Role_Services;
using Ts3era.Services.User_Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region//Add Swagger Authancated

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v2",
        Title = "ASP.NET 6 Web API",
        Description = "Ts3era"
    });
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {

        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input "

    });

    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
         {
        {
        new OpenApiSecurityScheme
        {
            Reference=new OpenApiReference
             {
            Type=ReferenceType.SecurityScheme,
            Id="Bearer"
              }
        },
        new string[]{}
        }

    });

});
#endregion
//add jwt 

builder.Services.AddAuthentication(options =>
{

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}
).AddJwtBearer(options =>
{

    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issure"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audiance"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"]))




    };

});

#region  AddModelErrors 
builder.Services.Configure<ApiBehaviorOptions>(option =>
{

    option.InvalidModelStateResponseFactory = context =>
    {
        var Errors = context.ModelState
        .Where(m => m.Value.Errors.Count > 0)
        .SelectMany(m => m.Value.Errors)
        .Select(e => e.ErrorMessage).ToList();

        var response = new ValidationError
        {
            Errors = Errors
        };


        return new BadRequestObjectResult(response);

    };

});
#endregion

//connectionstring 
var connection = builder.Configuration.GetConnectionString("CS");
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(connection);
});

//injection 
builder.Services.AddTransient<IUserServices, UserServices>();
builder.Services.AddTransient<IRoleServices, RoleServices>();

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IComplaintRepository, ComplaintRepository>();
builder.Services.AddTransient<IGovernorate_Repository, Governorate_Repository>();
builder.Services.AddTransient<IPortRepository, PortRepository>();
builder.Services.AddTransient<IPortType_Repositort, PortType_Repository>();


builder.Services.AddTransient<IFavoriteServies, FavoriteServies>(); 
builder.Services.AddTransient<IFeedBackRepository, FeedBackRepository>();
//Automapper 
builder.Services.AddAutoMapper(typeof(Program));//full project 
 



//jwt 

builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
//identity
builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
//Services login and Register 
builder.Services.AddScoped<IAuthServices,AuthServices>();
//add Email  confiquration 
builder.Services.Configure<Emails>(builder.Configuration.GetSection("mailsettings"));
builder.Services.AddTransient<IEmailServices,EmailServices>();

builder.Services.Configure<IdentityOptions>(c => c.SignIn.RequireConfirmedEmail = true);
builder.Services.Configure<DataProtectionTokenProviderOptions>(c => c.TokenLifespan = TimeSpan.FromHours(30));

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);


builder.Services.AddCors(corsoption =>
{
    corsoption.AddPolicy("Mypolicy", crospolicy => crospolicy.WithOrigins("http://localhost:3000/")
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .AllowAnyHeader()
    );
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<ExceptionMiddleWare>();
}

app.UseStaticFiles();
app.UseCors("Mypolicy");
app.UseRouting();

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
