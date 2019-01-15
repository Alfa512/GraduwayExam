using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GraduwayExam.Common.Contracts.Data;
using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Common.Models.ViewModel;
using Task = GraduwayExam.Data.Models.Task;

namespace GraduwayExam.Common.Services
{
    public class TaskService : ITaskService
    {
        private IDataContext _context;
        //private ITaskRepository _repository;

        public TaskService(IDataContext context/*, ITaskRepository repository*/)
        {
            _context = context;
            //_repository = repository;
            //_taskManager = taskManager;
            //_taskManager = new TaskManager<Task>();
        }

        public IEnumerable<TaskVm> GetAll()
        {
            return DomainToViewList(_context.Tasks.GetAll().ToList());
        }

        public TaskVm GetById(string id)
        {
            return DomainToViewList(_context.Tasks.GetAll().FirstOrDefault(u => u.Id == id));
        }

        public async Task<TaskVm> CreateAsync(TaskVm taskModel, string password)
        {
            var task = Mapper.Map<Task>(taskModel);
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new ApplicationException("Password is required");

            //var result = await _taskManager.CreateAsync(task, taskModel.Password);
            //if (result.Succeeded)
            {
                //await _taskManager.SignInAsync(task, isPersistent: false, rememberBrowser: false);

                return taskModel;
            }

            return null;
        }

        public async void UpdateAsync(TaskVm taskParam, string password = null)
        {
            var taskModel = Mapper.Map<Task>(taskParam);

            var task = _context.Tasks.GetAll().FirstOrDefault(u => u.Id == taskModel.Id);

            if (task == null)
                throw new ApplicationException("Task not found");

            //await _taskManager.UpdateAsync(task);
            _context.Tasks.Update(task);
            _context.Commit();
        }

        public void Delete(string id)
        {
            var task = _context.Tasks.GetAll().FirstOrDefault(u => u.Id == id);
            if (task != null)
            {
                _context.Tasks.Delete(task);
                _context.Commit();
            }
        }

        public IEnumerable<TaskVm> DomainToViewList(List<Task> tasks)
        {
            var vTasks = new List<TaskVm>();

            if (tasks != null && tasks.Count > 0)
            {
                foreach (var task in tasks)
                {
                    vTasks.Add(Mapper.Map<TaskVm>(task));
                }
            }

            return vTasks;
        }
        public List<Task> ViewToDomainList(List<TaskVm> tasks)
        {
            var vTasks = new List<Task>();

            if (tasks != null && tasks.Count > 0)
            {
                foreach (var task in tasks)
                {
                    vTasks.Add(Mapper.Map<Task>(task));
                }
            }

            return vTasks;
        }

        public TaskVm DomainToViewList(Task task)
        {
            return Mapper.Map<TaskVm>(task);
        }

        public Task ViewToDomain(TaskVm task)
        {
            return Mapper.Map<Task>(task);
        }
    }


}