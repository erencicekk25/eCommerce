using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Divisima.UI.Tools;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Divisima.UI.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class HomeController : Controller
    {
        IRepository<Admin> repoAdmin;
        public HomeController(IRepository<Admin> _repoAdmin)
        {
            repoAdmin = _repoAdmin;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous, Route("/admin/login")]
        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [AllowAnonymous, Route("/admin/login"), HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, string ReturnUrl)
        {
            string md5Password = GeneralTool.getMD5(password);
            Admin admin = repoAdmin.GetBy(x => x.Username == username && x.Password == md5Password);
            if (admin != null)//eğer bir kayıt dönüyorsa
            {
                List<Claim> claims = new List<Claim> {
                    new Claim(ClaimTypes.PrimarySid,admin.ID.ToString()),
                   new Claim(ClaimTypes.Name,admin.NameSurname)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "DivisimaAuth");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties() { IsPersistent = true, ExpiresUtc = DateTime.Now.AddHours(1) });
                admin.LastLoginDate = DateTime.Now;
                admin.LastLoginIP = HttpContext.Connection.RemoteIpAddress.ToString();
                repoAdmin.Update(admin,x=>x.LastLoginDate,y=>y.LastLoginIP);
                if (!string.IsNullOrEmpty(ReturnUrl)) return Redirect(ReturnUrl);
                else return Redirect("/admin");
            }
            else TempData["bilgi"] = "Geçersiz kullanıcı adı veya şifre";
            return RedirectToAction("Login");
        }

        [Route("/admin/logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}