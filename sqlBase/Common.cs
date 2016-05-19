using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace sqlBase
{
    public class Common
    {
        public void GetAllVendorNames(string VSLCode,string Zone) // selecting all the vendor names 
        {
            try
            {
                string qry = @"SELECT DISTINCT V.VD_CODE,CVD_CODE,VD_NAME,VD_ADD1,VD_ADD2,VD_CITY,VD_STATE,CY_CODE
                                      FROM PURCHASE.VEND_MF V,
                                           PURCHASE.PO_HD P
                                      WHERE V.VD_CODE=P.VD_CODE
                                      AND P.VSLCODE	= " + VSLCode + 
                                     " AND P.ZONE='" + Zone +"'" ;
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {

            }
        }

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

        public void GetIndDt(string cc_code)
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
                                                       WHERE    CC_CODE ='" + cc_code + "'";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {
                Console.WriteLine("{0} Exception caught.", exc);
            }
        }

        public void GetIndHd(string cc_code)
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
                                                                    WHERE    VSLCODE='" + cc_code + "'";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {
                Console.WriteLine("{0} Exception caught.", exc);
            }
        }


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
