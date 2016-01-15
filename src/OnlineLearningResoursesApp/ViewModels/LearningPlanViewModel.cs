using OnlineLearningResourcesApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearningResourcesApp.ViewModels
{
    public class LearningPlanViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Name { get; set; }

        public string UserName { get; set; }

        public string Speciality { get; set; }

        public int? Duration {
            get
            {
                return Calculate();
            }
        }

        public ICollection<Course> Courses { get; set; }


        public int Calculate()
        {
            if (Courses != null)
            {
                int total = 0;
                foreach (Course course in Courses)
                {
                    total += course.Duration;
                }
                return total;
            }
            else
            {
                return 0;
            }
        }
    }
}
