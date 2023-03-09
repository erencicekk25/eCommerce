using Divisima.BL.Repositories;
using Divisima.DAL.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSession(opt => { opt.IdleTimeout = TimeSpan.FromMinutes(30); });
builder.Services.AddDbContext<SQLContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("CS1")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(SQLRepository<>));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    opt.LoginPath = "/admin/login";
    opt.LogoutPath = "/admin/logout";
});
var app = builder.Build();
//middleware
if (!app.Environment.IsDevelopment()) app.UseStatusCodePagesWithRedirects("/hata/{0}");

//app.Run(async context => await context.Response.WriteAsync("Middleware 1"));
//app.Run(async context => await context.Response.WriteAsync("Middleware 2"));

//app.Use(async (context, next) =>
//{
//    //if (context.Request.Path.Value.Contains("education")) context.Response.Redirect("/eegitimler");
//    await context.Response.WriteAsync("Middleware 1");

//    await next.Invoke();

//    await context.Response.WriteAsync("Middleware 2");
//});


//app.Map("/test", app =>
//    app.Run(async context =>
//    {
//        //await context.Response.WriteAsync("Middleware 1");
//         context.Response.Redirect("sepet");
//    }));


//app.MapWhen(x => x.Request.Method == "GET" || x.Request.IsHttps || x.Request.Cookies.Count()!=0, app =>
//{
//    app.Run(async context => await context.Response.WriteAsync("Middleware 2"));
//});

app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();//kimlik do�rulama
app.UseAuthorization();//kimlik yetkilendirme 
app.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=home}/{action=index}/{id?}");
app.MapControllerRoute(name: "default", pattern: "{controller=home}/{action=index}/{id?}");
app.Run();

// www.abc.com/admin/contoller/action

//performans
//microsoft deste�i
//cross-platform (platform ba��ms�z / linux, macos, windows sunucularda yay�nlanabilir)
//open-source
//g�venlik
//community b�y�kl���

//Domain (Alan Ad� / isim hakk�)
//Hosting (Bar�nd�rma)