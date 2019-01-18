using System;

namespace GraduwayExam.Common.Models.ViewModel
{
    public class TaskVm
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
        public int Priority { get; set; }
        public string UserId { get; set; }
        public string CreatorId { get; set; }
        public DateTime Date { get; set; }
        public int EstimatedSeconds { get; set; }

    }
}