using Microsoft.Extensions.FileProviders;
using Vikekh.Cv.WebRazor;
using Vikekh.Cv.WebRazor.Configuration;
using Vikekh.Cv.WebRazor.Repositories;
using Vikekh.JsonResume;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RuntimeGeneration>(builder.Configuration.GetSection(nameof(RuntimeGeneration)));

// Add services to the container.
builder.Services.AddRazorPages();

var applicationRoot = null as string;

if (applicationRoot == null)
{
    applicationRoot = @"C:\Users\Viktor\source\repos\cv";
}

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(applicationRoot));
builder.Services.AddSingleton<IYamlToJsonConverter>(new YamlToJsonConverter(Path.Combine(applicationRoot, "data", "yaml")));
builder.Services.AddSingleton<IJsonValidator>(new JsonValidator(Path.Combine(applicationRoot, "schema.json")));

var runtimeGeneration = new RuntimeGeneration();
builder.Configuration.GetSection(nameof(RuntimeGeneration)).Bind(runtimeGeneration);

if (runtimeGeneration.Enabled.HasValue && ((bool)runtimeGeneration.Enabled))
{
    builder.Services.AddSingleton<IJsonResumeRepository, GeneratedJsonResumeRepository>();
}
else
{
    builder.Services.AddSingleton<IJsonResumeRepository, JsonResumeRepository>();
}

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
