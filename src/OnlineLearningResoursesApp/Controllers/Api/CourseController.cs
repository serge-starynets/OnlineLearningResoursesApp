using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineLearningResourcesApp.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using AutoMapper;
using Microsoft.AspNet.Authorization;

namespace OnlineLearningResourcesApp.Controllers.Api
{
    [Authorize]    
    public class CourseController : Controller
    {
        private ILogger<CourseController> _logger;
        private ILearningRepository _repository;

        public CourseController(ILearningRepository repository, ILogger<CourseController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // Get courses of specific plan
        [HttpGet]
        [Route("api/plans/{planName}/courses")]
        public JsonResult Get(string planName)
        {
            try {
                var results = _repository.GetPlanByName(planName, User.Identity.Name);

                if(results == null)
                {
                    return Json(null);
                }

                return Json(Mapper.Map<IEnumerable<CourseViewModel>>(results.Courses.OrderBy(c => c.Name)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get courses for plan {planName}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Error occured finding plan name");
            }
        }

        // Gets all courses
        [HttpGet]
        [Route("api/courses")]
        public JsonResult Get()
        {
            try
            {
                var results = _repository.GetAllCourses();

                if (results == null)
                {
                    return Json(null);
                }

                return Json(Mapper.Map<IEnumerable<CourseViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get courses", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Error occured finding courses");
            }
        }

        // Add new course to the specific plan
        [HttpPost]
        [Route("api/plans/{planName}/courses")]
        public JsonResult Post(string planName, [FromBody]Course vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Map to the Entity
                    var newCourse = Mapper.Map<Course>(vm);

                    // Save to the db
                    _repository.AddNewCourseToPlan(planName, User.Identity.Name, newCourse);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<CourseViewModel>(newCourse));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new course", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Failed to save new course");
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Validation failed on new course");
        }

        // Create new course 
        [HttpPost]
        [Route("api/courses")]
        public JsonResult Post([FromBody]CourseViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Map to the Entity
                    var newCourse = Mapper.Map<Course>(vm);

                    // Save to the db
                    _repository.AddNewCourse(newCourse);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<CourseViewModel>(newCourse));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new course", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Failed to save new course");
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Validation failed on new course");
        }

        // Make course active
        [HttpPut]
        [Route("api/courses/{id}")]
        public JsonResult MakeCourseActive(int id)
        {
            try
            {
                _repository.UpdateCourseToActive(id);

                Response.StatusCode = (int)HttpStatusCode.Accepted;
                return Json("course started");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to start course", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Failed to start course");
            }

        }


        // Delete course
        [HttpDelete]
        [Route("api/courses/{id}")]
        public JsonResult Delete(int id)
        {
            try {
                _repository.DeleteCourse(id);
                if (_repository.SaveAll())
                {
                    Response.StatusCode = (int)HttpStatusCode.NoContent;
                    return Json("course deleted");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to delete course", ex);
                return Json("Failed to delete new course");
            }
            return Json("Failed to delete course. Cannot find course");
        }
    }
}
