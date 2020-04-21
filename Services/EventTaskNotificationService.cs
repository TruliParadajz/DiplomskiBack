using BackendApi.Context;
using BackendApi.Entities;
using BackendApi.Helpers_and_Extensions;
using BackendApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BackendApi.Services
{
    public interface IEventTaskNotificationService
    {
        public Task NotifyEmail(int eventTaskId, int userId);
    }
    public class EventTaskNotificationService : ControllerBase, IEventTaskNotificationService
    {
        private DataContext _context;
        private IUserNotificationService _userNotificationService;
        private IUserService _userService;
        private IEventTasksService _eventTaskService;
        private readonly SmtpSettings _smtpSettings;
        private readonly AppSettings _appSettings;
        private readonly IConfiguration _configuration;

        public EventTaskNotificationService(
            DataContext context,
            IUserNotificationService userNotificationService,
            IUserService userService,
            IEventTasksService eventTaskService,
            IOptions<SmtpSettings> smtpSettings,
            IOptions<AppSettings> appSettings,
            IConfiguration configuration)
        {
            _context = context;
            _userNotificationService = userNotificationService;
            _userService = userService;
            _eventTaskService = eventTaskService;
            _smtpSettings = smtpSettings.Value;
            _appSettings = appSettings.Value;
            _configuration = configuration;
        }

        public Task NotifyEmail(int eventTaskId, int userId)
        {
            var userNotification = _context.UserNotifications
                .Where(uN => uN.UserId == userId)
                .FirstOrDefault();

            var eventTaskNotification = _context.EventTaskNotifications
                .Where(eTN => eTN.EventTaskId == eventTaskId)
                .FirstOrDefault();

            if (eventTaskNotification != null)
            {
                return null;
            }
            var eventTask = _eventTaskService.GetById(eventTaskId);
            string endDateTime = "";
            if (eventTask.EndDt != null)
            {
                endDateTime = eventTask.EndDt.ToString();
            }

            var user = _userService.GetById(userId);


            EmailNotificationModel emailModel = new EmailNotificationModel()
            {
                UserId = userId,
                EventTaskId = eventTaskId,
                EndDateTime = endDateTime,
                StartDateTime = eventTask.StartDt.ToString(),
                Recipient = user.Username,
                Subject = "Task Id " + eventTask.Id.ToString(),
                Text = $"Dear {user.FirstName}" +
                '\n' +
                $"Your Event Task titled {eventTask.Title} " +
                $" will start in {userNotification.Hours.ToString()} hours." +
                '\n' +
                ".o Team",
                Hours = userNotification.Hours
            };
            var result = SendEmailAsync(emailModel);
            if (result.IsCompleted)
            {
                EventTaskNotification newEventTaskNotification = new EventTaskNotification()
                {
                    UserId = userId,
                    EventTaskId = eventTaskId,
                    User = user,
                    EventTask = eventTask,
                    EmailNotification = 1,
                    UserNotification = userNotification,
                    UserNotificationId = userNotification.Id
                };
                _context.EventTaskNotifications.Add(newEventTaskNotification);
                _context.SaveChanges();
            }
            return result;
        }

        public Task SendEmailAsync(EmailNotificationModel emailModel)
        {
            Execute(emailModel.Recipient, emailModel.Subject, emailModel.Text).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(string recipient, string subject, string text)
        {
            //var mykey = _configuration["SmtpSettings"];
            //var server = _smtpSettings.Server;
            //var username = _smtpSettings.Username;
            //var password = _smtpSettings.Password;
            //var port = _smtpSettings.Port;
            //var tls = _smtpSettings.TLS;

            string server = "smtp.gmail.com";
            var username = "veze.bez69@gmail.com";
            var password = "Bezveze123";
            int port = 587;

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(username, ".o"),
                Subject = subject,
                Body = text,
                IsBodyHtml = false,
                Priority = MailPriority.High,

            };

            mail.To.Add(new MailAddress(recipient));
            try
            {
                using (var smtp = new SmtpClient(server, port))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(username, password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
