using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace sqlBase
{
    public class RequisitionSelect
    {

        public void GetLastCodes(string p_vslcode)
        {
            try
            {
                string qry = @"SELECT       IM_CODE as item_code,
                                                AID_NO as aid_no,
                                                  ID_NO as id_no,
                                                          GRV_NO,
                                                         AGRV_NO,
                                                       P_VSLCODE,
                                                         EQ_CODE,
                                                           EQ_NO,
                                                        TRANS_NO,
                                                        CTRANS_NO                                                                                    
                                                             FROM    PURCHASE.LASTCODES 
                                                            WHERE    P_VSLCODE='" + p_vslcode + "'";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {
                Console.WriteLine("{0} Exception caught.", exc);
            }
        }

        public void GetIndDt(string vslcode)
        {
            try
            {
                string qry = @"SELECT                  ZONE,	   
                                         ID_NO as id_number,
                                       IM_CODE as item_code,	
                                    REQ_QTY as required_qty,
                                 DLV_BEFR as deliver_before,				      
                                                    UPDFLAG,			  
                                                    CC_CODE,	
                                                    ROB_QTY, 	 
                                                    VSLCODE,	  
                                     CODE_TYPE as code_type,
                                                   CCM_CODE,	  
                                                    ORDER_NO                                                                                     
                                                        FROM    PURCHASE.IND_DT 
                                                       WHERE    VSLCODE='" + vslcode + "'";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {
                Console.WriteLine("{0} Exception caught.", exc);
            }
        }

        public void GetIndHd(string vslcode)
        {
            try
            {
                string qry = @"SELECT                               ZONE,	   
                                                    ID_NO	as id_number,   
                                            CID_NO as requisition_number,	       
                                             ID_DATE as requisition_date,    
                                                  CODE_TYPE as code_type,
                                                                ID_LEVEL, 
                                                                ID_REQBY,   
                                                                 UPDFLAG,			
                                                                 FORW_BY,    
                                                               FORW_FLAG, 
                                                                   PO_NO,   
                                                                 VSLCODE,										     
                                                      EQ_NO as eq_number,
                                                                 CC_CODE,	     
                                                                    SEND, 
                                                                 TP_CODE, 
                                                                CCM_CODE, 			    
                                                             IM_CATEGORY, 								      
                                                               DEPT_CODE,     
                                                             ID_PKT_SENT, 	   
                                                              ATTACHMENT,
                                                                  FROMOFF                                                                                    
                                                                     FROM    PURCHASE.IND_HD 
                                                                    WHERE    VSLCODE='" + vslcode + "'";
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
