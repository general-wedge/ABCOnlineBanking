//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ABCBankingApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bill
    {
        public int bill_id { get; set; }
        public int bill_type { get; set; }
        public long balance_owing { get; set; }
        public int account_id { get; set; }
    
        public virtual BankAccount BankAccount { get; set; }
        public virtual BillType BillType { get; set; }
    }
}
