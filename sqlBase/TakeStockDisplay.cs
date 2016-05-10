using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace sqlBase
{
    public class TakeStockDisplay
    {
        public void GetStoreItems(char code_type)
        {
            try
            {
                string qry = @"SELECT       IM_CODE as item_code,
                                                        CIM_CODE,
                                            IM_DESC as item_desc,
                                            IM_SPEC as item_spec,
                                                        SE2_CODE,
                                            IM_UNIT as item_unit,
                                                        MIN_STOC,
                                                        MAX_STOC,
                                          CODE_TYPE as code_type,                                       
                                        DE_BY as data_entered_by,
                                       DE_AT as data_entered_date                                             
                                                             FROM    PURCHASE.ITEM_MF 
                                                            WHERE    UPDFLAG<>'D' AND CODE_TYPE='" + code_type + "'";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {
                Console.WriteLine("{0} Exception caught.", exc);
            }
        }


        public void GetSpareItems(string VSLCode)
        {
            try
            {
                string qry = @"SELECT   IM_CODE as item_code,  
                                                    CIM_CODE,
                                        IM_DESC as item_desc,
                                        IM_SPEC as item_spec,
                                                    SE2_CODE,
                                        IM_UNIT as item_unit,
                                                    MIN_STOC,
                                                    MAX_STOC,
                                      CODE_TYPE as code_type,
                                                     UPDFLAG,
                                          EQ_CODE as eq_code,
                                    DE_BY as data_entered_by,
                                   DE_AT as data_entered_date
                                                         FROM   PURCHASE.VSL_ITEM_MF 
                                                        WHERE   UPDFLAG<>'D' AND VSLCODE=" + VSLCode;
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public void GetStock(string VSLCode)
        {
            try
            {
                string qry = @"SELECT   IM_CODE as item_code,
                                          ROB_QTY as rob_qty,
                                      CODE_TYPE as code_type,                                             
                                               LAST_RECD_QTY,
                                                    TRANS_NO,
                                             TRANS_TYPE_CODE,
                                                     UPDFLAG,
                                                   MIN_STOCK,
                                                   MAX_STOCK,
                                    DE_BY as data_entered_by,
                                   DE_AT as data_entered_date
                                                         FROM    PURCHASE.STOCK
                                                        WHERE    UPDFLAG<>'D' AND VSLCODE=" + VSLCode;
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {
                Console.WriteLine("{0} Exception caught.", exc);
            }
        }
    }
}
