using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAppMVC.Models;

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
        //[Authorize]
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        public async Task<ActionResult<IEnumerable<object>>> GetStudents()
        {
            if (_context.Students == null)
            {
                return NotFound();
            }

            // TODO: Pense à utiliser le DTO pour les étudiants

            //return await _context.Students.ToListAsync();
            //return await _context.Students.Select(x => StudentToDTO(x)).ToListAsync();

            return await _context.Students
                .Select(s => new
                {
                    s.StudentID,
                    s.Firstname,
                    s.Lastname,
                    Group = new
                    {
                        s.GroupID,
                        s.Group.Name
                    },
                    Promotion = new
                    {
                        s.PromotionID,
                        s.Promotion.Name
                    }
                }).ToListAsync();
        }

        /// <summary>
        /// Get all Students for DataTable.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Returns all students correctly with complete DataTable</response>
        /// <response code="400">If the students list is null</response>
        [HttpGet("/api/datatable/Students")]
        public async Task<ActionResult<DataTableResponse>> GetDataTableStudents()
        {
            if (_context.Students == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Select(s => new
                {
                    s.StudentID,
                    s.Firstname,
                    s.Lastname,
                    Group = new
                    {
                        s.GroupID,
                        s.Group.Name
                    },
                    Promotion = new
                    {
                        s.PromotionID,
                        s.Promotion.Name
                    }
                }).ToListAsync();

            return new DataTableResponse
            {
                RecordsTotal = students.Count(),
                RecordsFiltered = 10,
                Data = students.ToArray()
            };
        }

        /// <summary>
        /// Get One Student with Signature DataTable.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Returns one student correctly with complete Signature DataTable</response>
        /// <response code="400">If the student is null</response>
        [HttpGet("/api/datatable/Student/{id}")]
        public async Task<ActionResult<DataTableResponse>> GetDataTableStudentSignatures(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Group)
                .Include(s => s.Promotion)
                .Include(s => s.Signatures)
                .FirstOrDefaultAsync(s => s.StudentID == id)
            ;

            if (student == null)
            {
                return NotFound();
            }
            
            return new DataTableResponse
            {
                RecordsTotal = student.Signatures.Count,
                RecordsFiltered = 10,
                Data = student.Signatures.Select(sign => SignatureToDTO(sign)).ToArray()
            };
            
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
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Group)
                .Include(s => s.Promotion)
                .FirstOrDefaultAsync(s => s.StudentID == id)
            ;
            
            if (student == null)
            {
                return NotFound();
            }

            return new
            {
                student.StudentID,
                student.Firstname,
                student.Lastname,
                Group = new
                {
                    student.Group.GroupID,
                    student.Group.Name
                },
                Promotion = new
                {
                    student.Promotion.PromotionID,
                    student.Promotion.Name
                }
            };
        }
        // </snippet_GetByID>

        // PUT: api/Students/5
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
        ///         "promotion": {
        ///             "id": 1,
        ///             "name": "Promotion"
        ///         }
        ///     }
        /// </remarks>
        /// <response code="201">Returns the updated student correctly</response>
        /// <response code="400">If the student is null</response>
        // <snippet_Update>
        //[Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentDTO>> PutStudent(int id, StudentDTO studentDTO)
        {
            if (id != studentDTO.StudentID)
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

            // UPDATE GROUP
            var newStudentGroup = await _context.Groups.FindAsync(studentDTO.Group.GroupID);

            if (newStudentGroup == null)
            {
                return BadRequest("Invalid Group ID");
            }

            student.GroupID = studentDTO.Group.GroupID;
            student.Group = newStudentGroup;

            // UPDATE PROMOTION
            var newStudentPromotion = await _context.Promotions.FindAsync(studentDTO.PromotionID);

            if (newStudentPromotion == null)
            {
                return BadRequest("Invalid Promotion ID");
            }

            student.PromotionID = studentDTO.Promotion.PromotionID;
            student.Promotion = newStudentPromotion;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!StudentExists(id))
            {
                return NotFound();
            }

            return StudentToDTO(student);
        }
        // </snippet_Update>


        // POST: api/Students
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
        ///         },
        ///         "promotion": {
        ///             "id": 1,
        ///             "name": "Promotion"
        ///         }
        ///     }
        /// </remarks>
        /// <response code="201">Returns the newly created student</response>
        /// <response code="400">If the student is null</response>
        // <snippet_Create>
        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentDTO>> PostStudent(StudentDTO studentDTO)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'SignatureContext.Students'  is null.");
            }

            // UPDATE GROUP
            var studentGroup = await _context.Groups.FindAsync(studentDTO.GroupID);

            if (studentGroup == null)
            {
                return BadRequest("Invalid Group ID");
            }

            // UPDATE PROMOTION
            var studentPromotion = await _context.Promotions.FindAsync(studentDTO.PromotionID);

            if (studentPromotion == null)
            {
                return BadRequest("Invalid Promotion ID");
            }

            var student = new Student
            {
                Firstname = studentDTO.Firstname,
                Lastname = studentDTO.Lastname,
                GroupID = studentDTO.GroupID,
                Group = studentGroup,
                PromotionID = studentDTO.PromotionID,
                Promotion = studentPromotion
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetStudent),
                new { id = student.StudentID },
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
        [Authorize]
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
            return (_context.Students?.Any(s => s.StudentID == id)).GetValueOrDefault();
        }

        private static StudentDTO StudentToDTO(Student student) => new()
        {
            StudentID = student.StudentID,
            Firstname = student.Firstname,
            Lastname = student.Lastname,
            GroupID = student.GroupID,
            Group = new GroupDTO
            {
                GroupID = student.Group.GroupID,
                Name = student.Group.Name
            },
            PromotionID = student.PromotionID,
            Promotion = new PromotionDTO
            {
                PromotionID = student.Promotion.PromotionID,
                Name = student.Promotion.Name
            }
        };

        private static SignatureDTO SignatureToDTO(Signature signature) => new()
        {
            SignatureID = signature.SignatureID,
            CreatedAt = signature.CreatedAt,
            IsPresent = signature.IsPresent
        };
    }
}
