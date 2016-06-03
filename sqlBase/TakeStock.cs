using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text; 

namespace sqlBase
{ 
    public class TakeStock
    {
        public string rob_qty { get; set; }
        public string physical_rob_entered { get; set; }
        public string data_entered_by { get; set; }
        public string data_entered_date { get; set; }
        public string item_code { get; set; }
        public string vessel_code { get; set; }
        public string code_type { get; set; }
        public object trans_type_code { get; set; }

        DBOperations db = new DBOperations();

        //Updating ROB_QTY to PURCHASE.STOCK .
        public void SetUpdatePurchaseStock(TakeStock stock)

        {
            try
            {
                string qry = @"UPDATE PURCHASE.STOCK SET         ROB_QTY      =              '" + stock.rob_qty +
                                                     "',        TOTAL_IN      = '" + stock.physical_rob_entered +
                                                     "',           DE_BY      =      '" + stock.data_entered_by + 
                                                     "',           DE_AT      =    '" + stock.data_entered_date +
                                                     "',         UPDFLAG      =                            'C'" +
                                                "' WHERE         IM_CODE      =       '" + stock.item_code + "'";
                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry);
            }
            catch (Exception exc)
            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        //Inserting  values to PURCHASE.TRANS_HD table
        public void SetInsertPurchaseTransHeader(TakeStock stock)

        {          
            try
            {
                /*TRANS_TYPE_CODE*/
                decimal rob_qty = Convert.ToDecimal(stock.rob_qty);
                decimal physical_rob_entered = Convert.ToDecimal(stock.physical_rob_entered);
                if (rob_qty > physical_rob_entered) // If transaction is Issue
                {
                    object trans_type_code = db.ExecuteScalarOnSourceDB("SELECT trans_type_code FROM purchase.trans_type WHERE prog_code = 'IS' AND updflag <> 'D'");
                }
                else if(physical_rob_entered > rob_qty) // If transaction is Purchase
                {
                    object trans_type_code = db.ExecuteScalarOnSourceDB("SELECT trans_type_code FROM purchase.trans_type WHERE prog_code = 'PO' AND updflag <> 'D'");                    
                }   
                 
                 /*TRANS_NO*/
                //To get values for trans_no calculations          
                object trans_noL = db.ExecuteScalarOnSourceDB(" SELECT TRANS_NO FROM PURCHASE.LASTCODES WHERE P_VSLCODE = 'COMMON'  ");
                object codeprefix = db.ExecuteScalarOnSourceDB("SELECT  CODE_PREFIX from PURCHASE.SETUP");
                string trans_no = codeprefix + "." + "0000000000" + trans_noL; //Calculation for getting TRANS_NO
                // After calculating trans_no updating it to purchase.lastcodes table
                string qry1 = @"UPDATE purchase.lastcodes SET  TRANS_NO= trans_no WHERE p_vslcode = 'COMMON'";
                DBOperations UI1 = new DBOperations();
                int result1 = UI1.OperationsOnSourceDB(qry1);

                /*CTRANS_NO*/
                // Setting ctrans_no and updating it to purchase.lastcodes
                string qry2 = @"UPDATE purchase.lastcodes SET ctrans_no = ctrans_no +1 WHERE	p_vslcode ='" + stock.vessel_code + "'" ;
                DBOperations UI2 = new DBOperations();
                int result2 = UI2.OperationsOnSourceDB(qry2);
                //To get values for ctrans_no calculation
                object leadzero = db.ExecuteScalarOnSourceDB("select LEAD_ZEROES from pms.setup");
                object nCLastTransNo = db.ExecuteScalarOnSourceDB("SELECT	ctrans_no FROM	purchase.lastcodes WHERE   p_vslcode = '" + stock.vessel_code + "'");
                int c = Convert.ToString(trans_noL).Length;
                string b="0";
                int a = Convert.ToInt32(leadzero) - c;
                for (int i = 0; i <= a; i++)
                {
                    b = b + "0";
                }
                object sPrefixTR =db.ExecuteScalarOnSourceDB("select TR_PREFIX from pms.setup");
                object sSuffixTR = db.ExecuteScalarOnSourceDB("select TR_SUFFIX from pms.setup");
                string ctrans_no = b + sPrefixTR + nCLastTransNo + sSuffixTR;//Calculation for getting CTRANS_NO

                string qry = @"INSERT INTO   PURCHASE.TRANS_HD (        VSLCODE, 
                                                                       TRANS_NO, 
                                                                      CTRANS_NO, 
                                                                TRANS_TYPE_CODE, 
                                                                     TRANS_DATE, 
                                                                     TRANS_FROM, 
                                                                        UPDFLAG, 
                                                                          DE_BY, 
                                                                          DE_AT)
                                     VALUES            ('" + stock.vessel_code + 
                                                              "','" + trans_no +
                                                             "','" + ctrans_no + 
                                                       "','" + trans_type_code + 
                                                                      "','PS'" + 
                                                                       "','C'" + 
                                                 "','" + stock.data_entered_by + 
                                         "','" + stock.data_entered_date + "')";
                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry);
            }
            catch (Exception exc)
            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        //Inserting  values to PURCHASE.TRANS_TD table
        public void SetInsertPurchaseTransDetail(TakeStock stock)

        {
            try
            {
                /*TRANS_NO*/
                //To get values for trans_no calculations  
                object trans_noL = db.ExecuteScalarOnSourceDB(" SELECT TRANS_NO FROM PURCHASE.LASTCODES WHERE P_VSLCODE = 'COMMON'  ");
                object codeprefix = db.ExecuteScalarOnSourceDB("SELECT  CODE_PREFIX from PURCHASE.SETUP");
                string trans_no = codeprefix + "." + "0000000000" + trans_noL;//Calculation for getting TRANS_NO

                /*TRANS_QTY*/
                //To get values for trans_qty calculations  
                decimal rob_qty = Convert.ToDecimal(stock.rob_qty);
                decimal physical_rob_entered = Convert.ToDecimal(stock.physical_rob_entered);
                decimal trans_qty = Math.Abs(rob_qty - physical_rob_entered); //Calculation for getting TRANS_QTY

                string qry  =    @"INSERT INTO   PURCHASE.TRANS_DT (                VSLCODE, 
                                                                                   TRANS_NO, 
                                                                                    IM_CODE, 
                                                                                  CODE_TYPE, 
                                                                                  TRANS_QTY,
                                                                                    UPDFLAG, 
                                                                                   PLAN_QTY, 
                                                                                      DE_BY, 
                                                                                      DE_AT)
                                            VALUES                 ('" + stock.vessel_code + 
                                                                          "','" + trans_no + 
                                                                   "','" + stock.item_code + 
                                                                   "','" + stock.code_type + 
                                                                         "','" + trans_qty + 
                                                                                   "','C'" +
                                                                                    "','0'"+
                                                             "','" + stock.data_entered_by + 
                                                     "','" + stock.data_entered_date + "')";

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

