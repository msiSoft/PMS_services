using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sqlBase.Classes
{
    public class POHdr
    {
        public string po_number {get;set;}
        public string cpo_number { get; set; }
        public string vd_code { get; set; }
        public DateTime po_date { get; set; }
        public int challan_number { get; set; }
        public  DateTime receipt_date { get; set; }
        public string remarks { get; set; }
    }
}
