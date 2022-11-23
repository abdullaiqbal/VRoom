using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace vroom.Models
{
    public class Make
    {
        [ValidateNever]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
       
        //public ICollection<Model> Models { get; set; }
    }
}