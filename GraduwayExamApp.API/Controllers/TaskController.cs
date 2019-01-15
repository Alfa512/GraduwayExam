using System.Collections.Generic;
using System.Linq;
using GraduwayExam.Common.Contracts.Services;
using GraduwayExam.Common.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
        public List<TaskVm> List()
        {
            var tasks = _taskService.GetAll().ToList();
            tasks.Add(new TaskVm()
            {
                Id = "1",
                Name = "Cool Task 1",
                Description = "Very-very bit text for task description. I can't realise how it is big."
            });
            tasks.Add(new TaskVm()
            {
                Id = "2",
                Name = "Cool Task 2",
                Description = "Second very bit text for task description. I can't realise how it is big too."
            });
            return tasks;
        }

        [Route("create")]
        public string Create(TaskVm task)
        {
            var str = "name: jack, lastname: smith";
            return str;
        }
    }
}