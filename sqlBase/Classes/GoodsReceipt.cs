using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sqlBase.Classes
{
    public class GoodsReceipt
    {
        public string vslcode { get; set; }
        public Dictionary<PurchaseHdrObj , List<ItemsObj>> GR { get; set; }
    }

    public class PurchaseHdrObj
    {
        public string po_number { get; set; }
        public string vd_code { get; set; }
        public string challan_number { get; set; }
        public string receipt_date { get; set; }        //date
        public string remarks { get; set; }       
        public string data_entered_by { get; set; }
        public string data_entered_at { get; set; }    //date 

    }

    public class ItemsObj
    {
        public string po_number { get; set; }
        public string item_code  { get; set; }
        public string code_type { get; set; }
        public decimal received_qty { get; set; }
        public decimal accepted_qty { get; set; }
    }
}
