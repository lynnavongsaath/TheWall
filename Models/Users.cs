using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheWall.Models
{
    public class Users
    {
        [Key]
        public int UserId {get;set;}
        [Required(ErrorMessage="First name is required.")]
        [MinLength(2, ErrorMessage="First name must be at least 2 characters long.")]
        public string FirstName {get;set;}
        [Required(ErrorMessage="Last name is required.")]
        [MinLength(2, ErrorMessage="Last name must be at least 2 characters long.")]
        public string LastName {get;set;}
        [Required(ErrorMessage="Email address is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email {get;set;}
        [Required(ErrorMessage="Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage="Your password must be at least 8 characters long.")]
        public string Password {get;set;}
        [NotMapped]
        [Compare("Password")]
        public string ConfirmPassword {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public string FullName {
            get { return $"{FirstName} {LastName}"; }
        }
        //navigation properties 
        public List<Messages> Messages {get;set;}
    }
    public class Login
    {
        [Required(ErrorMessage="Email address is required.")]
        [DataType(DataType.EmailAddress)]
        public string EmailAttempt {get;set;}
        [Required(ErrorMessage="Password is required.")]
        [DataType(DataType.Password)]
        public string PasswordAttempt {get;set;}
    }
}