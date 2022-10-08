using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Movies.API.DbContexts;
using Movies.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MoviesInfoContext>(dbContextOptions =>
dbContextOptions.UseSqlServer(builder.Configuration["ConnectionStrings:MoviesInfoDBConnectionString"]));

builder.Services.AddScoped<IMoviesInfoRepository, MoviesInfoRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddControllers(option =>
{
    option.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "TestAPI",
            Description = "my first api",
            TermsOfService=new Uri("https://www.google.com"),
            Contact=new OpenApiContact
            {
                Name="mahmoud",
                Email="test@gmail.com"
            },
            License=new OpenApiLicense
            {
                Name = "licience",
                Url=new Uri("https://www.google.com"),
            }
        });
        options.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
        {
           Name="Authorization",
           Type=SecuritySchemeType.ApiKey,
           Scheme="Bearer",
           BearerFormat="JWT",
           In=ParameterLocation.Header,
           Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Name="Bearer",
                In=ParameterLocation.Header
            },
            new List<string>()
            }
        });

    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(c=>c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

//app.UseEndpoints(endpoints => {
//    endpoints.MapControllers();
//});

app.MapControllers();

app.Run();
