using Azure;
using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Divisima.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Divisima.UI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Slide> repoSlide;
        IRepository<Product> repoProduct;
        public HomeController(IRepository<Slide> _repoSlide, IRepository<Product> _repoProduct)
        {
            repoSlide = _repoSlide;
            repoProduct = _repoProduct;
        }
        //public IActionResult Index()
        //{
        //    return View(repoSlide.GetAll());//select * from Admin  //
        //}

        public IActionResult Index()
        {

            //HttpContext.Session.SetString("S1", "DivisimaAuthh");//session create
            //IEnumerable<Product> products = repoProduct.GetAll();
            //HttpContext.Session.SetString("S1",JsonConvert.SerializeObject(products));
            //CookieOptions cookie = new CookieOptions();
            //cookie.Expires = DateTime.Now.AddHours(10);
            //Response.Cookies.Append("C1", "Infotech Academy", cookie);
            
            IndexVM indexVM = new IndexVM {
                Slides = repoSlide.GetAll().OrderBy(x => x.DisplayIndex),
                LatestProducts=repoProduct.GetAll().Include(x=>x.ProductPictures).OrderByDescending(x=>x.ID).Take(8),
                SalesProducts = repoProduct.GetAll().Include(x => x.ProductPictures).OrderBy(x => Guid.NewGuid()).Take(8)
            };
            return View(indexVM);
        }
    }
}
