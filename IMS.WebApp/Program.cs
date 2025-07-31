using IMS.WebApp.Components;
using IMS.Plugins.InMemory;
using IMS.UseCases.Inventories;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Products;
using IMS.UseCases.Products.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// IOC container (interface, konkret implementation)
// Man bruger interface, konkret implementation så IOC kender til mappingen mellem interface og implementation.
// AddInteractiveServerComponents opsætter Server Interactivity
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddSingleton<IInventoryRepository, InventoryRepository>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();

builder.Services.AddTransient<IAddInventoryUseCase, AddInventoryUseCase>();
builder.Services.AddTransient<IViewInventoriesByNameUseCase, ViewInventoriesByNameUseCase>();
builder.Services.AddTransient<IEditInventoryUseCase, EditInventoryUseCase>();
builder.Services.AddTransient<IViewInventoryByIdUseCase, ViewInventoryByIdUseCase>();
builder.Services.AddTransient<IDeleteInventoryUseCase, DeleteInventoryUseCase>();
builder.Services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();

builder.Services.AddTransient<IViewProductsByNameUseCase, ViewProductsByNameUseCase>();

var app = builder.Build();

// Konfigurere HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

// Requests bliver sendt til Razor Component App.razor, som håndterer routing og rendering af komponenter.
// AddInteractiveServerRenderMode gør at der opsættes SiglenRe forbindelse til server (Server interactivity)
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();