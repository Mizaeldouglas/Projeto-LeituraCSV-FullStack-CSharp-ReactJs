using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecommerce.API.Data;
using Ecommerce.API.Models;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public ClientController(EcommerceContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientModel>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientModel>> GetClientModel(int id)
        {
            var clientModel = await _context.Clients.FindAsync(id);

            if (clientModel == null)
            {
                return NotFound();
            }

            return clientModel;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientModel(string id, ClientModel clientModel)
        {
            if (id != clientModel.CPF_CNPJ)
            {
                return BadRequest();
            }

            _context.Entry(clientModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ClientModel>> PostClientModel(ClientModel clientModel)
        {
            _context.Clients.Add(clientModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientModel", new { id = clientModel.CPF_CNPJ }, clientModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientModel(int id)
        {
            var clientModel = await _context.Clients.FindAsync(id);
            if (clientModel == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(clientModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientModelExists(string id)
        {
            return _context.Clients.Any(e => e.CPF_CNPJ == id);
        }
    }
}
