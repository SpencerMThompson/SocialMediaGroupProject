using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaApplication.Models
{
    /* yo */
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage ="Must Enter First Name")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Must Enter Last Name")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Must Enter A Username")]
        [DisplayName("Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Must Enter A Password")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Must Re-enter Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Reenter Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Must Enter An Email")]
        public string Email { get; set; }
        public ICollection<Post> Posts { get; set; }
        public bool VerifyUser(User user)
        {
            bool isValid = true;

            return isValid;
        }
        public bool VerifyPassword(string pass1, string pass2)
        {
            bool isValid = false;
            if(pass1 == pass2)
            {
                isValid = true;
            }
            return isValid;
        }
    }
}
