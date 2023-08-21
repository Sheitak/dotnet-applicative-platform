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
        [Authorize]
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
                RegisteredAt = d.RegisteredAt,
                Student = new StudentDTO
                {
                    StudentID = d.Student.StudentID,
                    Firstname = d.Student.Firstname,
                    Lastname = d.Student.Lastname
                }
            }).ToListAsync();
        }

        // GET: api/Datatable/Devices
        /// <summary>
        /// Get all Devices for DataTable.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Returns all devices correctly with complete DataTable</response>
        /// <response code="400">If the devices list is null</response>
        //[Authorize]
        [HttpGet("/api/Datatable/Devices")]
        public async Task<ActionResult<DataTableResponse>> GetDataTableDevices()
        {
            if (_context.Devices == null)
            {
                return NotFound();
            }

            var devices = await _context.Devices
                .Select(d => new DeviceDTO
                {
                    DeviceID = d.DeviceID,
                    MacAddress = d.MacAddress,
                    IsActive = d.IsActive,
                    RegisteredAt = d.RegisteredAt,
                    Student = new StudentDTO
                    {
                        StudentID = d.Student.StudentID,
                        Firstname = d.Student.Firstname,
                        Lastname = d.Student.Lastname
                    }
                }).ToListAsync();

            return new DataTableResponse
            {
                RecordsTotal = devices.Count(),
                RecordsFiltered = 10,
                Data = devices.ToArray()
            };
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
        [Authorize]
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
                RegisteredAt = device.RegisteredAt,
                Student = new StudentDTO
                {
                    StudentID = device.Student.StudentID,
                    Firstname = device.Student.Firstname, 
                    Lastname = device.Student.Lastname
                }
            };
        }
        // </snippet_GetById>

        // GET: api/Devices/GetDevicesByStudent
        /// <summary>
        /// Get all Devices by Student.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="201">Returns all devices by student correctly</response>
        /// <response code="400">If the devices list is null</response>
        [Authorize]
        [HttpGet("GetDevicesByStudent/{id}")]
        public async Task<ActionResult<IEnumerable<DeviceDTO>>> GetDevicesByStudent(int id)
        {
            // TODO: Vérifier l'utilisation, puis supprimer.
            if (_context.Devices == null)
            {
                return NotFound();
            }

            var devices = await _context.Devices
            .Where(d => d.Student.StudentID == id)
            .Select(d => new DeviceDTO
            {
                DeviceID = d.DeviceID,
                MacAddress = d.MacAddress,
                IsActive = d.IsActive,
                RegisteredAt = d.RegisteredAt,
                Student = new StudentDTO
                {
                    StudentID = d.Student.StudentID,
                    Firstname = d.Student.Firstname,
                    Lastname = d.Student.Lastname
                }
            })
            .ToListAsync();

            if (devices == null || devices.Count == 0)
            {
                return NotFound();
            }

            return devices;
        }

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
                RegisteredAt = deviceDTO.RegisteredAt,
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

        // PUT: api/Devices/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update a specific Device.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deviceDTO"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/Devices/1
        ///     {
        ///         "isActive": false,
        ///     }
        /// </remarks>
        /// <response code="201">Returns the updated device correctly</response>
        /// <response code="400">If the device is null</response>
        // <snippet_Update>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DeviceDTO>> PutDevice(int id, DeviceDTO deviceDTO)
        {
            if (id != deviceDTO.DeviceID)
            {
                return BadRequest();
            }

            var device = await _context.Devices
                .Include(d => d.Student)
                .Include(d => d.Student.Devices)
                .FirstOrDefaultAsync(d => d.DeviceID == id)
            ;

            if (device == null)
            {
                return NotFound();
            }

            var student = device.Student;

            if (student.Devices != null)
            {
                foreach (Device deviceStudent in student.Devices)
                {
                    deviceStudent.IsActive = false;
                }
            }

            device.IsActive = deviceDTO.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!DeviceExists(id))
            {
                return NotFound();
            }

            return DeviceToDTO(device);
        }
        // </snippet_Update>

        private bool DeviceExists(int id)
        {
            return (_context.Devices?.Any(d => d.DeviceID == id)).GetValueOrDefault();
        }

        private static DeviceDTO DeviceToDTO(Device device) => new()
        {
            DeviceID = device.DeviceID,
            MacAddress = device.MacAddress,
            IsActive = device.IsActive,
            RegisteredAt = device.RegisteredAt,
            Student = new StudentDTO
            {
                StudentID = device.Student.StudentID,
                Firstname = device.Student.Firstname,
                Lastname = device.Student.Lastname
            }
        };
    }
}
