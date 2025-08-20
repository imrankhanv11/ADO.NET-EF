using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstApproach.Models
{
    public class Projects
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string ProjectName { get; set; }

        [Required]
        public int Budget {  get; set; }

        public virtual ICollection<EmployeeProject> Employees { get; set; }

        public Projects()
        {
            Employees = new HashSet<EmployeeProject>();
        }
    }
}
