using EmployeePortal.Api.Bootstraper;
using EmployeePortal.Api.Configuration;
using EmployeePortal.Api.DataAccess.Database;
using EmployeePortal.Api.DataAccess.Database.EntityFramework;
using EmployeePortal.Api.DataAccess.Interfaces.Database;
using EmployeePortal.Api.Domain.Employees;
using EmployeePortal.Api.Domain.TimeLogs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var postgresDbOptions = new PostgresDbOptions();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(
        options =>
        {
            builder.Configuration.Bind("AzureAd", options);
            options.Events = new JwtBearerEvents();
            //options.TokenValidationParameters.NameClaimType = "name";
        },
        options =>
        {
            builder.Configuration.Bind("AzureAd", options);
        });

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("AuthPolicy", policyBuilder =>
        policyBuilder.Requirements.Add(new ScopeAuthorizationRequirement(["Profile.ReadWrite"])));
});

builder.Configuration.Bind("PostgresDbOptions", postgresDbOptions);

builder.Services.AddDbContext<DatabaseContext>(options => { options.UseNpgsql(postgresDbOptions.BuildConnectionString()); });
builder.Services.AddDomainServices();
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

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
app.UseCors(config => config
    .WithOrigins("http://localhost:4200")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials());

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    context.Database.Migrate();
    SeedEmployees(context);
    SeedProjects(context);
}

app.Run();

static void SeedEmployees(DatabaseContext context)
{
    if (!context.Set<Employee>().Any())
    {
        var employee = new Employee()
        {
            Id = Guid.NewGuid(),
            FirstName = "FirstName1",
            LastName = "LastName1",
            Email = "user1@company.co"
        };
        context.Set<Employee>().Add(employee);

        employee = new Employee()
        {
            Id = Guid.NewGuid(),
            FirstName = "FirstName2",
            LastName = "LastName2",
            Email = "user2@company.co"
        };
        context.Set<Employee>().Add(employee);

        employee = new Employee()
        {
            Id = Guid.NewGuid(),
            FirstName = "FirstName3",
            LastName = "LastName3",
            Email = "user3@company.co"
        };
        context.Set<Employee>().Add(employee);

        context.SaveChanges();
    }
}
static void SeedProjects(DatabaseContext context)
{
    if (!context.Set<Project>().Any())
    {
        var project = new Project()
        {
            Id = Guid.NewGuid(),
            Name = "PTO-Sickleave"
        };
        context.Set<Project>().Add(project);

        project = new Project()
        {
            Id = Guid.NewGuid(),
            Name = "PTO-Vacation"
        };
        context.Set<Project>().Add(project);

        project = new Project()
        {
            Id = Guid.NewGuid(),
            Name = "Wonder"
        };
        context.Set<Project>().Add(project);

        project = new Project()
        {
            Id = Guid.NewGuid(),
            Name = "HonestDoor"
        };
        context.Set<Project>().Add(project);

        context.SaveChanges();
    }
}