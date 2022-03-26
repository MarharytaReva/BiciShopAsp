using BiciShop.Models.ViewModels.ExcelViewModels;
using BLL.DTO;
using BLL.Services;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BiciShop.Controllers
{
    public class BiciTypeExcelController : Controller
    {
        readonly BiciTypeService biciTypeService;
        readonly BiciService biciService;
        public BiciTypeExcelController(BiciTypeService biciTypeService, BiciService biciService)
        {
            this.biciTypeService = biciTypeService;
            this.biciService = biciService;
        }
        public async Task<IActionResult> Index(TypeViewModel model)
        {
            var types = await biciTypeService.GetAllAsync();
            model.BiciTypes = types;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int typeId)
        {
            if (await biciService.IsTypeDeletable(typeId))
                await biciTypeService.DeleteAsync(typeId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Create(BiciTypeDTO biciType)
        {
           
                await biciTypeService.CreateAsync(biciType);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Update(BiciTypeDTO biciType)
        {
            try
            {
                await biciTypeService.UpdateAsync(biciType);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", new TypeViewModel() { ErrorText = "Type name is required and must be less then 50 characters"});
            } 
        }
        [HttpPost]
        public async Task<FileResult> ExportToExcel()
        {
            var types = await biciTypeService.GetAllAsync();
            return await Task.Run(() =>
            {
                DataTable dt = new DataTable("Bicicletas");
                dt.Columns.AddRange(new DataColumn[] { new DataColumn("BiciTypeId"),
                                                     new DataColumn("BiciTypeName")});
                foreach (var type in types)
                {
                    dt.Rows.Add(type.BiciTypeId, type.BiciTypeName);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BiciTypesExcelFile.xlsx");
                    }
                }
            });
        }
    }
}

