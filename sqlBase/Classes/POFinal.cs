using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sqlBase.Classes
{
    public class POFinal
    {
        public string po_number { get; set; }
        public string item_code { get; set; }
        public string code_type { get; set; }
        public decimal requested_qty { get; set; }
    }
}
