using BiciShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using BiciShop.Models.Services;
using BLL.Services;
using BLL.DTO;
using System.Text;
using BLL.DTO.QuerySettings;
using BiciShop.Extensions;

namespace BiciShop.Controllers
{
    public class HomeController : Controller
    {
        BiciService biciService;
        BiciTypeService biciTypeService;
        private const int offset = 10;
        const string key = "QueryOptions";

        public HomeController(BiciService biciService, BiciTypeService biciTypeService)
        {
            this.biciService = biciService;
            this.biciTypeService = biciTypeService;
        }

       
        private QueryOptions GetQueryOptions()
        {
            QueryOptions queryOptions = HttpContext.Session.GetObjectAsJson<QueryOptions>(key);
            if (queryOptions is null)
            {
                queryOptions = new QueryOptions();
                HttpContext.Session.SetObjectAsJson(key, queryOptions);
            }
            return queryOptions;
        }
        public async Task<IActionResult> Index(HomeIndexData model)
        {
            var colors = await biciService.GetAllColorsAsync();
            var types = await biciTypeService.GetAllNamesAsync();

            var sortOrders = Enum.GetNames(typeof(OrderSort));
            var sortFields = Enum.GetNames(typeof(FieldSort)).Select(x => new 
            { 
                Text = EnumService.AddSpacesToName(x),
                Value = x
            });
            colors.Insert(0, BiciService.DefaultStr);
            types.Insert(0, BiciService.DefaultStr);

            model.Colors = new SelectList(colors, model.QueryOptions.FiltrationOptions.Color);
            model.BiciTypes = new SelectList(types, model.QueryOptions.FiltrationOptions.BiciType);
            model.SortOrders = new SelectList(sortOrders, model.QueryOptions.SortOptions.OrderSort);
            model.SortFields = new SelectList(sortFields, "Value", "Text", model.QueryOptions.SortOptions.FieldSort);
            model.QueryOptions = GetQueryOptions();

            return View(model);
        }
        public async Task<IActionResult> BiciList(HomeIndexData model)
        {
            if (model.FirstLoad)
                model.QueryOptions = GetQueryOptions();
            else
                HttpContext.Session.SetObjectAsJson(key, model.QueryOptions);

            model.PageCount = await biciService.GetPageCount(offset, model.QueryOptions);
            model.Bicicletas = await biciService.GetAllAsync(offset, model.QueryOptions);
            

            return PartialView(model);
        }
       
        public async Task<IActionResult> BiciInfo(int? id)
        {
            if (id is null)
            {
                return RedirectToAction("Index");
            }
            BicicletaDTO bicicleta = await biciService.GetItemAsync((int)id);
            return View(bicicleta);
        } 
    }
}
