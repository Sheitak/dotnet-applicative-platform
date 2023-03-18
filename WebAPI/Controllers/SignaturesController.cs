using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignaturesController : ControllerBase
    {
        private readonly SignatureContext _context;

        public SignaturesController(SignatureContext context)
        {
            _context = context;
        }

        // GET: api/Signatures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Signature>>> GetSignatures()
        {
          if (_context.Signatures == null)
          {
              return NotFound();
          }
            return await _context.Signatures.ToListAsync();
        }

        // GET: api/Signatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Signature>> GetSignature(int id)
        {
          if (_context.Signatures == null)
          {
              return NotFound();
          }
            var signature = await _context.Signatures.FindAsync(id);

            if (signature == null)
            {
                return NotFound();
            }

            return signature;
        }

        // PUT: api/Signatures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSignature(int id, Signature signature)
        {
            if (id != signature.Id)
            {
                return BadRequest();
            }

            _context.Entry(signature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SignatureExists(id))
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

        // POST: api/Signatures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Signature>> PostSignature(Signature signature)
        {
          if (_context.Signatures == null)
          {
              return Problem("Entity set 'SignatureContext.Signatures'  is null.");
          }
            _context.Signatures.Add(signature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSignature", new { id = signature.Id }, signature);
        }

        // DELETE: api/Signatures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSignature(int id)
        {
            if (_context.Signatures == null)
            {
                return NotFound();
            }
            var signature = await _context.Signatures.FindAsync(id);
            if (signature == null)
            {
                return NotFound();
            }

            _context.Signatures.Remove(signature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SignatureExists(int id)
        {
            return (_context.Signatures?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
