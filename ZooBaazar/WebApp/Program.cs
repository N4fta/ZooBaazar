using Microsoft.AspNetCore.Authentication.Cookies;
using Logic;
using Logic.ScheduleStuff;
using Data_Access;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Adding Authentication Services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options =>
    {
        options.LoginPath = new PathString("/Login");
        options.AccessDeniedPath = new PathString("/");
    }
);
builder.Services.AddScoped<ScheduleRepository>();
builder.Services.AddScoped<ScheduleManager>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IFeedingPlanRepository, FeedingPlanRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
builder.Services.AddScoped<IRelationshipRepository, RelationshipRepository>();
builder.Services.AddScoped<RelationshipManager>();
builder.Services.AddScoped<MedicalRecordManager>();
builder.Services.AddScoped<FeedingPlanManager>();
builder.Services.AddScoped<AnimalManager>();
builder.Services.AddScoped<TaskManager>();
builder.Services.AddScoped<ContractManager>();
builder.Services.AddScoped<EmployeeManager>();
builder.Services.AddScoped<LocationManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Tells app to use Authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
