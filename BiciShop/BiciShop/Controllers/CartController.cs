using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiciShop.Models;
using BiciShop.Extensions;
using BLL.Services;
using BLL.DTO;

namespace BiciShop.Controllers
{
    public class CartController : Controller
    {
        BiciService biciService;
        const string key = "Cart";
        public CartController(BiciService biciService)
        {
            this.biciService = biciService;
        }
        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetObjectAsJson<Cart>(key);
            if(cart is null)
            {
                cart = new Cart();
                HttpContext.Session.SetObjectAsJson(key, cart);
            }
            return cart;
        }
        [HttpPost]
       public async Task<IActionResult> Clear()
       {
            return await Task.Run(() =>
            {
                Cart cart = GetCart();
                cart.Clear();
                HttpContext.Session.SetObjectAsJson(key, cart);
                return RedirectToAction("Index");
            }); 
       }
        public IActionResult Index()
        {
            Cart cart = GetCart();
            return View(cart);
        }
        public ActionResult GetTotalValue()
        {
            ViewBag.Value = GetCart().GetTotalValue();
            return PartialView("TotalValueView");
        }
        public IActionResult PriceView(int bicicletaId, int? count)
        {
            Cart cart = GetCart();
            CartLine cartLine = cart.CartLines.FirstOrDefault(x => x.Bicicleta.BicicletaId == bicicletaId);
            if(cartLine != null)
            {
                if(count != null)
                {
                    cartLine.Count = (int)count;
                    HttpContext.Session.SetObjectAsJson(key, cart);
                }
                ViewBag.Discount = cartLine.Bicicleta.Discount;
                ViewBag.Count = cartLine.Count;
                ViewBag.Price = cartLine.Bicicleta.Price;
            }
            return PartialView("PriceView");
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int bicicletaId, string returnUrl)
        {
            BicicletaDTO bicicleta = await biciService.GetItemAsync(bicicletaId);
            if(bicicleta != null)
            {
                Cart cart = GetCart();
               
                cart.Add(bicicleta, 1);
                HttpContext.Session.SetObjectAsJson(key, cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int bicicletaId, string returnUrl)
        {
            BicicletaDTO bicicleta = await biciService.GetItemAsync(bicicletaId);
            if(bicicleta != null)
            {
                Cart cart = GetCart();
                cart.Remove(bicicleta);
                HttpContext.Session.SetObjectAsJson(key, cart);
            }

            return RedirectToAction("Index", returnUrl);
        }
    }
}
