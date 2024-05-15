using Ecommerce.API.Common.Utilities;
using Ecommerce.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalcDeliveryTimeController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public CalcDeliveryTimeController(EcommerceContext context)
        {
            _context = context;
        }

        [HttpGet("{document}")]
        public async Task<ActionResult<int>> CalculateDeliveryTime(string document)
        {
            var client = await _context.Clients.FindAsync(document);

            if (client == null)
            {
                return NotFound();
            }

            var cep = client.CEP;
            var region = await OpenCep.GetRegionOpenCep(cep);

            int deliveryTime;

            if (RegionDeliveryTimes.Times.ContainsKey(region))
            {
                deliveryTime = RegionDeliveryTimes.Times[region];
            }
            else
            {
                deliveryTime = cep.StartsWith("01000") ? 0 : 10; 
            }

            return deliveryTime;
        }
    }
}