using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
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

        // GET: api/Students
        /// <summary>
        /// Get all Students.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Returns all students correctly</response>
        /// <response code="400">If the students list is null</response>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            if (_context.Students == null)
            {
                return NotFound();
            }

            return await _context.Students.Select(s => new StudentDTO
            {
                StudentID = s.StudentID,
                Firstname = s.Firstname,
                Lastname = s.Lastname,
                Group = new GroupDTO
                {
                    GroupID = s.Group.GroupID,
                    Name = s.Group.Name
                },
                Promotion = new PromotionDTO
                {
                    PromotionID = s.Promotion.PromotionID,
                    Name = s.Promotion.Name
                }
            }).ToListAsync();
        }

        // GET: api/Datatable/Students
        /// <summary>
        /// Get all Students for DataTable.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Returns all students correctly with complete DataTable</response>
        /// <response code="400">If the students list is null</response>
        //[Authorize]
        [HttpGet("/api/Datatable/Students")]
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

        // GET: api/Datatable/Students/1
        /// <summary>
        /// Get One Student with Signature DataTable.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Returns one student correctly with complete Signature DataTable</response>
        /// <response code="400">If the student is null</response>
        //[Authorize]
        [HttpGet("/api/Datatable/Student/{id}")]
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

        // GET: api/Students/GetById/5
        /// <summary>
        /// Get a specific Student.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/Students/GetById/1
        ///     {
        ///         "id": 1,
        ///         "firstname": "John",
        ///         "lastname": "Doe",
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
        /// <response code="201">Returns the specific student correctly</response>
        /// <response code="400">If the student is null</response>
        // <snippet_GetById>
        [Authorize]
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudent(int id)
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

            if (student.Group == null || student.Promotion == null)
            {
                return BadRequest("Group or Promotion from Student not found");
            }

            return new StudentDTO
            {
                StudentID = student.StudentID,
                Firstname = student.Firstname,
                Lastname = student.Lastname,
                Group = new GroupDTO
                {
                    GroupID = student.Group.GroupID,
                    Name = student.Group.Name
                },
                Promotion = new PromotionDTO
                {
                    PromotionID = student.Promotion.PromotionID,
                    Name = student.Promotion.Name
                }
            };
        }
        // </snippet_GetById>

        // GET: api/Students/GetByIdWithMacAddress/8/82A70095380B
        /// <summary>
        /// Get a specific Student device informations.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="macAddress"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/Students/GetByIdWithMacAddress/1/82A70095380B
        ///     {
        ///         "id": 1,
        ///         "firstname": "John",
        ///         "lastname": "Doe",
        ///         "isActive": true,
        ///         "macAdress": "82A70095380B"
        ///     }
        /// </remarks>
        /// <response code="201">Returns the specific device student informations correctly</response>
        /// <response code="400">If the student is null</response>
        // <snippet_GetByIdWithMacAddress>
        // TODO: MAUI AUTH AZURE
        //[Authorize]
        [HttpGet("GetByIdWithMacAddress/{id}/{macAddress}")]
        public async Task<ActionResult<StudentDTO>> GetStudentByIdWithMacAddress(int id, string macAddress)
        {
            var student = await _context.Students
                .Include(s => s.Devices)
                .FirstOrDefaultAsync(s => s.StudentID == id)
            ;

            if (student == null)
            {
                return NotFound();
            }

            var device = student.Devices.FirstOrDefault(d => d.MacAddress == macAddress);

            if (device == null)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.Forbidden,
                    Title = "Appareil introuvable",
                    Detail = "Cet appareil n'existe pas sur votre compte. Vous n'êtes pas autorisé à émarger. " +
                    "Souhaitez-vous envoyer une demande d'enregistrement à la vie étudiante ?"
                };

                return BadRequest(problemDetails);
            }

            if (!device.IsActive)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.Locked,
                    Title = "Appareil inactif",
                    Detail = "Cet appareil n'est pas encore activé pour votre compte. " +
                    "Souhaitez-vous envoyer un email à la vie étudiante pour relancer l'activation ?"
                };

                return BadRequest(problemDetails);
            }

            var studentDTO = new StudentDTO
            {
                StudentID = student.StudentID,
                Firstname = student.Firstname,
                Lastname = student.Lastname,
                Device = new DeviceDTO
                {
                    DeviceID = device.DeviceID,
                    IsActive = device.IsActive,
                    MacAddress = device.MacAddress
                }
            };

            return studentDTO;
        }
        // </snippet_GetByIdWithMacAddress>

        // PUT: api/Students/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update a specific Student.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentDTO"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/Students/1
        ///     {
        ///         "id": 1,
        ///         "firstname": "John",
        ///         "lastname": "UPDATE",
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
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentDTO>> PutStudent(int id, StudentDTO studentDTO)
        {
            if (id != studentDTO.StudentID)
            {
                return BadRequest();
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

            student.Firstname = studentDTO.Firstname;
            student.Lastname = studentDTO.Lastname;

            if (studentDTO.Group == null || studentDTO.Promotion == null)
            {
                return BadRequest("Group or Promotion from Student not found");
            }

            // UPDATE GROUP
            var targetStudentGroup = await _context.Groups.FindAsync(studentDTO.Group.GroupID);

            if (targetStudentGroup == null)
            {
                return BadRequest("Invalid Group ID");
            }

            student.GroupID = studentDTO.Group.GroupID;
            student.Group = targetStudentGroup;

            // UPDATE PROMOTION
            var targetStudentPromotion = await _context.Promotions.FindAsync(studentDTO.Promotion.PromotionID);

            if (targetStudentPromotion == null)
            {
                return BadRequest("Invalid Promotion ID");
            }

            student.PromotionID = studentDTO.Promotion.PromotionID;
            student.Promotion = targetStudentPromotion;

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
        ///         "firstname": "Jane",
        ///         "lastname": "Doe",
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
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentDTO>> PostStudent(StudentDTO studentDTO)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'SignatureContext.Students'  is null.");
            }

            var studentGroup = await _context.Groups.FindAsync(studentDTO.GroupID);

            if (studentGroup == null)
            {
                return BadRequest("Invalid Group ID");
            }

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

        // DELETE: api/Students/5
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
