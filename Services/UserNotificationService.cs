using BackendApi.Context;
using BackendApi.Entities;
using BackendApi.Helpers_and_Extensions;
using BackendApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Services
{
    public interface IUserNotificationService
    {
        UserNotification Create(int userId);
        UserNotification Update(UserNotificationUpdateModel notificationInput);
        UserNotification GetById(int userId);
    }
    public class UserNotificationService : IUserNotificationService
    {
        private DataContext _context;

        public UserNotificationService(DataContext context)
        {
            _context = context;
        }
        public UserNotification Create(int userId)
        {
            UserNotification newNotification = new UserNotification()
            {
                UserId = userId,
                AppNotification = true,
                EmailNotification = true,
                Hours = 24
            };
            _context.UserNotifications.Add(newNotification);
            _context.SaveChanges();
            return newNotification;
        }

        public UserNotification GetById(int userId)
        {
            var result = _context.UserNotifications.Where(uN => uN.UserId == userId).FirstOrDefault();
            return result;
        }

        public UserNotification Update(UserNotificationUpdateModel notificationInput)
        {
            var userNotification = _context.UserNotifications.
                Find(notificationInput.Id);

            if (userNotification == null)
            {
                throw new AppException("User Notification data not found");
            }
            else
            {
                userNotification.EmailNotification = notificationInput.EmailNotification;
                userNotification.AppNotification = notificationInput.AppNotification;

                if(notificationInput.EmailNotification || notificationInput.AppNotification)
                {
                    userNotification.Hours = notificationInput.Hours;
                }

                _context.Update(userNotification);
                _context.SaveChanges();

                return userNotification;
            }
        }
    }
}
