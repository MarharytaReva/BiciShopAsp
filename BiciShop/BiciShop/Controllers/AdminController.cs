using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiciShop.Models;
using Microsoft.AspNetCore.Authorization;
using BiciShop.Models.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using BiciShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BLL.Services;
using BLL.DTO;

namespace BiciShop.Controllers
{
    [Authorize(Roles = "admin, head")]
    public class AdminController : Controller
    {

        BiciService biciService;
        BiciTypeService biciTypeService;
        OrderService orderService;
        public AdminController(BiciService biciService, BiciTypeService biciTypeService, OrderService orderService)
        {
            this.biciService = biciService;
            this.biciTypeService = biciTypeService;
            this.orderService = orderService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create(int? id)
        {
            var types = await biciTypeService.GetAllAsync();
            var typesSelect = new SelectList(types, "BiciTypeId", "BiciTypeName");
            if (id == null)
            {
                return View(new CreateBiciViewModel(){ BiciTypes = typesSelect }); 
            }
            else
            {
                BicicletaDTO bicicleta = await biciService.GetItemAsync((int)id);
                return View(new CreateBiciViewModel()
                {
                    Bicicleta = bicicleta,
                    BiciTypes = typesSelect
                });
            }
        }
        private BicicletaDTO SetPhoto(CreateBiciViewModel model)
        {
            BicicletaDTO bicicleta = model.Bicicleta;
            var file = model.File;
            if (string.IsNullOrEmpty(bicicleta.Photo) || file != null)
                bicicleta.Photo = PhotoConvert.GetPhotoBase64(file);
          
            return bicicleta;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBiciViewModel model)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() =>
                {
                    BicicletaDTO bicicletaWithPhoto = SetPhoto(model);
                    biciService.CreateOrUpdate(bicicletaWithPhoto);
                });

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if(await orderService.IsDeletable(id))
                await biciService.DeleteAsync(id);
            
            return RedirectToAction("Index", "Home");
        }
    }
}
