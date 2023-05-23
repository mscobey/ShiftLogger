using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftLogger.Data;

namespace ShiftLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftLoggerController : ControllerBase
    {

        private readonly DataContext _context;

        public ShiftLoggerController(DataContext context)
        {
            _context = context;
        }



        // GET: api/<ShiftLoggerController>
        [HttpGet]
        public async Task<ActionResult<List<Shift>>> Get()
        {
            return Ok(await _context.Shifts.ToListAsync());
        }

        // GET api/<ShiftLoggerController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Shift>>> Get(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
                return BadRequest("Shift not found.");

            return Ok(shift);
        }

        // POST api/<ShiftLoggerController>
        [HttpPost]
        public async Task<ActionResult<List<Shift>>> Post(Shift shift)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();

            return Ok(await _context.Shifts.ToListAsync());
        }

        // PUT api/<ShiftLoggerController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Shift>>> Put(Shift updatedShift)
        {
            var shift = await _context.Shifts.FindAsync(updatedShift.shiftId);
            if (shift == null)
                return BadRequest("Shift not found.");

            shift.shiftId = updatedShift.shiftId;
            shift.employeeId = updatedShift.employeeId;
            shift.Start = updatedShift.Start;
            shift.End = updatedShift.End;
            shift.Duration = updatedShift.Duration;

            await _context.SaveChangesAsync();

            return Ok(await _context.Shifts.ToListAsync());

        }

        // DELETE api/<ShiftLoggerController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Shift>>> Delete(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
                return BadRequest("Shift not found.");
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();

            return Ok(await _context.Shifts.ToListAsync());


        }
    }
}
