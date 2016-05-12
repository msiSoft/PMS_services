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


        public void SavePurchaseDtl(string vslcode, string grvno_auto, POFinal goodsReceivedtl, string vendorcode)
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
                                                         VALUES   ('" + grvno_auto + "','" + goodsReceivedtl.item_code + "', " + goodsReceivedtl.received_qty + "," + goodsReceivedtl.accepted_qty + ",'Y'," + vslcode + ",'" + goodsReceivedtl.code_type + "' ,'" + goodsReceivedtl.po_number + "' ,'" + vendorcode + "')";
            DBOperations DB = new DBOperations();
            int result = DB.OperationsOnSourceDB(qry);
        }

        public void UpdStock(POFinal goodsReceivedtl, string vslcode)
        {
            string dt = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
            string qry = @"UPDATE PURCHASE.STOCK SET 
                                                      ROB_QTY=ROB_QTY +  " + goodsReceivedtl.accepted_qty +
                                                    ", LAST_RECD_QTY = " + goodsReceivedtl.accepted_qty +
                                                    ", LAST_RECD_DT= " + dt +
                                                    ", TOTAL_IN= TOTAL_IN + " + goodsReceivedtl.accepted_qty +
                                                    ", DE_BY= '" + goodsReceivedtl.data_entered_by +
                                                    "', DE_AT= " + goodsReceivedtl.data_entered_date +
                                                    ", UPDFLAG =" + "'D' " +
                                                    " WHERE VSLCODE = " + vslcode +
                                                   " AND IM_CODE ='" + goodsReceivedtl.item_code +
                                                   "' AND CODE_TYPE= '" + goodsReceivedtl.code_type + "'" +
                                                   " AND UPDFLAG = " + "'D'";


            DBOperations DB = new DBOperations();
            int result = DB.OperationsOnSourceDB(qry);

        }
    }

}
