using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Model;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddKeyPerFile("/run/secrets", optional: true);



// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddDbContext<ChoreAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ChoreApp")));

builder.Services.AddScoped<IChoreAppContext, ChoreAppContext>();
builder.Services.AddScoped<IChoreRepository, ChoreRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFamilyRepository, FamilyRepository>();
builder.Services.AddScoped<IWorkEventRepository, WorkEventRepository>();
builder.Services.AddScoped<IWorkTimeSlotRepository, WorkTimeSlotRepository>();


builder.Services.AddSingleton<WeatherForecastService>();

// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChoreApp.Web", Version = "v1" });
//     c.UseInlineDefinitionsForEnums();
// });

// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

if (!app.Environment.IsEnvironment("Integration"))
{
    await app.Seed();
}

app.Run();
