using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace sqlBase
{
    public class Purchase
    {
        public void GetPONumbers(string VSLCode,string Zone) // selecting ponumber, vendor code etc
        {
            try
            {

                string flg = "'D'";

                string qry = @"SELECT DISTINCT  PO_HD.ZONE,
                                                PO_HD.CPO_NO as cpo_number,  
                                                PO_HD.PO_NO as po_number, 
                                                PO_HD.VD_CODE as vd_code 
	                                            FROM PURCHASE.PO_HD,
                                                     PURCHASE.PO_DT
	                                            WHERE 	PO_HD.PO_NO = PO_DT.PO_NO
	                                            AND	PO_HD.VSLCODE	= " + VSLCode 
                                                 + " AND PO_HD.ZONE='"+Zone +
                                                 "' AND	PO_HD.UPDFLAG 	<> 	" + flg
                                                + " AND	PO_DT.VSLCODE	= " + VSLCode
                                                + " AND PO_DT.ZONE='"+ Zone 
                                                + "' AND	PO_DT.UPDFLAG 	<> " + flg
                                                + " AND	PO_DT.RECD_QTY < PO_DT.IM_QTY ORDER	BY PO_HD.PO_NO";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {

            }
        }

        public void GetAllPODetails(string VSLCode, string Zone) // getting item details from PURCHASE.ID_FINAL_DT corresponding to the vslcode  and  zone 
        {
            try
            {

                string qry = @"SELECT DISTINCT 
                                      F.IM_CODE as item_code,
                                      F.PO_NO as po_number,
                                      H.PO_DATE as po_date,  
                                      F.REQ_QTY as requested_qty,
                                      F.IV_QTY,
                                      F.ORDER_NO,
                                      F.TD_QTY,
                                      F.PO_IM_QTY as ordered_qty,
                                      F.PO_IV_RECD_QTY,
                                      F.PO_GRV_RECD_QTY,
                                      F.PO_GRV_CLOSED,
                                      F.ROB_QTY
                                      FROM PURCHASE.ID_FINAL_DT F,
                                            PURCHASE.PO_HD H
                                      WHERE  F.PO_NO=H.PO_NO       
                                      AND   F.VSLCODE= " + VSLCode +
                                      " AND H.ZONE='" +Zone + "'";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {

            }
        }
    }
}
