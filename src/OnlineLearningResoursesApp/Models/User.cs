using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlineLearningResourcesApp.Models
{
    public class User : IdentityUser
    {
        public DateTime FirstPlan {get; set;}

    }
}
