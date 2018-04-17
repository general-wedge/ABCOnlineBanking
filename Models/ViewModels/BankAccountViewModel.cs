using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABCBankingApplication.Models.ViewModels
{
    public class BankAccountViewModel
    {
        public List<ChequeingAccount> ChequeingAccts { get; set; }
        public List<SavingsAccount> SavingsAccts { get; set; }
        public string CustomerName { get; set; }
    }
}