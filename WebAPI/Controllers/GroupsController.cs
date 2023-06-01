using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Models.DTO;

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
        /// <summary>
        /// Get all Groups.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Returns all groups correctly</response>
        /// <response code="400">If the groups list is null</response>
        //[Authorize]
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
        /// <summary>
        /// Get a specific Group.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/Groups/1
        ///     {
        ///         "id": 1,
        ///         "name": "Group"
        ///     }
        /// </remarks>
        /// <response code="201">Returns the specific group correctly</response>
        /// <response code="400">If the group is null</response>
        // <snippet_GetById>
        //[Authorize]
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
        /// <summary>
        /// Get a specific Group.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/Groups/Example
        ///     {
        ///         "id": 1,
        ///         "name": "Example"
        ///     }
        /// </remarks>
        /// <response code="201">Returns the specific group correctly</response>
        /// <response code="400">If the group is null</response>
        // <snippet_GetByName>
        //[Authorize]
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
        /// <summary>
        /// Update a specific Group.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/Groups/1
        ///     {
        ///         "id": 1,
        ///         "name": "Group"
        ///     }
        /// </remarks>
        /// <response code="201">Returns the updated group correctly</response>
        /// <response code="400">If the group is null</response>
        // <snippet_Update>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GroupDTO>> PutGroup(int id, GroupDTO groupDTO)
        {
            if (id != groupDTO.GroupID)
            {
                return BadRequest();
            }

            //_context.Entry(@group).State = EntityState.Modified;

            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            group.Name = groupDTO.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!GroupExists(id))
            {
                return NotFound();
            }

            return GroupToDTO(group);
        }

        // POST: api/Groups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create a Group.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Groups
        ///     {
        ///         "name": "Group"
        ///     }
        /// </remarks>
        /// <response code="201">Returns the newly created group</response>
        /// <response code="400">If the group is null</response>
        // <snippet_Create>
        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GroupDTO>> PostGroup(GroupDTO groupDTO)
        {
          if (_context.Groups == null)
          {
              return Problem("Entity set 'SignatureContext.Groups'  is null.");
          }

          var group = new Group { Name = groupDTO.Name };

          _context.Groups.Add(group);
          await _context.SaveChangesAsync();

          return CreatedAtAction(
              nameof(GetGroup), 
              new { id = group.GroupID }, 
              GroupToDTO(group)
          );
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

        private static GroupDTO GroupToDTO(Group group) => new()
        {
            GroupID = group.GroupID,
            Name = group.Name
        };
    }
}
