using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearningResoursesApp.Models
{
    public class LearningContextSeedData
    {
        private LearningContext _context;
        private UserManager<User> _userManager;

        public LearningContextSeedData(LearningContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Seed db
        public async Task EnsureSeedDataAsync()
        {
            if (await _userManager.FindByEmailAsync("jon.doe@onlinelearningresoursesapp.com") == null)
            {
                // Add the user
                var newUser = new User()
                {
                    UserName = "jondoe",
                    Email = "jon.doe@onlinelearningresoursesapp.com"
                };

              await _userManager.CreateAsync(newUser, "P@ssword1");
            }

            if (!_context.LearningPlans.Any())
            {
                // Add new data
                var cSharpLearningPlan = new LearningPlan()
                {
                    Name = "CSharp Learning Plan",
                    Speciality = "Programming Languages",
                    Courses = new List<Course>()
                    {
                        new Course() {Name = "CSharp Fundamentals with CSharp 5.0", Url = "https://app.pluralsight.com/library/courses/csharp-fundamentals-csharp5/table-of-contents",
                        Duration = 14, Order = 1, PlanName = "CSharp Learning Plan", IsActive = false},
                        new Course() {Name = "CSharp Generics", Url = "https://app.pluralsight.com/library/courses/csharp-generics/table-of-contents",
                        Duration = 14, Order = 3, PlanName = "CSharp Learning Plan", IsActive = false},
                        new Course() {Name = "Object-Oriented Programming Fundamentals in CSharp", Url = "https://app.pluralsight.com/library/courses/object-oriented-programming-fundamentals-csharp/table-of-contents",
                        Duration = 14, Order = 2, PlanName = "CSharp Learning Plan", IsActive = false},
                        new Course() {Name = "CSharp From Scratch", Url = "https://app.pluralsight.com/library/courses/csharp-from-scratch/table-of-contents",
                        Duration = 14, Order = 0, PlanName = "CSharp Learning Plan", IsActive = false},
                        new Course() {Name = "CSharp Events, Delegates and Lambdas", Url = "https://app.pluralsight.com/library/courses/csharp-events-delegates/table-of-contents",
                        Duration = 14, Order = 4, PlanName = "CSharp Learning Plan", IsActive = false}
                    }
                };

                _context.LearningPlans.Add(cSharpLearningPlan);
                _context.Courses.AddRange(cSharpLearningPlan.Courses);

                var pythonLearningPlan = new LearningPlan()
                {
                    Name = "Python Fundamentals Learning Plan",
                    Speciality = "Programming Languages/Web Development",
                    Courses = new List<Course>()
                    {
                        new Course() {Name = "Programming Foundations with Python", Url = "https://www.udacity.com/course/programming-foundations-with-python--ud036",
                        Duration = 28, PlanName = "Python Fundamentals Learning Plan", IsActive = false},
                        new Course() {Name = "Intro to Computer Science", Url = "https://www.udacity.com/course/intro-to-computer-science--cs101",
                        Duration = 42, PlanName = "Python Fundamentals Learning Plan", IsActive = false},
                        new Course() {Name = "Data stuctures in Python", Url = "https://www.coursera.org/learn/python-data",
                        Duration = 14, PlanName = "Python Fundamentals Learning Plan", IsActive = false},
                        new Course() {Name = "Using Databases with Python", Url = "https://www.coursera.org/learn/python-databases",
                        Duration = 14, PlanName = "Python Fundamentals Learning Plan", IsActive = false},
                        new Course() {Name = "Using Python to Access Web Data", Url = "https://www.coursera.org/learn/python-network-data",
                        Duration = 14, PlanName = "Python Fundamentals Learning Plan", IsActive = false}
                    }
                };

                _context.LearningPlans.Add(pythonLearningPlan);
                _context.Courses.AddRange(pythonLearningPlan.Courses);

                var javaLearningPlan = new LearningPlan()
                {
                    Name = "Java Fundamentals Learning Plan",
                    Speciality = "Programming Languages",
                    Courses = new List<Course>()
                    {
                        new Course() {Name = "Java Fundamentals: The Java Language", Url = "https://app.pluralsight.com/library/courses/java-fundamentals-language/table-of-contents",
                        Duration = 28, PlanName = "Java Fundamentals Learning Plan", IsActive = false},
                        new Course() {Name = "Java Fundamentals: Generics", Url = "https://app.pluralsight.com/library/courses/java-generics/table-of-contents",
                        Duration = 42, PlanName = "Java Fundamentals Learning Plan", IsActive = false},
                        new Course() {Name = "Java Fundamentals, Part 2", Url = "https://app.pluralsight.com/library/courses/java2/table-of-contents",
                        Duration = 14, PlanName = "Java Fundamentals Learning Plan", IsActive = false},
                        new Course() {Name = "Java Web Fundamentals", Url = "https://app.pluralsight.com/library/courses/java-web-fundamentals/table-of-contents",
                        Duration = 14, PlanName = "Java Fundamentals Learning Plan", IsActive = false},
                        new Course() {Name = "Introduction to Testing in Java", Url = "https://app.pluralsight.com/library/courses/java-testing-introduction/table-of-contents",
                        Duration = 14, PlanName = "Java Fundamentals Learning Plan", IsActive = false}
                    }
                };

                _context.LearningPlans.Add(javaLearningPlan);
                _context.Courses.AddRange(javaLearningPlan.Courses);

                _context.SaveChanges();
            }
        }
    }
}
