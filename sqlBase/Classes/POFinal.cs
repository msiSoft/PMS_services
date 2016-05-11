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
        public decimal ordered_qty { get; set; }
        public decimal received_qty { get; set; }
        public decimal accepted_qty { get; set; }
        public string data_entered_date { get; set; }
        public string data_entered_by { get; set; }
        public bool is_updated_on_server { get; set; }


        public void SavePurchaseDtl(string vslcode, string grvno_auto, POFinal pdtl, string vendorcode)
        {
            string qry = @"INSERT INTO PURCHASE.GRV_DT 
                                                        ( 
                                                        GRV_NO,
                                                        IM_CODE,
                                                        QTY_RECD,
                                                        QTY_ACPT,
                                                        UPDFLAG,
                                                        VSLCODE,
                                                        CODE_TYPE,	
                                                        PO_NO,
                                                        VD_CODE
                                                        )
                                                         VALUES   ('" + grvno_auto + "','" + pdtl.item_code + "', " + pdtl.received_qty + "," + pdtl.accepted_qty + ",'Y'," + vslcode + ",'" + pdtl.code_type + "' ,'" + pdtl.po_number + "' ,'" + vendorcode + "')";
            DBOperations DB = new DBOperations();
            int result = DB.OperationsOnSourceDB(qry);
        }

    }

}
