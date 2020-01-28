using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcPuc2.Model
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50,ErrorMessage ="Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [Required]
        [RegularExpression("@^([a - zA - Z0 - 9_\-\.] +)@([a - zA - Z0 - 9_\-\.] +)\.([a - zA - Z]{2, 5})$",
            ErrorMessage ="Email is not valid")]
        [Display(Name ="Office Email")]
        public string Email { get; set; }
        public Dept Department { get; set; }
    }
}
