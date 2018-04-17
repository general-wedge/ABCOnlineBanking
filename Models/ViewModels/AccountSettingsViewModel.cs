using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABCBankingApplication.Models.ViewModels
{
    public class AccountSettingsViewModel
    {
        public PasswordViewModel pVM { get; set; }
        public AddressViewModel aVM { get; set; }
    }
}