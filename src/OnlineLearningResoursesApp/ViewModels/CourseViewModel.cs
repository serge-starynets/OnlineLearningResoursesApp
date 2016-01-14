using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearningResoursesApp.Controllers.Api
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Name { get; set; }

        public DateTime DateOfStart { get; set; }

        public string Url { get; set; }

        public string PlanName { get; set; }

        public int Duration { get; set; }

        public bool IsActive { get; set; }
    }
}