using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Models.DTO;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Students EndPoints.
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SignatureContext _context;

        /// <summary>
        /// Context Initialisation.
        /// </summary>
        public StudentsController(SignatureContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all Students.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Returns all students correctly</response>
        /// <response code="400">If the students list is null</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            //return await _context.Students.ToListAsync();
            return await _context.Students.Select(x => StudentToDTO(x)).ToListAsync();
        }

        // GET: api/Student/5
        /// <summary>
        /// Get a specific Student.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="201">Returns the specific student correctly</response>
        /// <response code="400">If the student is null</response>
        // <snippet_GetByID>
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
            //return StudentToDTO(student);
        }
        // </snippet_GetByID>

        // PUT: api/Student/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update a specific Student.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/Students/1
        ///     {
        ///         "id": 1,
        ///         "firstname": "Student",
        ///         "lastname": "UPDATED",
        ///         "group": {
        ///             "id": 1,
        ///             "name": "Group"
        ///         }
        ///     }
        /// </remarks>
        /// <response code="201">Returns the updated student correctly</response>
        /// <response code="400">If the student is null</response>
        // <snippet_Update>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutStudent(int id, StudentDTO studentDTO)
        {
            if (id != studentDTO.Id)
            {
                return BadRequest();
            }

            //_context.Entry(student).State = EntityState.Modified;

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            student.Firstname = studentDTO.Firstname;
            student.Lastname = studentDTO.Lastname;
            student.Group = studentDTO.Group;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!StudentExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        // </snippet_Update>


        // POST: api/Student
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create a Student.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Students
        ///     {
        ///         "firstname": "Student",
        ///         "lastname": "Student",
        ///         "group": {
        ///             "id": 1,
        ///             "name": "Group"
        ///         }
        ///     }
        /// </remarks>
        /// <response code="201">Returns the newly created student</response>
        /// <response code="400">If the student is null</response>
        // <snippet_Create>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentDTO>> PostStudent(StudentDTO studentDTO)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'SignatureContext.Students'  is null.");
            }

            var student = new Student
            {
                Firstname = studentDTO.Firstname,
                Lastname = studentDTO.Lastname,
                Group = studentDTO.Group
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetStudent),
                new { id = student.Id },
                StudentToDTO(student)
            );
        }
        // </snippet_Create>

        // DELETE: api/Student/5
        /// <summary>
        /// Delete a Student.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="201">Student correctly deleted</response>
        /// <response code="400">If the student is null</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(s => s.Id == id)).GetValueOrDefault();
        }

        private static StudentDTO StudentToDTO(Student student) => new()
        {
            Id = student.Id,
            Firstname = student.Firstname,
            Lastname = student.Lastname,
            Group = student.Group
        };
    }
}
