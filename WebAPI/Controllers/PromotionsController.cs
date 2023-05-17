using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly SignatureContext _context;

        public PromotionsController(SignatureContext context)
        {
            _context = context;
        }

        // GET: api/Promotions
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Promotion>>> GetPromotions()
        {
          if (_context.Promotions == null)
          {
              return NotFound();
          }
            return await _context.Promotions.ToListAsync();
        }

        // GET: api/Promotions/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Promotion>> GetPromotion(int id)
        {
          if (_context.Promotions == null)
          {
              return NotFound();
          }
            var promotion = await _context.Promotions.FindAsync(id);

            if (promotion == null)
            {
                return NotFound();
            }

            return promotion;
        }

        // GET: api/Promotions/ByName/{name}
        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<Promotion>> GetPromotionByName(string name)
        {
            if (_context.Promotions == null)
            {
                return NotFound();
            }

            var promotion = await _context.Promotions.FirstOrDefaultAsync(p => p.Name == name);

            if (promotion == null)
            {
                return NotFound();
            }

            return promotion;
        }

        // PUT: api/Promotions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPromotion(int id, Promotion promotion)
        {
            if (id != promotion.PromotionID)
            {
                return BadRequest();
            }

            _context.Entry(promotion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromotionExists(id))
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

        // POST: api/Promotions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Promotion>> PostPromotion(Promotion promotion)
        {
          if (_context.Promotions == null)
          {
              return Problem("Entity set 'SignatureContext.Promotions'  is null.");
          }
            _context.Promotions.Add(promotion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPromotion", new { id = promotion.PromotionID }, promotion);
        }

        // DELETE: api/Promotions/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion(int id)
        {
            if (_context.Promotions == null)
            {
                return NotFound();
            }
            var promotion = await _context.Promotions.FindAsync(id);
            if (promotion == null)
            {
                return NotFound();
            }

            _context.Promotions.Remove(promotion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PromotionExists(int id)
        {
            return (_context.Promotions?.Any(e => e.PromotionID == id)).GetValueOrDefault();
        }
    }
}
