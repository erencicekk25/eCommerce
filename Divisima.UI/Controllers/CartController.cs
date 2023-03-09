using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Divisima.UI.Models;
using Divisima.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Divisima.UI.Controllers
{
    public class CartController : Controller
    {
        IRepository<Product> repoProduct;
        IRepository<Country> repoCountry;
        IRepository<City> repoCity;
        IRepository<District> repoDistrict;
        IRepository<Order> repoOrder;
        IRepository<OrderDetail> repoOrderDetail;
        public CartController(IRepository<Product> _repoProduct, IRepository<Country> _repoCountry, IRepository<City> _repoCity, IRepository<District> _repoDistrict, IRepository<Order> _repoOrder, IRepository<OrderDetail> _repoOrderDetail)
        {
            repoProduct = _repoProduct;
            repoCountry = _repoCountry;
            repoCity = _repoCity;
            repoDistrict = _repoDistrict;
            repoOrder = _repoOrder;
            repoOrderDetail = _repoOrderDetail;
        }

        [Route("/sepet/sepeteekle"), HttpPost]
        public string AddCart(int productid, int quantity)
        {
            Product product = repoProduct.GetAll(x => x.ID == productid).Include(i => i.ProductPictures).FirstOrDefault();
            if (product != null)
            {
                Cart cart = new Cart
                {
                    ProductID = productid,
                    ProductName = product.Name,
                    ProductPicture = product.ProductPictures.FirstOrDefault().Picture,
                    ProductPrice = product.Price,
                    Quantity = quantity
                };
                List<Cart> carts = new List<Cart>();
                bool varmi = false;
                if (Request.Cookies["MyCart"] != null)//daha önce bir ürün sepete eklenmiş ise
                {
                    carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
                    foreach (Cart c in carts)
                    {
                        if (c.ProductID == productid)
                        {
                            varmi = true;
                            c.Quantity += quantity;
                            break;
                        }
                    }
                }
                if (varmi == false) carts.Add(cart);
                CookieOptions options = new();
                options.Expires = DateTime.Now.AddHours(3);
                Response.Cookies.Append("MyCart", JsonConvert.SerializeObject(carts), options);
                return product.Name;
            }
            else return "~ Ürün bulunamadı...";
        }

        [Route("/sepet/sepettensil"), HttpPost]
        public string RemoveCart(int productid)
        {
            string rtn = "";
            if (Request.Cookies["MyCart"] != null)
            {
                List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
                bool varmi = false;
                foreach (Cart c in carts)
                {
                    if (c.ProductID == productid)
                    {
                        varmi = true;
                        carts.Remove(c);
                        break;
                    }
                }
                if(varmi==true)
                {
                    CookieOptions options = new();
                    options.Expires = DateTime.Now.AddHours(3);
                    Response.Cookies.Append("MyCart", JsonConvert.SerializeObject(carts), options);
                    rtn = "OK";
                }
            }
            return rtn;
        }

        [Route("/sepet/sepetsayisi")]
        public int CartCount()
        {
            int geri = 0;
            if (Request.Cookies["MyCart"] != null)
            {
                List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
                geri = carts.Sum(x => x.Quantity);
            }
            return geri;
        }

        [Route("/sepet")]
        public IActionResult Index()
        {
            if (Request.Cookies["MyCart"] != null)
            {
                List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
                return View(carts);
            }
            else return Redirect("/");
        }

        [Route("/sepet/alisveristamamla")]
        public IActionResult CheckOut()
        {
            ViewBag.ShippingFee = 1000;//1000 TL üzeri kargo bedava
            if (Request.Cookies["MyCart"] != null)
            {
                List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
                CheckoutVM checkoutVM = new CheckoutVM
                {
                    Order = new Order(),
                    Carts = carts,
                    Countries = repoCountry.GetAll().OrderBy(x => x.Name)
                };
                return View(checkoutVM);
            }
            else return Redirect("/");
        }

        [Route("/sepet/alisveristamamla"),HttpPost,ValidateAntiForgeryToken]
        public IActionResult CheckOut(CheckoutVM model)
        {
            if(model.Order.PaymentOption==EPaymentOption.KrediKartı)
            {
                //Kredi kartı kontrol
            }
            model.Order.RecDate = DateTime.Now;
            string orderNumber = DateTime.Now.Microsecond.ToString()+DateTime.Now.Minute.ToString()+DateTime.Now.Second.ToString()+DateTime.Now.Hour.ToString() + DateTime.Now.Microsecond.ToString() + DateTime.Now.Microsecond.ToString();
            if (orderNumber.Length > 20) orderNumber = orderNumber.Substring(0, 20);
            model.Order.OrderNumber = orderNumber;
            model.Order.OrderStatus = EOrderStatus.Hazırlanıyor;
            if (model.Order.Country != null)
            {
                int countryID= Convert.ToInt32(model.Order.Country);
                model.Order.Country = repoCountry.GetBy(x => x.ID == countryID).Name;
            }
            if (model.Order.City != null)
            {
                int cityID = Convert.ToInt32(model.Order.City);
                model.Order.City = repoCity.GetBy(x => x.ID == cityID).Name;
            }
            if (model.Order.Distinct != null)
            {
                int distinctID = Convert.ToInt32(model.Order.Distinct);
                model.Order.Distinct = repoDistrict.GetBy(x => x.ID == distinctID).Name;
            }
            repoOrder.Add(model.Order);
            List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
            foreach (Cart cart in carts)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    OrderID= model.Order.ID,
                    ProductName=cart.ProductName,
                    ProductID=cart.ProductID,
                    ProductPicture=cart.ProductPicture,
                    ProductPrice=cart.ProductPrice,
                    Quantity=cart.Quantity
                };
                repoOrderDetail.Add(orderDetail);
            }
            MailGonder(model.Order.MailAddress);
            //Firmaya Mail Gönder;
            return Redirect("/");
        }

        [Route("/sepet/sehirgetir")]
        public IActionResult getCity(int countryid)
        {
            return Json(repoCity.GetAll(x => x.CountryID == countryid).OrderBy(x => x.Name));
        }

        [Route("/sepet/ilcegetir")]
        public IActionResult getDistinct(int cityid)
        {
            return Json(repoDistrict.GetAll(x => x.CityID == cityid).OrderBy(x => x.Name));
        }

        void MailGonder(string to)
        {
            SmtpClient smtpClient = new();
            smtpClient.Host = "mail.biltekno.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = false;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("test@biltekno.com", "tstMail1");
            MailMessage mailMessage = new();
            mailMessage.From = new MailAddress("test@biltekno.com");
            mailMessage.To.Add(to);
            mailMessage.Subject = "Sipariş";
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.Body = "Siparişiniz başarıyla alındı...";
            smtpClient.Send(mailMessage);
        }
    }
}
