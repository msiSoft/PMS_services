using System;
using System.Collections.Generic;
using System.Data;
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


        public void SetInsertPurchaseGoodsRecieptDetail(string vslcode, string grvno_auto, POFinal goodsReceivedtl, string vendorcode,string zone)
        {
            string qry = @"INSERT INTO PURCHASE.GRV_DT 
                                                        ( 
                                                        ZONE,
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
                                                         VALUES   ('" + zone + "','" + grvno_auto + "','" + goodsReceivedtl.item_code + "', " + goodsReceivedtl.received_qty + "," + goodsReceivedtl.accepted_qty + ",'Y'," + vslcode + ",'" + goodsReceivedtl.code_type + "' ,'" + goodsReceivedtl.po_number + "' ,'" + vendorcode + "')";
            DBOperations DB = new DBOperations();
            int result = DB.OperationsOnSourceDB(qry);
        }

        public void SetUpdatePurchaseFinalDetail(POFinal goodsReceivedtl,string zone)
        {
            int recd_qty = 0; 
            int itm_qty=0;
            char flg;
            string qry = @" SELECT  PO_GRV_RECD_QTY,		
                                    PO_IM_QTY
                                    FROM PURCHASE.ID_FINAL_DT
                                    WHERE PO_NO 	='" + goodsReceivedtl.po_number  +
                                    "' AND IM_CODE  ='" + goodsReceivedtl.item_code +
                                    "' AND ZONE='"+ zone + 
                                    "' AND UPDFLAG 	<> 	'D' ";
            SqlBase_OleDb db = new SqlBase_OleDb(qry);
            DataTable tbl = db.GetTable();
            foreach (DataRow row in tbl.Rows)
            {
                recd_qty = Convert.ToInt32 (row["PO_GRV_RECD_QTY"]);
                itm_qty = Convert.ToInt32(row["PO_IM_QTY"]);
            }

            if (recd_qty + goodsReceivedtl.accepted_qty >= itm_qty)
            {
                flg = 'Y';
            }
            else if (recd_qty + goodsReceivedtl.accepted_qty <= 0)
            {
                flg = 'N';
            }
            else
            {
                flg = 'H'; //Partial 
            }

            string updqry = @"UPDATE PURCHASE.ID_FINAL_DT SET 
                                                             PO_GRV_RECD_QTY 	= 	PO_GRV_RECD_QTY + "+ goodsReceivedtl.accepted_qty  +
		                                                    ", PO_GRV_CLOSED 		= 	'"+flg +
                                                            "',  UPDFLAG			=	" + "'C' " +
                                        " WHERE  PO_NO 	='" + goodsReceivedtl.po_number +
                                        "' AND IM_CODE  ='" + goodsReceivedtl.item_code + "' ";


            DBOperations DB = new DBOperations();
            int result = DB.OperationsOnSourceDB(updqry);
        }

        public void SetUpdatePurchaseOrderDetail(POFinal goodsReceivedtl, string zone)
        {
            int recd_qty = 0;
            int itm_qty = 0;
            char flg;
            string qry = @" SELECT  RECD_QTY,		
                                    IM_QTY
                                    FROM PURCHASE.PO_DT
                                    WHERE PO_NO 	='" + goodsReceivedtl.po_number +
                                    "' AND IM_CODE  ='" + goodsReceivedtl.item_code+
                                    "' AND ZONE='" + zone +
                                    "' AND UPDFLAG 	<> 	'D' ";
            SqlBase_OleDb db = new SqlBase_OleDb(qry);
            DataTable tbl = db.GetTable();
            foreach (DataRow row in tbl.Rows)
            {
                recd_qty = Convert.ToInt32(row["RECD_QTY"]);
                itm_qty = Convert.ToInt32(row["IM_QTY"]);
            }

            if (recd_qty + goodsReceivedtl.accepted_qty >= itm_qty)
            {
                flg = 'Y';
            }
            else if (recd_qty + goodsReceivedtl.accepted_qty <= 0)
            {
                flg = 'N';
            }
            else
            {
                flg = 'H'; //Partial 
            }

            string updqry = @"UPDATE PURCHASE.PO_DT SET 
                                                             RECD_QTY 	= 	RECD_QTY + " + goodsReceivedtl.accepted_qty +
                                                            ", CLOSED 		= 	'" + flg +
                                                            "',  UPDFLAG			=	" + "'C' " +
                                        " WHERE  PO_NO 	='" + goodsReceivedtl.po_number +
                                        "' AND IM_CODE  ='" + goodsReceivedtl.item_code +
                                        "' AND UPDFLAG 	<> 	'D'";


            DBOperations DB = new DBOperations();
            int result = DB.OperationsOnSourceDB(updqry);
        }

        public void SetUpdatePurchaseStock(POFinal goodsReceivedtl, string vslcode)
        {
            string dt = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
            double amt = 0.00;
            double price = 0.00;
            int qty = 0;
            string selqry = @" SELECT PO_IM_AMT,
                                      PO_IM_QTY 
                                FROM PURCHASE.ID_FINAL_DT  
                                WHERE VSLCODE = " + vslcode +  
                                " AND IM_CODE ='" + goodsReceivedtl.item_code +
                                "' AND PO_NO='" + goodsReceivedtl.po_number +
                               "' AND CODE_TYPE='" + goodsReceivedtl.code_type + "'";
            SqlBase_OleDb db = new SqlBase_OleDb(selqry);
            DataTable tbl = db.GetTable();
            foreach (DataRow row in tbl.Rows)
            {
                amt = Convert.ToInt32(row["PO_IM_AMT"]);
                qty = Convert.ToInt32(row["PO_IM_QTY"]);
            }
            price = amt / qty;

            string qry = @"UPDATE PURCHASE.STOCK SET 
                                                      ROB_QTY=ROB_QTY +  " + goodsReceivedtl.accepted_qty +
                                                    ", LAST_RECD_QTY = " + goodsReceivedtl.accepted_qty +
                                                    ", LAST_RECD_DT = " + dt +
                                                    ", TOTAL_IN = TOTAL_IN + " + goodsReceivedtl.accepted_qty +
                                                    ", LAST_RECD_PRICE	= "+price + 
                                                    ",DE_BY = '" + goodsReceivedtl.data_entered_by +
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
