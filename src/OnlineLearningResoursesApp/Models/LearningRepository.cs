using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearningResoursesApp.Models
{
    public class LearningRepository : ILearningRepository
    {
        private LearningContext _context;
        private ILogger<LearningRepository> _logger;

        public LearningRepository(LearningContext context, ILogger<LearningRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddNewCourseToPlan(string planName, string username, Course newCourse)
        {
            var thePlan = GetPlanByName(planName, username);
            thePlan.Courses.Add(newCourse);
            newCourse.Order = thePlan.Courses.Max(c => c.Order) + 1;
            newCourse.PlanName = planName;
            _context.Courses.Add(newCourse);
        }

        public void AddCourseToPlan(string planName, string username, Course course)
        {
            var thePlan = GetPlanByName(planName, username);
            thePlan.Courses.Add(course);
            course.Order = thePlan.Courses.Max(c => c.Order) + 1;
            _context.Courses.Add(course);
        }

        public void UpdateCourseToActive(int id)
        {
            Course course = GetCourseById(id);
            course.IsActive = true;
            _context.SaveChanges();
        }

        public IEnumerable<Course> GetAllCourses()
        {
            try
            {
                return _context.Courses.OrderBy(c => c.PlanName).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get courses form database", ex);
                return null;
            }
        }

        public void DeleteCourse(int id)
        {
            Course course = GetCourseById(id);
            _context.Courses.Remove(course);
        }

        public void DeletePlan(int id)
        {
            LearningPlan plan = GetPlanById(id);
            _context.LearningPlans.Remove(plan);
        }

        public void RemoveCourse(int id, string planName, string username)
        {
            Course course = GetCourseFromPlanById(id, planName, username);

            course.PlanName = null;
            course.LearningPlanID = null;

            _context.SaveChanges();

        }

        public void AddNewCourse(Course newCourse)
        {
            _context.Courses.Add(newCourse);
        }

        public void AddLearningPlan(LearningPlan newPlan)
        {
            _context.Add(newPlan);
        }

        public IEnumerable<LearningPlan> GetAllPlans()
        {
            try {
                return _context.LearningPlans.OrderBy(lp => lp.Name).ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError("Could not get learning plans form database", ex);
                return null;
            }
        }

        public IEnumerable<LearningPlan> GetAllPlansWithCourses()
        {
            try {
                return _context.LearningPlans
                    .Include(lp => lp.Courses)
                    .OrderBy(lp => lp.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get learning plans with courses form database", ex);
                return null;
            }
        }

        public LearningPlan GetPlanByName(string planName, string username)
        {
            return _context.LearningPlans.Include(lp => lp.Courses)
                .Where(lp => lp.Name == planName && lp.UserName == username)
                .FirstOrDefault();
        }

        public LearningPlan GetPlanById(int id)
        {
            return _context.LearningPlans.SingleOrDefault(lp => lp.Id == id);
        }

        public Course GetCourseById(int id)
        {
            return _context.Courses.SingleOrDefault(c => c.Id == id);
        }

        public Course GetCourseFromPlanById(int id, string planName, string username)
        {
            LearningPlan plan = GetPlanByName(planName, username);
            
            Course course = plan.Courses.SingleOrDefault(c => c.Id == id);
            return course;
        }

        public IEnumerable<LearningPlan> GetUserPlansWithCourses(string name)
        {
            try
            {
                return _context.LearningPlans
                    .Include(lp => lp.Courses)
                    .OrderBy(lp => lp.Name)
                    .Where(lp => lp.UserName == name)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get learning plans with courses form database", ex);
                return null;
            }
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }


    }
}
