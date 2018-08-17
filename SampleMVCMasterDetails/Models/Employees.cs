using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SampleMVCMasterDetails.Models
{
    public class Employees
    {
     
        public long EmpId { get; set; }

        [Required(ErrorMessage ="Please Enter Name")]
        [MinLength(6)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Select Designation")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [RegularExpression(".+\\@.+\\..+",
 ErrorMessage = "Please enter a valid email address")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [MaxLength(10)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        [MinLength(10)]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Enter User Name")]
        [MinLength(6)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [MembershipPassword(
    MinRequiredNonAlphanumericCharacters = 1,
    MinNonAlphanumericCharactersError = "Your password needs to contain at least one symbol (!, @, #, etc).",
    ErrorMessage = "Your password must be 8 characters long and contain at least one symbol (!, @, #, etc).",
    MinRequiredPasswordLength = 8,PasswordStrengthError ="Weak Password",PasswordStrengthRegularExpression = "^.*(?=.{8,})(?=.*\\d).*$"

)]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [MembershipPassword(
    MinRequiredNonAlphanumericCharacters = 1,
    MinNonAlphanumericCharactersError = "Your password needs to contain at least one symbol (!, @, #, etc).",
    ErrorMessage = "Your password must be 8 characters long and contain at least one symbol (!, @, #, etc).",
    MinRequiredPasswordLength = 8, PasswordStrengthError = "Weak Password", PasswordStrengthRegularExpression = "^.*(?=.{8,})(?=.*\\d).*$"

)]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password and Confirm Password Must be same")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please Enter Age")]
        [Range(18,35)]
        public int Age { get; set; }

        

        public string ErrorCode { get; set; }

    }
}