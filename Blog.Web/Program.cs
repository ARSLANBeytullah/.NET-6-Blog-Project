using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Blog.Data.Context;
using Blog.Data.Extensions;
using Blog.Service.Extensions;
using Blog.Entity.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.LoadDataLayerExtension(builder.Configuration);
builder.Services.LoadServiceLayerExtension();
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); //AddRazorRuntimeCompilation methodu sayesin de projeyi tekrar aya�a kald�rmadan yapt���m�z de�i�ikleri g�rebiliyoruz.


builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
})
    .AddRoleManager<RoleManager<AppRole>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = new PathString("/Admin/Auth/Login");
    config.LogoutPath = new PathString("/Admin/Auth/Logout");
    config.Cookie = new CookieBuilder
    {
        Name = "Blog",
        HttpOnly = true, //sadece http isteklerini al�caz
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.SameAsRequest //Hem http hem de htpps taraf�nda istek alabiliriz. CookieSecurePolicy.SameAsRequest.Always ise sadece htpps de �al���r.
    };
    config.SlidingExpiration = true;
    config.ExpireTimeSpan = TimeSpan.FromDays(7); //Cookie sistemde ne kadar kalacak. Siteye giri� yap�ld���nda oturum ne kadar a��k kals�n demek bir nevi.
    config.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied"); //yetkisiz bir giri� oldu�unda bu sayfam�z �al��acak.

});

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
app.UseSession();

app.UseRouting();
app.UseAuthentication(); //kimlik do�rulama
app.UseAuthorization(); //yetkilendirme

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
