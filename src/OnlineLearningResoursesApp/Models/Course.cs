using System;

namespace OnlineLearningResourcesApp.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PlanName { get; set; }

        public int? LearningPlanID { get; set; }

        public DateTime DateOfStart { get; set; }

        public bool IsActive { get; set; }

        public string Url { get; set; }

        public int Duration{ get; set; }

        public int Order { get; set; }
    }
}