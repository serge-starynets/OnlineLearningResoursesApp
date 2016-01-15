using System;
using System.Collections;
using System.Collections.Generic;

namespace OnlineLearningResourcesApp.Models
{
    public class LearningPlan
    {
        public int Id { get; set; }

        public string Name{ get; set; }

        public string UserName { get; set; }

        public string Speciality{ get; set; }

        public int? Duration
        {
            get
            {
                return Calculate();
            }
            set
            {
                value = Calculate();
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
