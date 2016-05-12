using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace sqlBase
{
    public class Purchase
    {
        public void GetPONumbers(string VSLCode) // selecting ponumber, vendor code etc
        {
            try
            {
                string flg = "'D'";

                string qry = @"SELECT DISTINCT  PO_HD.CPO_NO,  
                                                PO_HD.PO_NO, 
                                                PO_HD.VD_CODE
	                                            FROM PURCHASE.PO_HD,
                                                     PURCHASE.PO_DT
	                                            WHERE 	PO_HD.PO_NO = PO_DT.PO_NO
	                                            AND	PO_HD.VSLCODE	= " + VSLCode
                                                 + " AND	PO_HD.UPDFLAG 	<> 	" + flg
                                                + " AND	PO_DT.VSLCODE	= " + VSLCode
                                                + " AND	PO_DT.UPDFLAG 	<> " + flg
                                                + " AND	PO_DT.RECD_QTY < PO_DT.IM_QTY ORDER	BY PO_HD.PO_NO";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {

            }
        }

        public void GetAllPODetails(string VSLCode) // getting item details from PURCHASE.ID_FINAL_DT corresponding to the vslcode 
        {
            try
            {

                string qry = @"SELECT F.ZONE,
                                      F.ID_NO,  
                                      F.IM_CODE,
                                      H.PO_DATE,  
                                      F.REQ_QTY,
                                      F.IV_QTY,
                                      F.ORDER_NO,
                                      F.TD_QTY,
                                      F.PO_IM_QTY,
                                      F.PO_IV_RECD_QTY,
                                      F.PO_GRV_RECD_QTY,
                                      F.PO_GRV_CLOSED,
                                      F.ROB_QTY
                                      FROM PURCHASE.ID_FINAL_DT F,
                                            PURCHASE.PO_HD H
                                      WHERE  F.VSLCODE= " + VSLCode;
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {

            }
        }
    }
}
