using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sqlBase.Classes
{
    public class POHdr
    {
        public string po_number { get; set; }
        public string cpo_number { get; set; }
        public string vd_code { get; set; }
        public string po_date { get; set; }
        public int challan_number { get; set; }
        public string receipt_date { get; set; }
        public string remarks { get; set; }
        public string data_entered_date { get; set; }
        public string data_entered_by { get; set; }
        public bool is_updated_on_server { get; set; }

        public void SavePurchaseHdr(string vslcode, string grvno_auto, string cgrv_no, POHdr goodsReceivedHdr,string Zone)
        {
            string qry = @"INSERT INTO PURCHASE.GRV_HD 
                                                        (
                                                        ZONE, 
                                                        GRV_NO,
                                                        CGRV_NO,	
                                                        CHL_NO,
                                                        GRV_REM,
                                                        PREP_BY,			
                                                        GRV_DT,
                                                        UPDFLAG,
                                                        PREP_AT,
                                                        VSLCODE,			
                                                        VD_CODE
                                                        )
                                                         VALUES   ('" + Zone  + "','" + grvno_auto + "','" + cgrv_no + "', " + goodsReceivedHdr.challan_number + ",'" + goodsReceivedHdr.remarks + "','" + goodsReceivedHdr.data_entered_by + "','" + goodsReceivedHdr.receipt_date + "','Y','" + goodsReceivedHdr.data_entered_date + "'," + vslcode + ",'" + goodsReceivedHdr.vd_code + "')";
            DBOperations DB = new DBOperations();
            int result = DB.OperationsOnSourceDB(qry);

        }

        public void UpdPOHdr(string vslcode, POHdr goodsReceivedHdr,string Zone)
        {
            string qry = @" UPDATE PURCHASE.PO_HD SET
                                                    NO_GRV=  @NULLVALUE(NO_GRV,0)+1 
                                                    , DELIVERED_DT=" + goodsReceivedHdr.receipt_date +
                                                    ", UPDFLAG =" + "'D' " +
                                                     " WHERE VSLCODE = " + vslcode +
                                                     " AND ZONE='"+ Zone +
                                                     "' AND PO_NO ='" + goodsReceivedHdr.po_number +
                                                      "' AND UPDFLAG <> " + "'D'";
            DBOperations DB = new DBOperations();
            int result = DB.OperationsOnSourceDB(qry);
        }
    }
}
