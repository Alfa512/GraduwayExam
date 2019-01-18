using System;
using System.Collections.Generic;
using System.Linq;
using GraduwayExam.Common.Contracts.Repositories;
using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Common.Models.Enums;
using GraduwayExam.Common.Models.ViewModel;
using GraduwayExam.Maps;
using GraduwayExam.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduwayExam.Common.Services
{
    public class TaskService : ITaskService
    {
        private ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TaskVm> GetAll()
        {
            return DomainToViewList(_repository.GetAll().ToList());
        }

        public IEnumerable<TaskVm> OrderTasks(List<TaskVm> tasks, OrderByTaskFilter filter)
        {
            switch (filter)
            {
                case OrderByTaskFilter.ByTitleAsc:
                    return tasks.OrderBy(t => t.Title);
                case OrderByTaskFilter.ByTitleDesc:
                    return tasks.OrderByDescending(t => t.Title);
                case OrderByTaskFilter.ByDateAsk:
                    return tasks.OrderBy(t => t.Date);
                case OrderByTaskFilter.ByDateDesc:
                    return tasks.OrderByDescending(t => t.Date);
                case OrderByTaskFilter.ByEstimateAsc:
                    return tasks.OrderBy(t => t.EstimatedSeconds);
                case OrderByTaskFilter.ByEstimateDesc:
                    return tasks.OrderByDescending(t => t.EstimatedSeconds);
                case OrderByTaskFilter.ByPriorityAsc:
                    return tasks.OrderBy(t => t.Priority);
                case OrderByTaskFilter.ByPriorityDesc:
                    return tasks.OrderByDescending(t => t.Priority);
                case OrderByTaskFilter.ByStateAsk:
                    return tasks.OrderBy(t => t.State);
                case OrderByTaskFilter.ByStateDesc:
                    return tasks.OrderByDescending(t => t.State);

                default:
                {
                    return tasks.OrderBy(t => t.Priority);
                } 
            }
        }

        public TaskVm GetById(string id)
        {
            return DomainToView(_repository.GetAll().FirstOrDefault(u => u.Id == id));
        }

        public IEnumerable<TaskVm> GetByUserId(string userId)
        {
            return DomainToViewList(_repository.GetAll().Where(t => t.UserId == userId).ToList());
        }

        public TaskVm Create(TaskVm taskModel)
        {
            var task = TaskMapper.Mapper().Map<Task>(taskModel);

            taskModel = DomainToView(_repository.Create(task).Entity);

            return taskModel;
        }

        public TaskVm Update(TaskVm taskParam)
        {
            //var task = _context.Tasks.GetAll().FirstOrDefault(u => u.Id == taskParam.Id);
            var task = _repository.GetAll().AsNoTracking().FirstOrDefault(u => u.Id == taskParam.Id);

            if (task == null)
                throw new ApplicationException("Task not found");

            task = TaskMapper.Mapper().Map<Task>(taskParam);

            _repository.Update(task);
            _repository.SaveCganges();

            return DomainToView(task);
        }

        public void Delete(string id)
        {
            var task = _repository.GetAll().FirstOrDefault(u => u.Id == id);
            if (task != null)
            {
                _repository.Delete(task);
                _repository.SaveCganges();
            }
        }

        public IEnumerable<TaskVm> DomainToViewList(List<Task> tasks)
        {
            var vTasks = new List<TaskVm>();

            if (tasks != null && tasks.Count > 0)
            {
                foreach (var task in tasks)
                {
                    vTasks.Add(TaskMapper.Mapper().Map<TaskVm>(task));
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
                    vTasks.Add(TaskMapper.Mapper().Map<Task>(task));
                }
            }

            return vTasks;
        }

        public TaskVm DomainToView(Task task)
        {
            return TaskMapper.Mapper().Map<TaskVm>(task);
        }

        public Task ViewToDomain(TaskVm task)
        {
            return TaskMapper.Mapper().Map<Task>(task);
        }
    }


}