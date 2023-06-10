using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Models.DTO;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Devices EndPoints.
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly SignatureContext _context;

        /// <summary>
        /// Context Initialisation.
        /// </summary>
        public DevicesController(SignatureContext context)
        {
            _context = context;
        }

        // GET: api/Devices
        /// <summary>
        /// Get all Devices.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Returns all devices correctly</response>
        /// <response code="400">If the devices list is null</response>
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceDTO>>> GetDevices()
        {
            if (_context.Devices == null)
            {
                return NotFound();
            }

            return await _context.Devices.Select(d => new DeviceDTO
            {
                DeviceID = d.DeviceID,
                MacAddress = d.MacAddress,
                IsActive = d.IsActive,
                Student = new StudentDTO
                {
                    StudentID = d.Student.StudentID,
                    Firstname = d.Student.Firstname,
                    Lastname = d.Student.Lastname
                }
            }).ToListAsync();
        }

        // GET: api/Devices/GetById/5
        /// <summary>
        /// Get a specific Device.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/Devices/GetById/1
        ///     {
        ///         "id": 1,
        ///         "macAddress": "123456789",
        ///         "isActive": "True",
        ///         "student": {
        ///             "id": 1,
        ///             "firstname": "John",
        ///             "lastname": "Doe"
        ///         }
        ///     }
        /// </remarks>
        /// <response code="201">Returns the specific device correctly</response>
        /// <response code="400">If the device is null</response>
        // <snippet_GetById>
        //[Authorize]
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<DeviceDTO>> GetDevice(int id)
        {
            if (_context.Devices == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.Student)
                .FirstOrDefaultAsync(d => d.DeviceID == id)
            ;

            if (device == null)
            {
                return NotFound();
            }

            if (device.Student == null)
            {
                return BadRequest("Student from Device not found");
            }

            return new DeviceDTO
            {
                DeviceID = device.DeviceID,
                MacAddress = device.MacAddress,
                IsActive = device.IsActive,
                Student = new StudentDTO
                {
                    StudentID = device.Student.StudentID,
                    Firstname = device.Student.Firstname, 
                    Lastname = device.Student.Lastname
                }
            };
        }
        // </snippet_GetById>

        // POST: api/Devices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create a Device.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Devices
        ///     {
        ///         "MacAddress": "123456789",
        ///         "IsActive": "false",
        ///         "StudentID": 1
        ///     }
        /// </remarks>
        /// <response code="201">Returns the newly created device</response>
        /// <response code="400">If the device is null</response>
        // <snippet_Create>
        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DeviceDTO>> PostDevice(DeviceDTO deviceDTO)
        {
            if (_context.Devices == null)
            {
                return Problem("Entity set 'SignatureContext.Devices'  is null.");
            }

            var studentDevice = await _context.Students.FindAsync(deviceDTO.StudentID);

            if (studentDevice == null)
            {
                return BadRequest("Invalid Student ID");
            }

            var device = new Device
            {
                MacAddress = deviceDTO.MacAddress,
                IsActive = deviceDTO.IsActive,
                StudentID = studentDevice.StudentID,
                Student = studentDevice
            };

            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetDevice),
                new { id = device.DeviceID },
                DeviceToDTO(device)
            );
        }
        // </snippet_Create>

        private static DeviceDTO DeviceToDTO(Device device) => new()
        {
            DeviceID = device.DeviceID,
            MacAddress = device.MacAddress,
            IsActive = device.IsActive,
            Student = new StudentDTO
            {
                StudentID = device.Student.StudentID,
                Firstname = device.Student.Firstname,
                Lastname = device.Student.Lastname
            }
        };
    }
}
