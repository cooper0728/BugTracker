using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Project()
        {
            this.Users = new HashSet<ApplicationUser>();
        }
        
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}