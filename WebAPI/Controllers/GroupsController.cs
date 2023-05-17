using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly SignatureContext _context;

        public GroupsController(SignatureContext context)
        {
            _context = context;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
          if (_context.Groups == null)
          {
              return NotFound();
          }
            return await _context.Groups.ToListAsync();
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(int id)
        {
            if (_context.Groups == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups.FindAsync(id);

            if (@group == null)
            {
                return NotFound();
            }

            return @group;
        }

        // GET: api/Groups/ByName/{name}
        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<Group>> GetGroupByName(string name)
        {
            if (_context.Groups == null)
            {
                return NotFound();
            }

            var group = await _context.Groups.FirstOrDefaultAsync(g => g.Name == name);

            if (group == null)
            {
                return NotFound();
            }

            return group;
        }

        // PUT: api/Groups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(int id, Group @group)
        {
            if (id != @group.GroupID)
            {
                return BadRequest();
            }

            _context.Entry(@group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(Group @group)
        {
          if (_context.Groups == null)
          {
              return Problem("Entity set 'SignatureContext.Groups'  is null.");
          }
            _context.Groups.Add(@group);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { id = @group.GroupID }, @group);
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            if (_context.Groups == null)
            {
                return NotFound();
            }
            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(@group);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupExists(int id)
        {
            return (_context.Groups?.Any(e => e.GroupID == id)).GetValueOrDefault();
        }
    }
}
