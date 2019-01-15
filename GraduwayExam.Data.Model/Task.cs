namespace GraduwayExam.Data.Models
{
    public class Task
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string CreatorId { get; set; }
        public User Creator { get; set; }

    }
}