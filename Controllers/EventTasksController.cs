using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendApi.Context;
using BackendApi.Entities;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTasksController : ControllerBase
    {
        private readonly DataContext _context;

        public EventTasksController(DataContext context)
        {
            _context = context;
        }

        // GET: api/EventTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventTask>>> GetEventTask()
        {
            return await _context.EventTask.ToListAsync();
        }

        // GET: api/EventTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventTask>> GetEventTask(int id)
        {
            var eventTask = await _context.EventTask.FindAsync(id);

            if (eventTask == null)
            {
                return NotFound();
            }

            return eventTask;
        }

        // PUT: api/EventTasks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventTask(int id, EventTask eventTask)
        {
            if (id != eventTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventTaskExists(id))
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

        // POST: api/EventTasks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<EventTask>> PostEventTask(EventTask eventTask)
        {
            _context.EventTask.Add(eventTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventTask", new { id = eventTask.Id }, eventTask);
        }

        // DELETE: api/EventTasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EventTask>> DeleteEventTask(int id)
        {
            var eventTask = await _context.EventTask.FindAsync(id);
            if (eventTask == null)
            {
                return NotFound();
            }

            _context.EventTask.Remove(eventTask);
            await _context.SaveChangesAsync();

            return eventTask;
        }

        private bool EventTaskExists(int id)
        {
            return _context.EventTask.Any(e => e.Id == id);
        }
    }
}
