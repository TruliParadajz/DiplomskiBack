using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendApi.Context;
using BackendApi.Entities;
using BackendApi.Services;
using BackendApi.Helpers_and_Extensions;
using System.Net;
using BackendApi.Models;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserNotificationsController : ControllerBase
    {
        private readonly DataContext _context;
        IUserNotificationService _userNotificationService;

        public UserNotificationsController(
            DataContext context,
            IUserNotificationService userNotificationService)
        {
            _context = context;
            _userNotificationService = userNotificationService;
        }

        // GET: api/UserNotifications
        /// <summary>
        /// Returns all the User Notifications.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserNotification>>> GetUserNotifications()
        {
            return await _context.UserNotifications.ToListAsync();
        }

        // GET: api/UserNotifications/5
        /// <summary>
        /// Returns the User Notification bounded by UserId. Only one per user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("userId/{userId}")]
        public ActionResult<UserNotification> GetUserNotification(int userId)
        {
            var userNotification = _userNotificationService.GetByUserId(userId);

            if (userNotification == null)
            {
                return NotFound();
            }

            return userNotification;
        }

        // PUT: api/UserNotifications/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Updates a User Notification.
        /// </summary>
        /// <param name="notificationInput"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<UserNotification>> PutUserNotification(UserNotificationUpdateModel notificationInput)
        {
            var result = await _userNotificationService.Update(notificationInput);
            return result;
        }

        // POST: api/UserNotifications
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Create a new User Notification. Used only when testing.
        /// </summary>
        /// <param name="userNotification"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserNotification>> PostUserNotification(UserNotification userNotification)
        {
            _context.UserNotifications.Add(userNotification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserNotification", new { id = userNotification.Id }, userNotification);
        }

        // DELETE: api/UserNotifications/5
        /// <summary>
        /// Deletes a User Notification.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserNotification>> DeleteUserNotification(int id)
        {
            var userNotification = await _context.UserNotifications.FindAsync(id);
            if (userNotification == null)
            {
                return NotFound();
            }

            _context.UserNotifications.Remove(userNotification);
            await _context.SaveChangesAsync();

            return userNotification;
        }

        private bool UserNotificationExists(int id)
        {
            return _context.UserNotifications.Any(e => e.Id == id);
        }
    }
}
