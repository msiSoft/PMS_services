﻿using System;
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

        public void SavePurchaseHdr(string vslcode, string grvno_auto, string cgrv_no, POHdr po)
        {
            string qry = @"INSERT INTO PURCHASE.GRV_HD 
                                                        ( 
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
                                                         VALUES   ('" + grvno_auto + "','" + cgrv_no + "', " + po.challan_number + ",'" + po.remarks + "','" + po.data_entered_by + "','" + po.receipt_date + "','Y','" + po.data_entered_date + "'," + vslcode + ",'" + po.vd_code + "')";
            DBOperations DB = new DBOperations();
            int result = DB.OperationsOnSourceDB(qry);


        }
    }
}
