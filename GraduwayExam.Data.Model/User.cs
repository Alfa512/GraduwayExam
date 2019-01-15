using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace GraduwayExam.Data.Models
{
    public class User : IdentityUser//, IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }

    }
}