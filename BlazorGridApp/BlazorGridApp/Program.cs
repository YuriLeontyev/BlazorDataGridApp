using BlazorGridApp.Client.Pages;
using BlazorGridApp.Components;
using BlazorGridApp.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddFluentUIComponents();
builder.Services.AddControllers()
    .AddOData(opt =>
        opt.AddRouteComponents("api", BuildEdmModel())
           .Filter()
           .OrderBy()
           .Count()
           .SetMaxTop(int.MaxValue));

static IEdmModel BuildEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<Product>("Products");
    return builder.GetEdmModel();
}

builder.Services.AddHttpClient("ServerSideHttp", client =>
{
    // Reads the first URL from ASPNETCORE_URLS or falls back to a default
    var urls = builder.Configuration["ASPNETCORE_URLS"]
               ?? builder.Configuration["urls"]
               ?? "https://localhost:7075";
    client.BaseAddress = new Uri(urls.Split(';').First());
});

builder.Services.AddScoped(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("ServerSideHttp");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorGridApp.Client._Imports).Assembly);

await app.RunAsync();
