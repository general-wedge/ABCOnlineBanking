using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ABCBankingApplication.Models.ViewModels
{
    public class UserViewModel
    {
        [StringLength(100, ErrorMessage = "Max length 100 characters")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [StringLength(100, ErrorMessage = "Max length 100 characters")]
        public string FirstName { get; set; }

        [StringLength(100, ErrorMessage = "Max length 100 characters")]
        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public string Address { get; set; }

        public Boolean isAdmin { get; set; }
    }
}