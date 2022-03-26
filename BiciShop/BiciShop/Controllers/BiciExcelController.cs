using BiciShop.Models.Services;
using BiciShop.Models.ViewModels.ExcelViewModels;
using BLL.DTO;
using BLL.Services;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiciShop.Controllers
{
    public class BiciExcelController : Controller
    {
        readonly BiciService biciService;
        readonly BiciTypeService biciTypeService;
        readonly OrderService orderService;
        readonly Func<IXLCell, BicicletaDTO, BicicletaDTO>[] funcs;
        public BiciExcelController(BiciService biciService, BiciTypeService biciTypeService, OrderService orderService)
        {
            this.biciService = biciService;
            this.biciTypeService = biciTypeService;
            this.orderService = orderService;
            funcs = new Func<IXLCell, BicicletaDTO, BicicletaDTO>[]
           {
                new Func<IXLCell, BicicletaDTO, BicicletaDTO>((cell, bici) =>
                {
                    bici.BicicletaId = Convert.ToInt32(cell.Value);
                    return bici;
                }),
                new Func<IXLCell, BicicletaDTO, BicicletaDTO>((cell, bici) =>
                {
                    bici.Title = cell.Value.ToString();
                    return bici;
                }),
                new Func<IXLCell, BicicletaDTO, BicicletaDTO>((cell, bici) =>
                {
                    bici.Discount = Convert.ToInt32(cell.Value);
                    return bici;
                }),
                new Func<IXLCell, BicicletaDTO, BicicletaDTO>((cell, bici) =>
                {
                    bici.Price = Convert.ToDouble(cell.Value);
                    return bici;
                }),
                new Func<IXLCell, BicicletaDTO, BicicletaDTO>((cell, bici) =>
                {
                    bici.Weight = Convert.ToDouble(cell.Value);
                    return bici;
                }),
                new Func<IXLCell, BicicletaDTO, BicicletaDTO>((cell, bici) =>
                {
                    bici.IssueYear = Convert.ToInt32(cell.Value);
                    return bici;
                }),
                new Func<IXLCell, BicicletaDTO, BicicletaDTO>((cell, bici) =>
                {
                    bici.WheelDiameter = Convert.ToSingle(cell.Value);
                    return bici;
                }),
                new Func<IXLCell, BicicletaDTO, BicicletaDTO>((cell, bici) =>
                {
                    bici.Quantity = Convert.ToInt32(cell.Value);
                    return bici;
                }),
                new Func<IXLCell, BicicletaDTO, BicicletaDTO>((cell, bici) =>
                {
                    bici.Color = cell.Value.ToString();
                    return bici;
                }),
                new Func<IXLCell, BicicletaDTO, BicicletaDTO>((cell, bici) =>
                {
                    string typeName = cell.Value.ToString();
                    var type = biciTypeService.GetItem(typeName);
                    if(type is null)
                    {
                       type =  biciTypeService.Create(new BiciTypeDTO() { BiciTypeName = typeName});
                    }
                    bici.BiciType = type;
                    return bici;
                })
           };
        }
        private string GetBase64FromCell(ClosedXML.Excel.Drawings.IXLPicture picture)
        {
            byte[] bytes;
            using (var memoryStream = picture.ImageStream)
            {
                bytes = memoryStream.ToArray();
            }
            string base64 = Convert.ToBase64String(bytes);
            return base64; 
        }
        private List<BicicletaDTO> ImportBiciList(IFormFile postedFile, string readRange, int counterDefaultValue)
        {
            using (var ms = new MemoryStream())
            {
                postedFile.CopyTo(ms);
                using (XLWorkbook workbook = new XLWorkbook(ms))
                {
                    IXLWorksheet worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed().ToList();
                    rows.RemoveAt(0);

                    var pics = worksheet.Pictures;
                    Dictionary<int, ClosedXML.Excel.Drawings.IXLPicture> picsPairs = new Dictionary<int, ClosedXML.Excel.Drawings.IXLPicture>();
                    foreach (var pic in pics)
                    {
                        int rowNumber = pic.TopLeftCell.Address.RowNumber;
                        picsPairs.Add(rowNumber, pic);
                    }

                    List<BicicletaDTO> bicis = new List<BicicletaDTO>();
                    foreach (IXLRow row in rows)
                    {
                        int cellCounter = counterDefaultValue;
                        BicicletaDTO bici = new BicicletaDTO();
                        foreach (var cell in row.Cells(readRange))
                        {
                            funcs[cellCounter].Invoke(cell, bici);
                            cellCounter++;
                        }
                        ClosedXML.Excel.Drawings.IXLPicture pic;
                        if (picsPairs.TryGetValue(row.RowNumber(), out pic))
                            bici.Photo = GetBase64FromCell(pic);

                        bicis.Add(bici);
                    }
                    return bicis;
                }
            }
        }
        [HttpPost]
        public async Task<ActionResult> EditFromExcel(IFormFile postedFile)
        {
            try
            {
                if (postedFile is null)
                    return RedirectToAction("Index");
                var bicis = await Task.Run(() => ImportBiciList(postedFile, "1:10", 0));
                foreach (var bici in bicis)
                {
                    await biciService.UpdateAsync(bici);
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", new BiciViewModel() { ErrorText = "There is error in the table"});
            }
        }
        [HttpPost]
        public async Task<FileResult> AddFromExcel(IFormFile postedFile)
        {
           try
            {
                var bicis = await Task.Run(() => ImportBiciList(postedFile, "1:9", 1));
                foreach (var bici in bicis)
                {
                    await biciService.CreateAsync(bici);
                }
                return await Task.Run(() => ExportBicis(bicis, "AddedBicis"));
            }
           catch(Exception ex)
           {
                return await Task.Run(() => ExportBicis(new List<BicicletaDTO>(), "AddedBicis"));
           }
        }
        public async Task<IActionResult> Index(BiciViewModel model)
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
           
            return View(model);
        }
        [HttpPost]
        public async Task<FileResult> Delete(IFormFile postedFile)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    postedFile.CopyTo(ms);
                    using (XLWorkbook workbook = new XLWorkbook(ms))
                    {
                        IXLWorksheet worksheet = workbook.Worksheet(1);
                        var rows = worksheet.RowsUsed().ToList();

                        rows.RemoveAt(0);
                        string readRange = "1:1";
                        List<int> ids = new List<int>();
                        foreach (IXLRow row in rows)
                        {
                            int id = Convert.ToInt32(row.Cells(readRange).First().Value);
                            ids.Add(id);
                        }
                        ids = ids.Distinct().ToList();
                        List<BicicletaDTO> notDeletedBicis = new List<BicicletaDTO>();
                        foreach (int id in ids)
                        {
                            if (await orderService.IsDeletable(id))
                                await biciService.DeleteAsync(id);
                            else
                            {
                                var bici = await biciService.GetItemAsync(id);
                                notDeletedBicis.Add(bici);
                            }
                        }
                        return await Task.Run(() => ExportBicis(notDeletedBicis, "NotDeletedBicis"));
                    }
                }
            }
            catch(Exception ex)
            {
                return await Task.Run(() => ExportBicis(new List<BicicletaDTO>(), "AllWasNotDeleted"));
            }
        }
        private FileResult ExportBicis(List<BicicletaDTO> bicis, string fileName)
        {
            DataTable dt = new DataTable("Bicicletas");
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("BicicletaId"),
                                                     new DataColumn("Title"),
                                                     new DataColumn("Discount"),
                                                     new DataColumn("Price"),
                                                     new DataColumn("Weight"),
                                                     new DataColumn("IssueYear"),
                                                     new DataColumn("WheelDiameter"),
                                                     new DataColumn("Quantity"),
                                                     new DataColumn("Color"),
                                                     new DataColumn("BiciType"),
                                                     new DataColumn("Photo")
                                                     });
            foreach (var bici in bicis)
            {
                    dt.Rows.Add(bici.BicicletaId, bici.Title, bici.Discount, bici.Price,
                    bici.Weight, bici.IssueYear, bici.WheelDiameter, bici.Quantity,
                    bici.Color, bici.BiciType.BiciTypeName, "");

            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt);

                int size = 50;
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    for (int column = dt.Columns.Count - 1; column < dt.Columns.Count; column++)
                    {
                        byte[] bytes = Convert.FromBase64String(bicis[row].Photo);
                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            var cell = ws.Cell(row + 2, column + 1);
                            var image = ws.AddPicture(ms).MoveTo(cell);
                            image.Width = size;
                            image.Height = size;
                        }
                    }
                }
                foreach (var row in ws.Rows())
                {
                    row.Height = size;
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
                }
            }
        }
        [HttpPost]
        public async Task<FileResult> ExportToExcel(BiciViewModel model)
        {
            var res = await biciService.GetAllAsync(model.Offset, model.QueryOptions);
            var bicis = res.ToList();
            return await Task.Run(() =>
            {
                return ExportBicis(bicis, "BicisExelFile");
            });
        }
    }
}
