using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace Divisima.UI.Areas.admin.Controllers
{
    [Area("admin"),Authorize]
    public class ProductPictureController : Controller
    {
        IRepository<ProductPicture> repoProductPicture;
        public ProductPictureController(IRepository<ProductPicture> _repoProductPicture)
        {
            repoProductPicture = _repoProductPicture;
        }
        public IActionResult Index(int productid)
        {
            ViewBag.ProductID = productid;
            return View(repoProductPicture.GetAll(x=>x.ProductID== productid).OrderByDescending(x=>x.ID));
        }

        public IActionResult New(int productid)
        {
            ViewBag.ProductID = productid;
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert(ProductPicture model)
        {
            if(ModelState.IsValid)
            {
                if(Request.Form.Files.Any())
                {
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productPicture"))) Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productPicture"));
                    string dosyaAdi = Request.Form.Files["Picture"].FileName;
                    using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productPicture", dosyaAdi), FileMode.Create))
                    {
                        await Request.Form.Files["Picture"].CopyToAsync(stream);
                    }
                    model.Picture = "/img/productPicture/" + dosyaAdi;
;                }

                repoProductPicture.Add(model);
                return RedirectToAction("Index",new { productid=model.ProductID });
            }
            else return RedirectToAction("New");
        }

        public IActionResult Edit(int id)
        {
            ProductPicture productPicture = repoProductPicture.GetBy(x => x.ID == id);
            if (productPicture != null) return View(productPicture);
            else return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProductPicture model)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Any())
                {
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productPicture"))) Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productPicture"));
                    string dosyaAdi = Request.Form.Files["Picture"].FileName;
                    using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productPicture", dosyaAdi), FileMode.Create))
                    {
                        await Request.Form.Files["Picture"].CopyToAsync(stream);
                    }
                    model.Picture = "/img/productPicture/" + dosyaAdi;                   
                }
                repoProductPicture.Update(model);
                return RedirectToAction("Index", new { productid = model.ProductID });
            }
            else return RedirectToAction("New");
        }

        public IActionResult Delete(int id)
        {
            ProductPicture productPicture = repoProductPicture.GetBy(x => x.ID == id);
            if (productPicture != null)
            {
                if (!string.IsNullOrEmpty(productPicture.Picture))
                {
                    string _pathFile = Directory.GetCurrentDirectory() + string.Format(@"\wwwroot") + productPicture.Picture.Replace("/", "\\");
                    FileInfo fileInfo = new FileInfo(_pathFile);
                    if (fileInfo.Exists) fileInfo.Delete();
                }

                repoProductPicture.Delete(productPicture);
            }
            return RedirectToAction("Index");
        }
    }
}
