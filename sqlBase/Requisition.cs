using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace sqlBase
{
    class Requisition
    {
        
        public string id_number { get; set; }
        public string eq_number { get; set; }
        public string vessel_code { get; set; }
        public string item_code { get; set; }
        public string required_qty { get; set; }
        public string deliver_before { get; set; }
        public string code_type { get; set; }
        public string requisition_number { get; set; }
        public string requisition_date { get; set; }
        public string data_enetered_date { get; set; }
        public string data_entered_at { get; set; }
        public string zone { get; set; }




        public void SaveRequisitionUpd(Requisition Requisition)

        {
            try
            {
                string qry = @"UPDATE PURCHASE.LASTCODES SET   ID_NO   ='" + Requisition.id_number +
                                                     "' WHERE   EQ_NO   ='" + Requisition.eq_number + "'";
                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry);
            }
            catch (Exception exc)
            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public void SaveRequisitionInsrtDt(Requisition Requisition)

        {
            try
            {

                string val = @" SELECT  ROB_QTY 
                                FROM    PURCHASE.STOCK
                                WHERE IM_CODE ='" + Requisition.item_code ;
                SqlBase_OleDb db = new SqlBase_OleDb(val);
                DataTable tbl = db.GetTable();
                string rob_qty = tbl.Rows[0]["ROB_QTY"].ToString();

                string qry = @"INSERT INTO   PURCHASE.IND_DT (      ZONE, 
                                                                 VSLCODE, 
                                                                   ID_NO, 
                                                                 IM_CODE, 
                                                                 REQ_QTY, 
                                                                DLV_BEFR, 
                                                                 CC_CODE,  
                                                                CCM_CODE, 
                                                                 ROB_QTY, 
                                                                 UPDFLAG, 
                                                               CODE_TYPE, 
                                                                 ORDER_NO )
                                      VALUES   (  '" + Requisition.zone + 
                                        "','" + Requisition.vessel_code + 
                                          "','" + Requisition.id_number + 
                                          "','" + Requisition.item_code + 
                                       "','" + Requisition.required_qty +
                                     "','" + Requisition.deliver_before + 
                                            "','Z','Z','" + rob_qty + 
                                            "','C','" + Requisition.code_type + "')";
                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry);


            }
            catch (Exception exc)
            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        

        public void SaveRequisitionInsrtHd(Requisition Requisition)

        {
            try
            {

                string val = @" SELECT  TP_CODE 
                                FROM    PURCHASE.IM_TYPE";
                SqlBase_OleDb db = new SqlBase_OleDb(val);
                DataTable tbl = db.GetTable();
                string tp_code = tbl.Rows[0]["TP_CODE"].ToString();

                string ans = @" SELECT  DEPT_CODE 
                                FROM    PURCHASE.DEPARTMENT";
                SqlBase_OleDb db1 = new SqlBase_OleDb(ans);
                DataTable tbl1 = db.GetTable();
                string dept_code = tbl.Rows[0]["DEPT_CODE"].ToString();

                string qry = @"INSERT INTO   PURCHASE.IND_HD (                   ZONE,	   
                                                                                   ID_NO,
                                                                                 VSLCODE,
                                                                                  CID_NO,      
                                                                                 ID_DATE, 
                                                                               CODE_TYPE,
                                                                                ID_LEVEL,
                                                                                 UPDFLAG,
                                                                               FORW_FLAG,
                                                                                   PO_NO,
                                                                                   EQ_NO,
                                                                                 CC_CODE,
                                                                                CCM_CODE,
                                                                                    SEND,
                                                                                 TP_CODE,
                                                                             IM_CATEGORY,
                                                                               DEPT_CODE,
                                                                              ATTACHMENT,
                                                                                  FROMOFF )
                                       VALUES           (         '" + Requisition.zone +
                                                          "','" + Requisition.id_number +
                                                      "','" + Requisition.vessel_code + 
                                                 "','" + Requisition.requisition_number + 
                                                 "','" + Requisition.requisition_date +
                                                          "','" + Requisition.code_type +
                                                           "','N', 'C','Z','Z','" + Requisition.eq_number +
                                                            "','Z','Z','1','" + tp_code +
                                                        "','G','" + dept_code +
                                                         "','0','V')";
                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry);

            }
            catch (Exception exc)
            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }


    }
}
