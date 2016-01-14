using System.Collections.Generic;

namespace OnlineLearningResoursesApp.Models
{
    public interface ILearningRepository
    {
        IEnumerable<LearningPlan> GetAllPlans();
        IEnumerable<Course> GetAllCourses();
        IEnumerable<LearningPlan> GetAllPlansWithCourses();
        void AddLearningPlan(LearningPlan newPlan);
        bool SaveAll();
        LearningPlan GetPlanByName(string planName, string username);
        void AddNewCourseToPlan(string planName, string username, Course newCourse);
        void AddNewCourse(Course newCourse);
        IEnumerable<LearningPlan> GetUserPlansWithCourses(string name);
        void DeleteCourse(int id);
        void RemoveCourse(int id, string planName, string username);
        void DeletePlan(int id);
        void UpdateCourseToActive(int id);
    }
}