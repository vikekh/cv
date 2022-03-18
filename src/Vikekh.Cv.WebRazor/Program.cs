using Microsoft.Extensions.FileProviders;
using Vikekh.Cv.WebRazor;
using Vikekh.Cv.WebRazor.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<ResumeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var physicalFileProvider = new PhysicalFileProvider(@"C:\Users\Viktor\source\repos\cv");
var yamlToJsonConverter = new YamlToJsonConverter(physicalFileProvider);
yamlToJsonConverter.Convert();
var jsonResumeGenerator = new JsonResumeGenerator();
await jsonResumeGenerator.RunAsync();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
