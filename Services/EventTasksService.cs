using BackendApi.Context;
using BackendApi.Entities;
using BackendApi.Helpers_and_Extensions;
using BackendApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Services
{
    public interface IEventTasksService
    {
        public EventTask Create(EventTask inputModel);
        public EventTask Edit(EventTask inputModel);
        public EventTask Delete(int id);
        public IEnumerable<EventTask> FindAll(int userId);
        public IEnumerable<EventTask> FindAll();
        public EventTask GetById(int eventTaskId);
    }
    public class EventTasksService : IEventTasksService
    {
        private DataContext _context;

        public EventTasksService(DataContext context)
        {
            _context = context;
        }

        private string ChangeColour(EventTask inputModel)
        {
            if (inputModel.EndDt == null || inputModel.EndDt.ToString() == "")
            {
                return Colour.Yellow.Value;
            }
            else if (Math.Abs(((DateTime)inputModel.EndDt - DateTime.Now).TotalDays) <= 3)
            {
                return Colour.Red.Value;
            }
            else
            {
                return Colour.Blue.Value;
            }
        }

        public EventTask Create(EventTask inputModel)
        {
            var ifUser = _context.Users.Select(u => u.Id == inputModel.UserId);
            if (inputModel.StartDt > inputModel.EndDt)
            {
                return null;
            }

            EventTask task = new EventTask()
            {
                Draggable = true,
                Resizable = true,
                StartDt = inputModel.StartDt,
                EndDt = inputModel.EndDt,
                Title = inputModel.Title,
                UserId = inputModel.UserId,
                Colour = Colour.Blue.Value,
                User = _context.Users.Where(u => u.Id == inputModel.UserId).FirstOrDefault()
            };

            task.Colour = ChangeColour(task);
            _context.EventTasks.Add(task);
            return task;
        }

        public EventTask Edit(EventTask inputModel)
        {
            if (inputModel.StartDt > inputModel.EndDt)
            {
                return null;
            }

            var isId = _context.EventTasks.Select(e => e.Id == inputModel.Id);

            if (isId == null)
            {
                return null;
            }

            inputModel.Colour = ChangeColour(inputModel);

            _context.Entry(inputModel).State = EntityState.Modified;

            return inputModel;
        }

        public EventTask Delete(int id)
        {
            var eventTask = _context.EventTasks.Find(id);
            if (eventTask == null)
            {
                return null;
            }

            _context.EventTasks.Remove(eventTask);

            return eventTask;
        }

        public IEnumerable<EventTask> FindAll(int userId)
        {
            var outputList = _context.EventTasks.Where(u => u.UserId == userId).ToList();
            foreach (var eventTask in outputList)
            {
                eventTask.Colour = ChangeColour(eventTask);
            }
            return outputList;
        }

        public IEnumerable<EventTask> FindAll()
        {
            var outputList = _context.EventTasks.ToList();
            foreach (var eventTask in outputList)
            {
                eventTask.Colour = ChangeColour(eventTask);
            }
            return outputList;
        }

        public EventTask GetById(int eventTaskId)
        {
            var data = _context.EventTasks.Find(eventTaskId);
            return data;
        }
    }
}
