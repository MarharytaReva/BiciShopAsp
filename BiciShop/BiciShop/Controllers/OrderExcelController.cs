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
using System.Threading.Tasks;

namespace BiciShop.Controllers
{
    public class OrderExcelController : Controller
    {
        OrderService orderService;
        HandlePhaseService handlePhaseService;
       
        public OrderExcelController(OrderService orderService, HandlePhaseService handlePhaseService)
        {
            this.orderService = orderService;
            this.handlePhaseService = handlePhaseService;
        }
        public async Task<IActionResult> Index(OrderExcelViewModel model)
        {
            var res = await handlePhaseService.GetAllAsync();
            var phases = res.ToList();

            HandlePhaseDTO[] phasesArray = new HandlePhaseDTO[phases.Count];
            phases.CopyTo(phasesArray, 0);
            model.PhasesForEdit = new SelectList(phasesArray, "HandlePhaseId", "PhaseName");

            phases.Insert(0, new HandlePhaseDTO { PhaseName = BiciService.DefaultStr });
            model.Phases = new SelectList(phases, "HandlePhaseId", "PhaseName");

            var sortOrders = Enum.GetNames(typeof(OrderSort));
            model.SortOrders = new SelectList(sortOrders);
            return View(model);
        }
        [HttpPost]
        public async Task<FileResult> ImportFromExcel(IFormFile postedFile, int phaseId)
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
                        var phase = await handlePhaseService.GetItemAsync(phaseId);
                        List<OrderDTO> updatedOrders = new List<OrderDTO>();
                        foreach (int id in ids)
                        {
                            var order = await orderService.GetItemAsync(id);
                            order.HandlePhase = phase;
                            var updatedOrder = await orderService.UpdateAsync(order);
                            updatedOrders.Add(updatedOrder);
                        }
                        return ExportOrders(updatedOrders, "UpdatedOrders");
                    }
                }
            }
           catch(Exception ex)
            {
                return await Task.Run(() => ExportOrders(new List<OrderDTO>(), "UpdatedOrders"));
            }   
        }
        private FileResult ExportOrders(IEnumerable<OrderDTO> orders, string fileName)
        {
            DataTable dt = new DataTable("Bicicletas");
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("OrderId"),
                                                     new DataColumn("Date"),
                                                     new DataColumn("TotalValue"),
                                                     new DataColumn("Phone"),
                                                     new DataColumn("Email"),
                                                     new DataColumn("HandlePhaseName"),
                                                     new DataColumn("Name Lastname"),
                                                     new DataColumn("BicicletaId"),
                                                     new DataColumn("Title"),
                                                     new DataColumn("Count in order"),
                                                     new DataColumn("Bicicleta Price"),
                                                     new DataColumn("Bicicleta Discount"),
                                                     new DataColumn("Bicicleta Quantity"),
                                                     new DataColumn("BiciType"),});
            foreach (var order in orders)
            {
                foreach (var unit in order.OrderUnits)
                    dt.Rows.Add(order.OrderId, order.Date, order.TotalValue,
                        order.Phone, order.Email, order.HandlePhase.PhaseName,
                        $"{order.Name} {order.Lastname}", unit.BicicletaId, unit.Bicicleta.Title,
                        unit.Count, unit.Bicicleta.Price, unit.Bicicleta.Discount, unit.Bicicleta.Quantity, unit.Bicicleta.BiciType.BiciTypeName);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
                }
            }
        }
        [HttpPost]
        public async Task<FileResult> ExportToExcel(OrderExcelViewModel model)
        {
            var orders = await orderService.GetAllAsync(model.Offset, model.OrderQueryOptions);
            return await Task.Run(() =>
            {
                return ExportOrders(orders, "OrdersExcelFile");
            });
        }
    }
}
