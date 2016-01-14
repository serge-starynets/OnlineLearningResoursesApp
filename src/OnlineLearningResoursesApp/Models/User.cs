using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace OnlineLearningResoursesApp.Models
{
    public class User : IdentityUser
    {
        public DateTime FirstPlan{ get; set; }

    }
}