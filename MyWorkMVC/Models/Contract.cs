using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public int SpecializedProfileId { get; set; }

        public virtual Portfolio PortfolioItem { get; set; }
    }
}
