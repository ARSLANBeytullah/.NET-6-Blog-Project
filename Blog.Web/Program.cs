using Microsoft.EntityFrameworkCore;
using Blog.Data.Context;
using Blog.Data.Extensions;
using Blog.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.LoadDataLayerExtension(builder.Configuration);
builder.Services.LoadServiceLayerExtension();
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); //AddRazorRuntimeCompilation methodu sayesin de projeyi tekrar ayaða kaldýrmadan yaptýðýmýz deðiþikleri görebiliyoruz.


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
    endpoints.MapAreaControllerRoute(   //Admin için tanýmladýðým rota.
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{Controller=Home}/{Action=Index}/{id?}"
        );
    endpoints.MapDefaultControllerRoute();  //Admin rotasýna girmesse eðer normal default rota tercih edilecek.
});

app.Run();
