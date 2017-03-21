using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZenithWebSite.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
