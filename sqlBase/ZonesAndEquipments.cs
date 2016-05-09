using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace sqlBase
{
    public class ZonesAndEquipments
    {
        /*
         *  To retrieve active main Equipments of a Vessel 
        */
        public void GetEquipments(string VSLCode)
        {
            try {
                string qry = @"SELECT ZONE as Zone,
                                   VSLCODE as VSLCode,
                                     EQ_NO as EQNO,
                                   EQ_NAME as EQName,
                                   UPDFLAG as UPDFlag,
                                  CEQ_CODE as CEQCode,
                                   EQ_CODE as EQCode 
                                      FROM PURCHASE.EQ_MF
                                     WHERE VSLCODE=" + VSLCode+ "AND UPDFLAG<>'D' AND RH_ENTRY = 1 ORDER BY CEQ_CODE";
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
                string qry = @"SELECT ST_CODE as STCode,
                                      ST_DESC as STDESC,
                                     DOC_TYPE as DOCType,
                                      VSLCODE as VSLCode,
                                      UPDFLAG as UPDFlag,
                                     ORDER_NO as OrderNO,
                                    PROG_CODE as PROGCode,
                                     DELETEDT as DeleteDT
                                         FROM PMS.STATUS_MF  
                                        WHERE UPDFLAG<>'D' AND VSLCODE=" + VSLCode;
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
        /*Insert or update Running Hour in running hour related tables*/
        //public void SetRH()
        //{
        //    try
        //    {
        //        OleDbHelper.updSrcDb("INSERT INTO PMS.RH_ENTRY (EQ_CODE) VALUES('2222')");
        //    }
        //    catch(Exception exc)
        //    {

        //    }
        //}
    }
}
