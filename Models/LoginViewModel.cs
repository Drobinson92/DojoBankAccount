using System.ComponentModel.DataAnnotations;
using System;
namespace bankaccounts.Models{
    public class LoginViewModel : BaseEntity{
        [Required]
        [EmailAddress]
        public string Email {get; set;}
        [Required]
        public string Password {get; set;}
    }
}