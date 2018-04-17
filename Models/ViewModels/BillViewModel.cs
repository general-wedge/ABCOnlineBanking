using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABCBankingApplication.Models.ViewModels
{
    public class BillViewModel
    {
        public double BalanceOwing { get; set; }
        public double Payment { get; set; }
        public int BillType { get; set; }
    }
}