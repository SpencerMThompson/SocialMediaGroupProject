using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaApplication.Models
{
    public class ViewModel
    {
        public IEnumerable<User> Users { get; set; }
        //public IQueryable<User> Users { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public User CurrentUser { get; set; }

        public string getUsername(int? userID)
        {
            string name = "no name";
            foreach (User user in Users)
            {
                if (user.UserId == userID)
                {
                    name = user.UserName;
                    return name;
                    break;
                }
            }
            return name;
        }
        public void setCurrentUser(User user)
        {
            user = CurrentUser;
        }
    }
}
