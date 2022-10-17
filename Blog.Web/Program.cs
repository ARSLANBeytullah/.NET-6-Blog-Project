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
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); //AddRazorRuntimeCompilation methodu sayesin de projeyi tekrar ayaða kaldýrmadan yaptýðýmýz deðiþikleri görebiliyoruz.


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
        HttpOnly = true, //sadece http isteklerini alýcaz
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.SameAsRequest //Hem http hem de htpps tarafýnda istek alabiliriz. CookieSecurePolicy.SameAsRequest.Always ise sadece htpps de çalýþýr.
    };
    config.SlidingExpiration = true;
    config.ExpireTimeSpan = TimeSpan.FromDays(7); //Cookie sistemde ne kadar kalacak. Siteye giriþ yapýldýðýnda oturum ne kadar açýk kalsýn demek bir nevi.
    config.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied"); //yetkisiz bir giriþ olduðunda bu sayfamýz çalýþacak.

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
app.UseAuthentication(); //kimlik doðrulama
app.UseAuthorization(); //yetkilendirme

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
