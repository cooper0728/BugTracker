using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketAttachment
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }
        public string Description { get; set; }
        public string MediaUrl { get; set; }
    }
}