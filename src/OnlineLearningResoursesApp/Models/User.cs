using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace OnlineLearningResourcesApp.Models
{
    public class User : IdentityUser
    {
        public DateTime FirstPlan{ get; set; }

    }
}