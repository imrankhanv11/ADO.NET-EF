using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstApproach.Models
{
    public class EmployeeProject
    {
        [Key,Column(Order = 0)]
        [ForeignKey("Employees")]
        public int EmpID { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Projects")]
        public int ProjectID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Roll { get; set; }

        public virtual Employees Employees { get; set; }
        public virtual Projects Projects { get; set; }
    }
}
