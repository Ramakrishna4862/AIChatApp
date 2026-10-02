using AIChatApp.Data;
using Microsoft.EntityFrameworkCore;
using AIChatApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddScoped<IAIService, FakeAIService>();
//builder.Services.AddScoped<IAIService, OpenAIService>();
//builder.Services.AddScoped<IAIService, GeminiAIService>();
builder.Services.AddHttpClient<GeminiAIService>();
builder.Services.AddHttpClient<GroqAIService>();
builder.Services.AddHttpClient<OpenRouterAIService>();

builder.Services.AddScoped<IAIService, FallbackAIService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
