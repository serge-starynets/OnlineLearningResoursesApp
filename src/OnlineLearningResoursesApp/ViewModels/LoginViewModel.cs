using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearningResourcesApp.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password{ get; set; }
    }
}
