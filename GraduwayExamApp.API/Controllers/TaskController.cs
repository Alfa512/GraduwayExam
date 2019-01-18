using System;
using System.Collections.Generic;
using System.Linq;
using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Common.Models.Enums;
using GraduwayExam.Common.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace GraduwayExamApp.API.Controllers
{
    [Route("api/task")]
    [EnableCors("AllowSpecificOrigin")]
    public class TaskController : Controller
    {
        private ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [AllowAnonymous]
        [Route("list")]
        public List<TaskVm> List(int? orderBy)
        {
            var sort = orderBy != null ? (OrderByTaskFilter)orderBy : OrderByTaskFilter.ByPriorityAsc;
            var tasks = _taskService.OrderTasks(_taskService.GetAll().ToList(), sort);
            return tasks.ToList();
        }

        [AllowAnonymous]
        [Route("getbyid")]
        public TaskVm GetById(string id)
        {
            var mTask = _taskService.GetById(id);
            return mTask;
        }

        [AllowAnonymous]
        [Route("byuserid")]
        public List<TaskVm> GetByUserId(string userId)
        {
            var tasks = _taskService.GetByUserId(userId).ToList();
            return tasks;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("create")]
        public TaskVm Create([FromBody]TaskVm task)
        {
            var mTask = _taskService.Create(task);
            return mTask;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("update")]
        public TaskVm Update([FromBody]TaskVm task)
        {
            var mTask = _taskService.Update(task);
            return mTask;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("delete")]
        public bool Delete([FromBody]string taskId)
        {
            try
            {
                _taskService.Delete(taskId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}