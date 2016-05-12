using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text; 

namespace sqlBase
{ 
    public class Stock
    {
        public string rob_qty { get; set; }
        public string trans_type_code { get; set; }
        public string data_entered_by { get; set; }
        public string data_entered_date { get; set; }
        public string total_in { get; set; }
        public string updflag { get; set; }
        public string item_code { get; set; }
        public string vessel_code { get; set; }
        public string code_type { get; set; }
        public string trans_no { get; set; }
        public string ctrans_no { get; set; }
        public string trans_date { get; set; }
        public string trans_from { get; set; }
        public string trans_qty{ get; set; }
        public string plan_qty{ get; set; }
        public string trans_rem{ get; set; }   
        public string im_rem { get; set; }


        public void SaveStockUpd(Stock stock)

        {
            try
            {
                string qry = @"UPDATE PURCHASE.STOCK SET               ROB_QTY      =          '" + stock.rob_qty + 
                                                      "',      TRANS_TYPE_CODE      =  '" + stock.trans_type_code + 
                                                      "',                DE_BY      =  '" + stock.data_entered_by + 
                                                      "',                DE_AT      ='" + stock.data_entered_date +
                                                      "',              UPDFLAG      =          '" + stock.updflag +
                                                     "' WHERE          IM_CODE      =        '" + stock.item_code + "'";
                Update_Insert UI = new Update_Insert();
                int result = UI.OperationsOnSourcecDB(qry);
            }
            catch (Exception exc)
            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public void SaveStockInsertHd(Stock stock)

        {
            try
            {
                string qry = @"INSERT INTO   PURCHASE.TRANS_HD (        VSLCODE, 
                                                                       TRANS_NO, 
                                                                      CTRANS_NO, 
                                                                TRANS_TYPE_CODE, 
                                                                     TRANS_DATE, 
                                                                     TRANS_FROM, 
                                                                        UPDFLAG, 
                                                                          DE_BY, 
                                                                           DE_AT  )
                                     VALUES            ('" + stock.vessel_code + 
                                                        "','" + stock.trans_no +
                                                       "','" + stock.ctrans_no + 
                                                 "','" + stock.trans_type_code + 
                                                      "','" + stock.trans_date + 
                                                      "','" + stock.trans_from + 
                                                         "','" + stock.updflag + 
                                                 "','" + stock.data_entered_by + 
                                               "','" + stock.data_entered_date + "')";
                Update_Insert UI = new Update_Insert();
                int result = UI.OperationsOnSourcecDB(qry);


            }
            catch (Exception exc)
            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public void SaveStockInsertTd(Stock stock)

        {
            try
            {
                string qry  =    @"INSERT INTO   PURCHASE.TRANS_DT (                VSLCODE, 
                                                                                   TRANS_NO, 
                                                                                    IM_CODE, 
                                                                                  CODE_TYPE, 
                                                                                  TRANS_QTY,
                                                                                    UPDFLAG, 
                                                                                   PLAN_QTY, 
                                                                                      DE_BY, 
                                                                                       DE_AT )
                                            VALUES                 ('" + stock.vessel_code + 
                                                                    "','" + stock.trans_no + 
                                                                   "','" + stock.item_code + 
                                                                   "','" + stock.code_type + 
                                                                   "','" + stock.trans_qty + 
                                                                     "','" + stock.updflag + 
                                                                     "','" + stock.plan_qty+
                                                             "','" + stock.data_entered_by + 
                                                           "','" + stock.data_entered_date + "')";

                Update_Insert UI = new Update_Insert();
                int result = UI.OperationsOnSourcecDB(qry);

            }
            catch (Exception exc)
            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }
    }
    
}

