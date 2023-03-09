using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Divisima.UI.Models;
using Divisima.UI.Tools;
using Divisima.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Divisima.UI.Controllers
{
    public class ProductController : Controller
    {
        IRepository<Product> repoProduct;
        public ProductController(IRepository<Product> _repoProduct)
        {
            repoProduct = _repoProduct;
        }
        public IActionResult Index()// Tüm Ürünler
        {
            return View();
        }

        [Route("/urun/{name}-{id}")]
        public IActionResult Detail(string name,int id)// Ürün Detayı
        {
            //ViewBag.SessionDegeri = HttpContext.Session.GetString("S1");//session okuma
            //ViewBag.SessionDegeri = JsonConvert.DeserializeObject<IEnumerable<Product>>(HttpContext.Session.GetString("S1"));

            ViewBag.CerezDegeri = Request.Cookies["C1"];

            Product product = repoProduct.GetAll(x => x.ID == id).Include(x=>x.Category).Include(x=>x.ProductPictures).FirstOrDefault();
            if(product!=null)
            {
                ViewBag.Title = product.Name;
                ProductVM productVM = new ProductVM { 
                    Product= product,
                    RelatedProducts=repoProduct.GetAll(x=>x.CategoryID==product.CategoryID && x.ID!=product.ID).Include(x=>x.ProductPictures)
                };
                return View(productVM);
            }
            else return Redirect("/");
        }

        [Route("/urun/ara"),HttpPost]
        public IActionResult getSearchProduct(string search)// Tüm Ürünler
        {
            return Json(repoProduct.GetAll(x=>x.Name.ToLower().Contains(search.ToLower()) || x.Description.ToLower().Contains(search.ToLower())).Include(x=>x.ProductPictures).Select(x=>new SearchProduct {ProductName=x.Name,ProductPicture=x.ProductPictures.FirstOrDefault().Picture,ProductLink="/urun/"+GeneralTool.getURL(x.Name)+"-"+x.ID }));
        }
    }
}
