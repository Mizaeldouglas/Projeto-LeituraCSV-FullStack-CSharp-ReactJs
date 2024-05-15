using Ecommerce.API.Data;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.API.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalcFreightController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public CalcFreightController(EcommerceContext context)
        {
            _context = context;
        }

        [HttpGet("{document}")]
        public async Task<ActionResult<decimal>> CalculateFreight(string document)
        {
            var client = await _context.Clients.FindAsync(document);

            if (client == null)
            {
                return NotFound();
            }

            var cep = client.CEP;
            decimal freight = 0;

            var region = await OpenCep.GetRegionOpenCep(cep);
            var product = await _context.Products.FindAsync(1);

            if (RegionFreightRates.Rates.ContainsKey(region))
            {
                freight = RegionFreightRates.Rates[region] * product.Preco;
            }
            else
            {
                if (cep.StartsWith("01000"))
                {
                    freight = 0;
                }
            }

            return freight;
        }
        
        
        [HttpGet("CalculateTotalWithFreightForAllOrders")]
        public async Task<ActionResult<List<decimal>>> CalculateTotalWithFreightForAllOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Product)
                .ToListAsync();

            var totals = new List<decimal>();
            foreach (var order in orders)
            {
                var cep = order.CEP.Replace("-", "").Replace(".", ""); // Remover traços e pontos

                var productPrice = order.Product.Preco;

                var region = await OpenCep.GetRegionOpenCep(cep);

                if (string.IsNullOrEmpty(region))
                {
                    return BadRequest($"CEP {cep} não corresponde a uma região válida.");
                }

                if (RegionFreightRates.Rates.TryGetValue(region, out var freightRate))
                {
                    var total = productPrice + (productPrice * freightRate);
                    totals.Add(total);
                }
                else
                {
                    return BadRequest($"Não foi possível encontrar uma taxa de frete para a região {region}.");
                }
            }

            return totals;
        }
        
        [HttpGet("CalculateTotalByRegion")]
        public async Task<ActionResult<Dictionary<string, decimal>>> CalculateTotalByRegion()
        {
            var orders = await _context.Orders
                .Include(o => o.Product)
                .ToListAsync();

            var totalsByRegion = new Dictionary<string, decimal>();

            foreach (var order in orders)
            {
                var cep = order.CEP.Replace("-", "").Replace(".", ""); // Remover traços e pontos

                var productPrice = order.Product.Preco;

                var region = await OpenCep.GetRegionOpenCep(cep);

                if (string.IsNullOrEmpty(region))
                {
                    return BadRequest($"CEP {cep} não corresponde a uma região válida.");
                }

                if (RegionFreightRates.Rates.TryGetValue(region, out var freightRate))
                {
                    var total = productPrice + (productPrice * freightRate);

                    if (totalsByRegion.ContainsKey(region))
                    {
                        totalsByRegion[region] += total;
                    }
                    else
                    {
                        totalsByRegion[region] = total;
                    }
                }
                else
                {
                    return BadRequest($"Não foi possível encontrar uma taxa de frete para a região {region}.");
                }
            }

            return totalsByRegion;
        }
    }
}