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
    public class HandlePhaseExcelController : Controller
    {
        HandlePhaseService handlePhaseService;
        public HandlePhaseExcelController(HandlePhaseService handlePhaseService)
        {
            this.handlePhaseService = handlePhaseService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<FileResult> ExportToExcel()
        {
            var phases = await handlePhaseService.GetAllAsync();
            return await Task.Run(() =>
            {
                DataTable dt = new DataTable("Bicicletas");
                dt.Columns.AddRange(new DataColumn[] { new DataColumn("HandlePhaseId"),
                                                     new DataColumn("PhaseName")});
                foreach (var phase in phases)
                {
                    dt.Rows.Add(phase.HandlePhaseId, phase.PhaseName);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "HandlePhasesExcelFile.xlsx");
                    }
                }
            });
        }
    }
}
