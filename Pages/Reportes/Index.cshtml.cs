using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using EstacionamientoR.Models;
using EstacionamientoR.Services;

namespace EstacionamientoR.Pages.Reportes
{
    public class IndexModel : PageModel
    {
        private readonly ReporteService _reporteService;

        public IndexModel(ReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        public decimal TotalIngresos { get; set; }

        public List<Pago> Pagos { get; set; }

        public async Task OnGetAsync()
        {
            TotalIngresos =
                await _reporteService.ObtenerTotalIngresos();

            Pagos =
                await _reporteService.ObtenerPagos();
        }

        public async Task<FileResult> OnPostExportarExcelAsync()
        {
            var pagos =
                await _reporteService.ObtenerPagos();

            ExcelPackage.License.SetNonCommercialPersonal("EstacionamientoR");

            using var package = new ExcelPackage();

            var worksheet =
                package.Workbook.Worksheets.Add("Pagos");

            worksheet.Cells[1, 1].Value = "Vehiculo ID";
            worksheet.Cells[1, 2].Value = "Total";
            worksheet.Cells[1, 3].Value = "Metodo";
            worksheet.Cells[1, 4].Value = "Fecha";

            int row = 2;

            foreach (var item in pagos)
            {
                worksheet.Cells[row, 1].Value =
                    item.VehiculoId;

                worksheet.Cells[row, 2].Value =
                    item.Total;

                worksheet.Cells[row, 3].Value =
                    item.MetodoPago;

                worksheet.Cells[row, 4].Value =
                    item.FechaPago.ToString();

                row++;
            }

            var bytes = package.GetAsByteArray();

            return File(
                bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "ReportePagos.xlsx");
        }
    }
}