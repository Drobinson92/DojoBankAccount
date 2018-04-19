using System.ComponentModel.DataAnnotations;
using System;
namespace bankaccounts.Models{
    public class UserViewModel : BaseEntity{
        [Required(ErrorMessage="First Name is required.")]
        [MinLength(2, ErrorMessage="First Name too short.")]
        [Display(Name="First Name")]
        public string FirstName {get; set;}
        [Required(ErrorMessage="Last Name is required.")]
        [MinLength(2, ErrorMessage="Last Name too short.")]
        [Display(Name="Last Name")]
        public string LastName {get; set;}
        [Required(ErrorMessage="Email is required")]
        [EmailAddress(ErrorMessage="Must match valid email format.")]
        public string Email {get; set;}
        [Required(ErrorMessage="Password is required.")]
        [MinLength(8, ErrorMessage="Passwords must have a minimum length of 8 characters.")]
        public string Password {get; set;}
        [Compare("Password", ErrorMessage="Passwords must match.")]
        [Display(Name="Confirm Password")]
        public string PasswordCheck {get; set;}
    }
}