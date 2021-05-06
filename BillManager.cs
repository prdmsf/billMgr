using System;
using System.Collections.Generic; 

namespace billMgr
{
    public class BillManager
    {
        public string Bill { get; set; }
        public DateTime BillDate { get; set; }
        public decimal TotalBill { get; set; }
    }

    public class BillDetail
    {
        public string friend { get; set; }
        public string bill { get; set; }
        public DateTime billdate { get; set;  }
        public decimal billPortion { get; set; }
    }

//    public class friends
//    {
//        public string friend { get; set; }
     //   public decimal billPortion { get; set; }
//    }
    public class newBills
    {
        public string Bill { get; set; }
        public DateTime BillDate { get; set; }
        public decimal TotalBill { get; set; }
        public string []  friends { get; set; }
    }

    public class Friends
    {
        string friend { get; set; }
        public decimal billPortion { get; set; }
    }
}
