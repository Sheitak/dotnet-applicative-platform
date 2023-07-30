using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Models.DTO;

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
        /// <summary>
        /// Get all Promotions.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Returns all promotions correctly</response>
        /// <response code="400">If the promotions list is null</response>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Promotion>>> GetPromotions()
        {
          if (_context.Promotions == null)
          {
              return NotFound();
          }
            return await _context.Promotions.ToListAsync();
        }

        // GET: api/Promotions//5
        /// <summary>
        /// Get a specific Promotion.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/Promotions/1
        ///     {
        ///         "id": 1,
        ///         "name": "Promotion"
        ///     }
        /// </remarks>
        /// <response code="201">Returns the specific promotion correctly</response>
        /// <response code="400">If the promotion is null</response>
        // <snippet_GetById>
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
        /// <summary>
        /// Get a specific Promotion.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/Promotions/Example
        ///     {
        ///         "id": 1,
        ///         "name": "Example"
        ///     }
        /// </remarks>
        /// <response code="201">Returns the specific promotion correctly</response>
        /// <response code="400">If the promotion is null</response>
        // <snippet_GetByName>
        [Authorize]
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
        /// <summary>
        /// Update a specific Promotion.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/Promotions/1
        ///     {
        ///         "id": 1,
        ///         "name": "Promotion"
        ///     }
        /// </remarks>
        /// <response code="201">Returns the updated promotion correctly</response>
        /// <response code="400">If the promotion is null</response>
        // <snippet_Update>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<PromotionDTO>> PutPromotion(int id, PromotionDTO promotionDTO)
        {
            if (id != promotionDTO.PromotionID)
            {
                return BadRequest();
            }

            //_context.Entry(promotion).State = EntityState.Modified;

            var promotion = await _context.Promotions.FindAsync(id);
            if (promotion == null)
            {
                return NotFound();
            }

            promotion.Name = promotionDTO.Name;

           try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PromotionExists(id))
            {
                return NotFound();
            }

            return PromotionToDTO(promotion);
        }

        // POST: api/Promotions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create a Promotion.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Promotions
        ///     {
        ///         "name": "Promotion"
        ///     }
        /// </remarks>
        /// <response code="201">Returns the newly created promotion</response>
        /// <response code="400">If the promotion is null</response>
        // <snippet_Create>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PromotionDTO>> PostPromotion(PromotionDTO promotionDTO)
        {
          if (_context.Promotions == null)
          {
              return Problem("Entity set 'SignatureContext.Promotions'  is null.");
          }

          var promotion = new Promotion { Name = promotionDTO.Name };

          _context.Promotions.Add(promotion);
          await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPromotion), 
                new { id = promotion.PromotionID }, 
                PromotionToDTO(promotion)
            );
        }

        // DELETE: api/Promotions/5
        /// <summary>
        /// Delete a Promotion.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="201">Promotion correctly deleted</response>
        /// <response code="400">If the promotion is null</response>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        private static PromotionDTO PromotionToDTO(Promotion promotion) => new()
        {
            PromotionID = promotion.PromotionID,
            Name = promotion.Name
        };
    }
}
