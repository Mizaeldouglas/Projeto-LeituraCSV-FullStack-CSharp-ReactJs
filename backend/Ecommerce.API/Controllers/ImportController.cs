using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Ecommerce.API.Data;
using Ecommerce.API.Models;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public ImportController(EcommerceContext context)
        {
            _context = context;
        }

        // Temp/Planilha.xlsx
        // Temp/Planilha teste.xlsx

        [HttpPost]
        [HttpPost]
        public async Task<ActionResult> ImportFromExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo file = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                // HashSet para armazenar os documentos únicos
                HashSet<string> uniqueDocuments = new HashSet<string>();

                for (int row = 2; row <= rowCount; row++)
                {
                    var data = worksheet.Cells[row, 6].Value != null
                        ? DateTime.FromOADate(double.Parse(worksheet.Cells[row, 6].Value.ToString()))
                        : DateTime.MinValue;

                    string productName = worksheet.Cells[row, 4].Value != null
                        ? worksheet.Cells[row, 4].Value.ToString()
                        : string.Empty;
                    ProductModel product = _context.Products.FirstOrDefault(p => p.Name == productName);

                    string clientCPF_CNPJ = worksheet.Cells[row, 1].Value?.ToString().Replace(".", "").Replace("-", "")
                        .Trim();

                    if (uniqueDocuments.Contains(clientCPF_CNPJ))
                    {
                        continue;
                    }

                    uniqueDocuments.Add(clientCPF_CNPJ);

                    ClientModel client = null;

                    if (!string.IsNullOrEmpty(clientCPF_CNPJ))
                    {
                        client = _context.Clients.FirstOrDefault(c => c.CPF_CNPJ == clientCPF_CNPJ);

                        if (client == null)
                        {
                            client = new ClientModel
                            {
                                CPF_CNPJ = clientCPF_CNPJ,
                                Name = worksheet.Cells[row, 2].Value?.ToString(),
                                CEP = worksheet.Cells[row, 3].Value?.ToString().Replace(".", "").Replace("-", ""),
                            };

                            _context.Clients.Add(client);
                        }
                    }

                    if (product != null && client != null)
                    {
                        OrderModel order = new OrderModel
                        {
                            CPF_CNPJ = clientCPF_CNPJ,
                            Name = worksheet.Cells[row, 2].Value.ToString(),
                            CEP = worksheet.Cells[row, 3].Value.ToString(),
                            NumberOrder = int.Parse(worksheet.Cells[row, 5].Value.ToString()),
                            Data = data,
                            Product = product,
                            ClientCPF_CNPJ = clientCPF_CNPJ,
                            Client = client
                        };

                        _context.Orders.Add(order);
                    }
                }

                await _context.SaveChangesAsync();
            }

            return Ok();
        }


        [HttpPost("UploadExcel")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is not selected");
            }

            var fileName = file.FileName;
            var path = Path.Combine("Temp", fileName);

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), path);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var filePath = new FilePathModel
            {
                Path = path
            };
            _context.FilePaths.Add(filePath);
            await _context.SaveChangesAsync();

            return Ok();
        }



        [HttpGet("GetAllFiles")]
        public async Task<ActionResult<IEnumerable<FilePathModel>>> GetAllFiles()
        {
            return await _context.FilePaths.ToListAsync();
        }
    }
}
