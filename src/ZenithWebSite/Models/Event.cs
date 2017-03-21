using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZenithWebSite.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public DateTime EventFromDateTime { get; set; }
        public DateTime EventToDateTime { get; set; }
        public string EnteredByUsername { get; set; }


        public int ActivityId { get; set; }

        public Activity Activity { get; set; }


        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
