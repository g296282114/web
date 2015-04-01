using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace torsion.Models
{
    public class UserRegister:UserProfile
    {
        [Required]
        public int Age { get; set; }
    }
}