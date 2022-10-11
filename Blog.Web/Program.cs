using Microsoft.EntityFrameworkCore;
using Blog.Data.Context;
using Blog.Data.Extensions;
using Blog.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.LoadDataLayerExtension(builder.Configuration);
builder.Services.LoadServiceLayerExtension();
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); //AddRazorRuntimeCompilation methodu sayesin de projeyi tekrar aya�a kald�rmadan yapt���m�z de�i�ikleri g�rebiliyoruz.


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

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(   //Admin i�in tan�mlad���m rota.
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{Controller=Home}/{Action=Index}/{id?}"
        );
    endpoints.MapDefaultControllerRoute();  //Admin rotas�na girmesse e�er normal default rota tercih edilecek.
});

app.Run();
