using System;
using System.ComponentModel.DataAnnotations.Schema;
using GraduwayExam.Common.Models.Enums;

namespace GraduwayExam.Data.Models
{
    [Table("Tasks")]
    public class Task
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskState State { get; set; }
        public TaskPriority Priority { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string CreatorId { get; set; }
        public User Creator { get; set; }
        public DateTime Date { get; set; }
        public int EstimatedSeconds { get; set; }
    }
}