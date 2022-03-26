using BiciShop.Extensions;
using BiciShop.Models;
using BiciShop.Models.ViewModels;
using BLL.DTO;
using BLL.DTO.QuerySettings;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiciShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        OrderService orderService;
        UserManager<IdentityUser> userManager;
        BiciService biciService;
        const int offset = 10;
        const string key = "Cart";
        HandlePhaseService phaseService;
        const string orderKey = "OrderQueryOptions";
        public OrderController(OrderService orderService, UserManager<IdentityUser> userManager, BiciService biciService, HandlePhaseService phaseService)
        {
            this.orderService = orderService;
            this.userManager = userManager;
            this.biciService = biciService;
            this.phaseService = phaseService;
        }
        private async Task<List<OrderUnitDTO>> GetCartUnits()
        {
            return await Task.Run(() =>
            {
                Cart cart = HttpContext.Session.GetObjectAsJson<Cart>(key);
                if (cart is null)
                {
                    cart = new Cart();
                    HttpContext.Session.SetObjectAsJson(key, cart);
                }
                List<CartLine> cartLines = cart.CartLines;
                return cartLines.Select(x => new OrderUnitDTO()
                {
                    Bicicleta = x.Bicicleta,
                    BicicletaId = x.Bicicleta.BicicletaId,
                    Count = x.Count
                }).ToList();
            }); 
        }
        private async Task<List<OrderUnitDTO>> GetUnit(int id)
        {
            BicicletaDTO bicicleta = await biciService.GetItemAsync(id);
            return new List<OrderUnitDTO>()
                {
                    new OrderUnitDTO()
                    {
                        Count = 1,
                        BicicletaId = bicicleta.BicicletaId,
                        Bicicleta = bicicleta
                    }
                };
        }
        private OrderQueryOptions GetOrderQueryOptions()
        {
            OrderQueryOptions queryOptions = HttpContext.Session.GetObjectAsJson<OrderQueryOptions>(orderKey);
            if (queryOptions is null)
            {
                queryOptions = new OrderQueryOptions();
                HttpContext.Session.SetObjectAsJson(key, queryOptions);
            }
            return queryOptions;
        }
        public async Task<IActionResult> Index(OrderIndexViewModel model)
        {
            await Task.Run(() =>
           {
               var sortOrders = Enum.GetNames(typeof(OrderSort));
               model.SortOrders = new SelectList(sortOrders);
               model.OrderQueryOptions = GetOrderQueryOptions();
           });
            return View("Index", model);
        }
        public async Task<IActionResult> OrderList(OrderIndexViewModel model)
        {
            if (model.FirstLoad)
                model.OrderQueryOptions = GetOrderQueryOptions();
            else
                HttpContext.Session.SetObjectAsJson(key, model.OrderQueryOptions);

            string userId = userManager.GetUserId(User);
            int pageCount = await orderService.GetUserOrdersCount(userId, offset);
            var orders = await orderService.GetAllAsync(offset, userId, model.OrderQueryOptions);
            model.PageCount = pageCount;
            model.Orders = orders;
            return PartialView(model);
        }
        public async Task<IActionResult> MakeOrder(int? id)
        {
            OrderDTO order = new OrderDTO();
            order.Email = User.Identity.Name;
            order.UserKey = userManager.GetUserId(User);
           
            if (id is null)
            {
                id = 0;
                order.OrderUnits = await GetCartUnits();
            }
            else
            {
                BicicletaDTO bicicleta = await biciService.GetItemAsync((int)id);
                if (bicicleta is null)
                    return RedirectToAction("Index");
                if (bicicleta.Quantity == 0)
                    return RedirectToAction("Index", "Home");
                order.OrderUnits = new List<OrderUnitDTO>()
                {
                    new OrderUnitDTO()
                    {
                        Count = 1,
                        Bicicleta = bicicleta
                    }
                };
            }
            order.TotalValue = order.GetTotalValue();
            return View(new MakeOrderViewModel() { Order = order, Id = (int)id});
        }
        [HttpPost]
        public async Task<IActionResult> MakeOrder(MakeOrderViewModel model)
        {
            OrderDTO order = model.Order;
            try
            {
                if (ModelState.IsValid)
                {
                    order.Date = DateTime.Now;
                    order.HandlePhase = await phaseService.GetDefaultPhase();
                    order.OrderUnits = model.Id == 0 ? await GetCartUnits() : await GetUnit(model.Id);
                    foreach(var unit in order.OrderUnits)
                    {
                        unit.Bicicleta.Quantity -= unit.Count;
                        await biciService.UpdateAsync(unit.Bicicleta);
                    }
                    await orderService.CreateAsync(order);

                    await Task.Run(() => GmailManager.Send(order));
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    return View(model);
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
       [HttpPost]
       public async Task<IActionResult> Delete(int orderId)
        {
            OrderDTO order = await orderService.GetItemAsync(orderId);
            if(order != null)
            {
                await orderService.DeleteAsync(orderId);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> OrderInfo(int id)
        {
            OrderDTO order = await orderService.GetItemAsync(id);
            if (order != null)
            {
                return View("OrderInfo", order);
            }
            return RedirectToAction("Index");
        }
    }
}
