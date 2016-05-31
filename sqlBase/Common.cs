using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace sqlBase
{
    public class Common
    {
        //Fetch Vessel informations
        public void GetVesselInfo() 
        {
            try
            {
                string qry = @"SELECT VSLCODE as vessel_code,
                                      INITIAL as initial
                                      FROM PMS.PMS_VESSELMF";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {

            }
        }
        //select Vessel info 
        public void GetStatusInfo()
        {
            try
            {
                string qry = @"SELECT ST_CODE as st_code,
                                      ST_DESC as st_desc,
                                      DOC_TYPE as doc_type,
                                      ORDER_NO as order_no
                                      FROM PMS.STATUS_MF where UPDFLAG<>'D'";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {

            }
        }
        //To get setup info
        public void GetSetupInfo()
        {
            try
            {
                string qry = @"SELECT JO_PREFIX as jo_prefix,
                                      JO_SUFFIX as jo_suffix,
                                      ID_PREFIX as id_prefix,
                                      ID_SUFFIX as id_suffix
                                      FROM PMS.SETUP";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {

            }
        }
        public void GetAllVendorNames(string VSLCode,string Zone) // selecting all the vendor names 
        {
            try
            {
                //string qry = @"SELECT DISTINCT V.VD_CODE,CVD_CODE,VD_NAME,VD_ADD1,VD_ADD2,VD_CITY,VD_STATE,CY_CODE
                //                      FROM PURCHASE.VEND_MF V,
                //                           PURCHASE.PO_HD P
                //                      WHERE V.VD_CODE=P.VD_CODE
                //                      AND P.VSLCODE	= " + VSLCode + 
                //                     " AND P.ZONE='" + Zone +"'" ;
                string qry = @"SELECT DISTINCT V.VD_CODE,
                                               CVD_CODE,
                                               D_NAME
                                      FROM PURCHASE.VEND_MF V,
                                           PURCHASE.PO_HD P
                                      WHERE V.VD_CODE=P.VD_CODE
                                      AND P.VSLCODE	= " + VSLCode +
                                    " AND P.ZONE='" + Zone + "'";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {

            }
        }

        // To select PURCHASE.LASTCODES  info
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

        // To select PURCHASE.IND_DT  info
        public void GetIndDt(string VSLCode)
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
                                                       WHERE    UPDFLAG <>'D' AND VSLCODE=" + VSLCode;
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {
                Console.WriteLine("{0} Exception caught.", exc);
            }
        }

        //To select PURCHASE.IND_HD info
        public void GetIndHd(string VSLCode)
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
                                                                    WHERE    UPDFLAG <>'D' AND VSLCODE=" + VSLCode;
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {
                Console.WriteLine("{0} Exception caught.", exc);
            }
        }

        // To get Store info
        public void GetStoreItems()
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
                                                            WHERE    UPDFLAG<>'D'";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {
                Console.WriteLine("{0} Exception caught.", exc);
            }
        }

        //To get Spare info
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

        //To fetch Stock information
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
        /*
         *  To retrieve active main Equipments of a Vessel 
        */
        public void GetEquipments(string VSLCode)
        {
            try
            {
                string qry = @"SELECT ZONE as zone,
                                     EQ_NO as eq_number,
                                   EQ_NAME as eq_name,
                                   UPDFLAG as upd_flag,
                                  CEQ_CODE as ceq_code,
                                   EQ_CODE as eq_code 
                                      FROM PURCHASE.EQ_MF
                                     WHERE VSLCODE=" + VSLCode + "AND UPDFLAG<>'D' AND RH_ENTRY = 1 ORDER BY CEQ_CODE";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();

                //foreach (DataRow r in tbl.Rows)
                //{
                //    Console.WriteLine("  {0}", r["AIRNAME"]);
                //}
            }
            catch (Exception exc)
            {

            }
        }
        /*
         * To retrieve active Zones of a Vessel
        */
        public void GetZones(string VSLCode)
        {
            try
            {
                string qry = @"SELECT ST_DESC as zone,
                                     ORDER_NO as order
                                         FROM PMS.STATUS_MF  
                                        WHERE DOC_TYPE ='LO' AND UPDFLAG<>'D' AND ST_CODE
                                           IN (SELECT DISTINCT LC_ST_CODE FROM PURCHASE.EQ_MF WHERE VSLCODE ='"+ VSLCode +"')";

                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();

                //foreach (DataRow r in tbl.Rows)
                //{
                //    Console.WriteLine("  {0}", r["AIRNAME"]);
                //}
            }
            catch (Exception exc)
            {

            }
        }

        /*
         * To retrieve current running hour of all active equipments
        */
        public void GetEquipmentsPresentRunningHours(string VSLCode)
        {
            try
            {
                string qry = @"SELECT EQ_CODE as EQCode,
                                      VSLCODE as VSLCode,
                                  RH_PREVIOUS as RHPrevious,
                                       RH_ADD as RHAdd,
                                   READING_DT as ReadingDT,
                                   READING_BY as ReadingBy,
                                  AVG_PER_DAY as AVGPerDay,
                                        DE_BY as DEBy,
                                        DE_AT as DEAt,
                                      UPDFLAG as UPDFlag,
                                  LAST_RH_ADD as LastRHAdd,
                              LAST_READING_DT as LastReadingDT,
                              LAST_READING_BY as LastReadingBy,            
                                     DELETEDT as DeleteDT
                                         FROM PMS.RH_ENTRY 
                                        WHERE UPDFLAG<>'D' AND VSLCODE=" + VSLCode + " ORDER BY EQ_CODE";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();

                //foreach (DataRow r in tbl.Rows)
                //{
                //    Console.WriteLine("  {0}", r["AIRNAME"]);
                //}
            }
            catch (Exception exc)
            {

            }
        }

        /*
         * To retrieve previous running hour of all active equipments
        */
        public void GetEquipmentsPreviousRunningHours(string VSLCode)
        {
            try
            {
                string qry = @"SELECT EQ_CODE as EQCode,
                                      VSLCODE as VSLCode,
                                  RH_PREVIOUS as RHPrevious,
                                       RH_ADD as RHAdd,
                                   READING_DT as ReadingDT,
                                   READING_BY as ReadingBy,
                                  AVG_PER_DAY as AVGPerDay,
                                        DE_BY as DEBy,
                                        DE_AT as DEAt,
                                      UPDFLAG as UPDFlag,
                                  LAST_RH_ADD as LastRHAdd,
                              LAST_READING_DT as LastReadingDT,
                              LAST_READING_BY as LastReadingBy,            
                                     DELETEDT as DeleteDT
                                         FROM PMS.RH_ENTRY_LOG 
                                        WHERE UPDFLAG<>'D' AND VSLCODE=" + VSLCode + " ORDER BY EQ_CODE";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();

                //foreach (DataRow r in tbl.Rows)
                //{
                //    Console.WriteLine("  {0}", r["AIRNAME"]);
                //}
            }
            catch (Exception exc)
            {

            }
        }
        
        public void GetJobOrder(string VSLCode) // To get all the job orders 
        {
            try
            {
                string qry = @"SELECT   JO_CODE          as jo_code,
                                        CJO_CODE         as cjo_code,
                                        EQ_CODE          as eq_code,
                                        JO_TITLE         as jo_title,
                                        JO_START_DT      as jo_start_date,
                                        JO_END_DT        as jo_end_date,
                                        CONDITION_BEFORE as condition_before,
                                        CONDITION_AFTER  as condition_after,
                                        RESP_CREW_NAME   as resp_crew_name,
                                        PRIORITY_ST_CODE as priority_code,
                                        JO_ST_CODE       as jo_status_code,                   
                                        DE_AT            as data_entered_date,
                                        DE_BY            as data_entered_by,
                                        UPDFLAG          as is_updated_on_server 
                                   FROM PMS.JOB_ORDER 
                                   WHERE UPDFLAG<>  'D'
                                   AND VSLCODE = " + VSLCode  +
                                   " ORDER BY EQ_CODE";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();

            }
            catch (Exception exc)
            {

            }
        }

        public void GetAllPOHdrDetails(string VSLCode, string Zone) // getting purchase details from PURCHASE.GRV_HD and PURCHASE.PO_HD corresponding to the vslcode  and  zone 
        {
            try
            {
                string qry = @"SELECT DISTINCT  PH.PO_NO   as po_number,
								PH.CPO_NO  as cpo_number,
								PH.VD_CODE as vd_code,
								PH.PO_DATE as po_date,
								GH.CHL_NO  as challan_number,
								GH.GRV_DT  as receipt_date ,
								GH.GRV_REM as remarks 
								FROM PURCHASE.PO_HD PH 
								LEFT JOIN PURCHASE.GRV_DT 
								GD ON PH.PO_NO = GD.PO_NO
								LEFT JOIN PURCHASE.GRV_HD GH 
								ON GD.GRV_NO = GH.GRV_NO 
								WHERE GH.VSLCODE= " + VSLCode +
                        "AND GH.ZONE  ='" + Zone +
                        "'AND GH.UPDFLAG <>'D'";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();

            }
            catch (Exception exc)
            {

            }
        }

        public void GetAllPOItemDetails(string VSLCode, string Zone) // getting item details from PURCHASE.ID_FINAL_DT and PURCHASE.GRV_DT corresponding to the vslcode  and  zone 
        {
            try
            {
                string qry = @"SELECT   I.PO_NO     as po_number,
							I.IM_CODE   as item_code,
							I.CODE_TYPE as code_type,
							I.REQ_QTY   as requested_qty,
							I.PO_IM_QTY as ordered_qty,
							G.QTY_RECD  as received_qty,
							G.QTY_ACPT  as accepted_qty
							FROM PURCHASE.ID_FINAL_DT I 
							JOIN PURCHASE.GRV_DT G
							ON I.PO_NO=G.PO_NO 
							AND I.IM_CODE=G.IM_CODE 
							WHERE I.VSLCODE=" + VSLCode +
                    "AND I.ZONE='" + Zone +
                    "' AND I.UPDFLAG<>'D'";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();

            }
            catch (Exception exc)
            {

            }
        }

    }
}
