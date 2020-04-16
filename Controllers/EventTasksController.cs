using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendApi.Context;
using BackendApi.Entities;
using BackendApi.Models;
using BackendApi.Services;
using BackendApi.Helpers_and_Extensions;
using System.Net;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTasksController : ControllerBase
    {
        private readonly DataContext _context;
        private IEventTasksService _eventTasksService;

        public EventTasksController(DataContext context, IEventTasksService eventTaskService)
        {
            _context = context;
            _eventTasksService = eventTaskService;
        }
        /// <summary>
        /// Method for getting all the EventTasks regardless of userId
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<EventTask>> GetEventTasks()
        {
            var eventTasks = _eventTasksService.FindAll();
            return Ok(eventTasks);
        }
        // GET: api/EventTasks
        /// <summary>
        /// Method for getting all the EventTasks of a specified user
        /// </summary>
        /// <returns>List of EventTasks</returns>
        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<EventTask>> GetEventTasks(int userId)
        {
            var eventTasks = _eventTasksService.FindAll(userId);
            return Ok(eventTasks);
        }
        // GET: api/EventTasks/5
        /// <summary>
        /// Method for getting an EventTask by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns EventTask</returns>
        //[HttpGet("{id}")]
        //public async Task<ActionResult<EventTask>> GetEventTask(int id)
        //{
        //    var eventTask = await _context.EventTask.FindAsync(id);
 
        //    if (eventTask == null)
        //    {
        //        throw new ApiException("Task not found", HttpStatusCode.NotFound);
        //    }

        //    return eventTask;
        //}

        // PUT: api/EventTasks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Method for editing an EventTask
        /// </summary>
        /// <param name="eventTask"></param>
        /// <returns>Returns edited EventTask</returns>
        [HttpPut]
        public async Task<ActionResult<EventTask>> PutEventTask(EventTask eventTask)
        {
            var editedEventTask =_eventTasksService.Edit(eventTask);            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventTaskExists(eventTask.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(editedEventTask);
        }

        // POST: api/EventTasks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Method for creating an EventTask
        /// </summary>
        /// <param name="eventTaskInput"></param>
        /// <returns>Returns newly created EventTask</returns>
        [HttpPost]
        public async Task<ActionResult<EventTask>> PostEventTask(EventTask eventTaskInput)
        {
            var eventTask = _eventTasksService.Create(eventTaskInput);
            if(eventTask == null)
            {
                throw new ApiException("Starting date must be less than ending date", HttpStatusCode.BadRequest);
            }
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetEventTask", new { id = eventTask.Id }, eventTask);
            return Ok(eventTask);
        }

        // DELETE: api/EventTasks/5
        /// <summary>
        /// Method for deleting an EventTask
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns deleted EventTask</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<EventTask>> DeleteEventTask(int id)
        {
            var eventTask = _eventTasksService.Delete(id);

            if(eventTask == null)
            {
                throw new ApiException("Task not found", HttpStatusCode.NotFound);
            }
            await _context.SaveChangesAsync();

            return eventTask;
        }

        /// <summary>
        /// Method for checking if an EventTask exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns searched EventTask</returns>
        private bool EventTaskExists(int id)
        {
            return _context.EventTask.Any(e => e.Id == id);
        }
    }
}
