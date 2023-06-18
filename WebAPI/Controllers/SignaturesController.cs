using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAppMVC.Models;

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
        /// <summary>
        /// Get all Signatures.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Returns all signatures correctly</response>
        /// <response code="400">If the signatures list is null</response>
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SignatureDTO>>> GetSignatures()
        {
            if (_context.Signatures == null)
            {
                return NotFound();
            }

            return await _context.Signatures.Select(s => new SignatureDTO
            {
                SignatureID = s.SignatureID,
                CreatedAt = s.CreatedAt,
                IsPresent = s.IsPresent,
                Student = new StudentDTO
                {
                    StudentID = s.Student.StudentID,
                    Firstname = s.Student.Firstname,
                    Lastname = s.Student.Lastname,
                    Group = new GroupDTO
                    {
                        GroupID = s.Student.Group.GroupID,
                        Name = s.Student.Group.Name
                    },
                    Promotion = new PromotionDTO
                    {
                        PromotionID = s.Student.Promotion.PromotionID,
                        Name = s.Student.Promotion.Name
                    }
                }
            }).ToListAsync();
        }

        /// <summary>
        /// Get all Signatures for DataTable.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Returns all signatures correctly with complete DataTable</response>
        /// <response code="400">If the students list is null</response>
        [HttpGet("/api/Datatable/Signatures")]
        public async Task<ActionResult<DataTableResponse>> GetDataTableSignatures()
        {
            if (_context.Signatures == null)
            {
                return NotFound();
            }

            var signatures = await _context.Signatures
                .Select(s => new
                {
                    s.SignatureID,
                    s.CreatedAt,
                    s.IsPresent,
                    Student = new
                    {
                        s.Student.Firstname,
                        s.Student.Lastname,
                        Group = new
                        {
                            s.Student.Group.GroupID,
                            s.Student.Group.Name
                        },
                        Promotion = new
                        {
                            s.Student.Promotion.PromotionID,
                            s.Student.Promotion.Name
                        }
                    }
                }).ToListAsync();

            return new DataTableResponse
            {
                RecordsTotal = signatures.Count(),
                RecordsFiltered = 10,
                Data = signatures.ToArray()
            };
        }

        // GET: api/Signatures/1
        /// <summary>
        /// Get a specific Signature.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/Signatures/1
        ///     {
        ///         "createdAt": "25/05/2023 15:15:15",
        ///         "IsPresent": true,
        ///         "StudentID": 1
        ///     }
        /// </remarks>
        /// <response code="201">Returns the specific signature correctly</response>
        /// <response code="400">If the signature is null</response>
        // <snippet_GetById>
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<SignatureDTO>> GetSignature(int id)
        {
            if (_context.Signatures == null)
            {
                return NotFound();
            }

            var signature = await _context.Signatures
                .Include(s => s.Student)
                    .ThenInclude(s => s.Group)
                .Include(s => s.Student)
                    .ThenInclude(s => s.Promotion)
                .FirstOrDefaultAsync(s => s.SignatureID == id)
            ;

            if (signature == null)
            {
                return NotFound();
            }

            if (signature.Student.Group == null || signature.Student.Promotion == null)
            {
                return BadRequest("Group or Promotion from Student signature not found");
            }

            return new SignatureDTO
            {
                SignatureID = signature.SignatureID,
                CreatedAt = signature.CreatedAt,
                IsPresent = signature.IsPresent,
                Student = new StudentDTO
                {
                    StudentID = signature.Student.StudentID,
                    Firstname = signature.Student.Firstname,
                    Lastname = signature.Student.Lastname,
                    Group = new GroupDTO
                    {
                        GroupID = signature.Student.Group.GroupID,
                        Name = signature.Student.Group.Name
                    },
                    Promotion = new PromotionDTO
                    {
                        PromotionID = signature.Student.Promotion.PromotionID,
                        Name = signature.Student.Promotion.Name
                    }
                }
            };
        }

        // PUT: api/Signatures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update a specific Signature.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <response code="201">Returns the updated signature correctly</response>
        /// <response code="400">If the signature is null</response>
        // <snippet_Update>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSignature(int id, Signature signature)
        {
            if (id != signature.StudentID)
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
        /// <summary>
        /// Create a Signature.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Signatures
        ///     {
        ///         "createdAt": "25/05/2023 15:15:15",
        ///         "IsPresent": true,
        ///         "StudentID": 1
        ///     }
        /// </remarks>
        /// <response code="201">Returns the newly created signature</response>
        /// <response code="400">If the signature is null</response>
        // <snippet_Create>
        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SignatureDTO>> PostSignature(SignatureDTO signatureDTO)
        {
            if (_context.Signatures == null)
            {
                return Problem("Entity set 'SignatureContext.Signatures'  is null.");
            }

            var targetStudent = await _context.Students
                .Include(s => s.Group)
                .Include(s => s.Promotion)
                .FirstOrDefaultAsync(s => s.StudentID == signatureDTO.StudentID)
            ;

            if (targetStudent == null)
            {
                return BadRequest("Invalid Student ID");
            }

            var signature = new Signature
            {
                CreatedAt = signatureDTO.CreatedAt,
                IsPresent = signatureDTO.IsPresent,
                StudentID = signatureDTO.StudentID,
                Student = targetStudent
            };

            _context.Signatures.Add(signature);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetSignature),
                new { id = signature.SignatureID },
                SignatureToDTO(signature)
            );
        }

        // DELETE: api/Signatures/5
        [Authorize]
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
            return (_context.Signatures?.Any(e => e.StudentID == id)).GetValueOrDefault();
        }

        private static SignatureDTO SignatureToDTO(Signature signature) => new()
        {
            SignatureID = signature.SignatureID,
            CreatedAt = signature.CreatedAt,
            IsPresent = signature.IsPresent,
            Student = new StudentDTO
            {
                StudentID = signature.Student.StudentID,
                Firstname = signature.Student.Firstname,
                Lastname = signature.Student.Lastname,
                Group = new GroupDTO
                {
                    GroupID = signature.Student.Group.GroupID,
                    Name = signature.Student.Group.Name
                },
                Promotion = new PromotionDTO
                {
                    PromotionID = signature.Student.Promotion.PromotionID,
                    Name = signature.Student.Promotion.Name
                }
            }
        };
    }
}
