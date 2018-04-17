using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABCBankingApplication.Models.ViewModels
{
    public class PasswordViewModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}