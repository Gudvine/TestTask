using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using TestTask.Data.Context;
using TestTask.Services;

var builder = WebApplication.CreateBuilder(args);

string? dbConnString = builder.Configuration.GetConnectionString("TestDbConnection");

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddMudServices();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseNpgsql(dbConnString));

builder.Services.AddBlazorDownloadFile();
builder.Services.AddScoped<FileService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
