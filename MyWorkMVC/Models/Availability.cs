using MyWorkMVC.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Models
{
    public class Availability
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }

        public bool IsAvailable { get; set; }
        public AvailableStatus AvailableStatus { get; set; }

        [DataType(DataType.Date)]
        public DateTime NextAvailable { get; set; }
    }
}
