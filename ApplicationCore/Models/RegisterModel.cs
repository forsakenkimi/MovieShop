using ApplicationCore.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email can not be empty")]
        [EmailAddress(ErrorMessage = "Email address should be in right form")]
        [StringLength(100)]
        public string Email { get; set; }

        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage =
            "Password Should have minimum 8 with at least one upper, lower, number and special character")]
        [Required(ErrorMessage = "Password can not be empty")]
        public string Password { get; set; }
        [Required(ErrorMessage = "First Name can not be empty")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name can not be empty")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [YearValidation(1900,2002)]
        public DateTime DateOfBirth { get; set; }
    }
}
