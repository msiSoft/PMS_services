using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
            try
            {
                string qry = @"SELECT ZONE as Zone,
                                   VSLCODE as VSLCode,
                                     EQ_NO as EQNO,
                                   EQ_NAME as EQName,
                                   UPDFLAG as UPDFlag,
                                  CEQ_CODE as CEQCode,
                                   EQ_CODE as EQCode 
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
        public void SetRH(RunningHour RH)
        {
            try
            {
                string maxPerDay = "";
                string avgPerDay = "";
                if (GetEquipmentDetails(RH.eq_code) <= 0)
                {         
                    string qry = @"INSERT INTO PMS.RH_ENTRY (VSLCODE, 
                                                                EQ_CODE, 
                                                                RH_PREVIOUS, 
                                                                RH_ADD,
                                                                READING_DT,
                                                                READING_BY,
                                                                DE_BY,
                                                                DE_AT,
                                                                UPDFLAG,
                                                                LAST_RH_ADD,
                                                                LAST_READING_DT,
                                                                LAST_READING_BY,
                                                                MAX_PER_DAY,
                                                                AVG_PER_DAY)
                                    VALUES   ('" + RH.vessel_code +
                                                "','" + RH.eq_code +
                                                "','" + RH.rh_previous +
                                                "','" + RH.rh_add +
                                                "','" + RH.data_entered_date +
                                                "','" + RH.data_entered_by +
                                                "','" + '$' + RH.data_entered_by +
                                                "','" + RH.data_entered_date +
                                                "','" + 'C' +
                                                "','" + RH.last_rh_add +
                                                "','" + RH.last_data_entered_date +
                                                "','" + RH.last_data_entered_by +
                                                "','" + maxPerDay +
                                                "','" + avgPerDay + "')";
                    DBOperations UI = new DBOperations();
                    int result = UI.OperationsOnSourceDB(qry);
                }
                else
                {
                    string qry = @"UPDATE PMS.RH_ENTRY SET 
                                                            RH_PREVIOUS = '" + RH.rh_previous +
                                                            "',RH_ADD = '" + RH.rh_add +
                                                            "',READING_DT = '" + RH.data_entered_date +
                                                            "',READING_BY = '" + RH.data_entered_by +
                                                            "',DE_BY = '" + '$' + RH.data_entered_by +
                                                            "',DE_AT = '" + RH.data_entered_date +
                                                            "',LAST_RH_ADD = '" + RH.last_rh_add +
                                                            "',LAST_READING_DT = '" + RH.last_data_entered_date +
                                                            "',LAST_READING_BY = '" + RH.last_data_entered_by +
                                                            "',MAX_PER_DAY = '" + maxPerDay +
                                                            "',AVG_PER_DAY = '" + avgPerDay +
                                                "' WHERE   EQ_CODE   ='" + RH.eq_code + "'";
                    DBOperations UI = new DBOperations();
                    int result = UI.OperationsOnSourceDB(qry);
                }
              
            }
            catch (Exception exc)
            {

            }
        }
        /*Get equipment details w.r.t EQ_CODE*/
        public long GetEquipmentDetails(string EQCode)
        {
            long rowCount = 0;
            try
            {
                string qry = @"SELECT *
                                      FROM PMS.RH_ENTRY
                                      WHERE UPDFLAG<>'D' AND EQ_CODE='" + EQCode + "'";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                rowCount = db.RowCount();
            }
            catch (Exception exc)
            {

            }
            return rowCount;
        }

        /*Insert or update Running Hour in running hour related tables*/
        public void SaveRHHistory(RunningHour RH)
        {
            try
            {
                string qry = @"INSERT INTO PMS.PMS.RH_ENTRY_LOG (VSLCODE, 
                                                                EQ_CODE, 
                                                                RH_PREVIOUS, 
                                                                RH_ADD,
                                                                READING_DT,
                                                                READING_BY,
                                                                DE_BY,
                                                                DE_AT,
                                                                UPDFLAG,
                                                                LAST_RH_ADD,
                                                                LAST_READING_DT,
                                                                LAST_READING_BY)
                                VALUES   ('" + RH.vessel_code +
                                            "','" + RH.eq_code +
                                            "','" + RH.rh_previous +
                                            "','" + RH.rh_add +
                                            "','" + RH.data_entered_date +
                                            "','" + RH.data_entered_by +
                                            "','" + '$' + RH.data_entered_by +
                                            "','" + RH.data_entered_date +
                                            "','" + 'C' +
                                            "','" + RH.last_rh_add +
                                            "','" + RH.last_data_entered_date +
                                            "','" + RH.last_data_entered_by +"')";
                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry);
              
            }
            catch (Exception exc)
            {

            }
        }
        /*Update current RH in Equipment table*/
        public void SetRHInEquipment(RunningHour RH)
        {
            try
            {
                int result;
                string parentQry = @"UPDATE PURCHASE.EQ_MF SET 
                                                   RH_PRESENT = '" + RH.rh_current +
                                                   "' WHERE VSLCODE = '" + RH.vessel_code + "' AND EQ_CODE = '" + RH.eq_code + "'";
                DBOperations dbO = new DBOperations();
                result = dbO.OperationsOnSourceDB(parentQry);

                string childEqQry = @"UPDATE PURCHASE.EQ_MF SET 
                                                   RH_PRESENT = '" + RH.rh_current +
                                                    "' WHERE VSLCODE = '"+ RH.vessel_code+ "' AND INH_RUNHRS_EQ = '" + RH.eq_code + "'";
                result = dbO.OperationsOnSourceDB(childEqQry);

            }catch(Exception exc)
            {

            }
        }

        public DataRow GetStatusCode()
        {
            DataRow dr = null;
            try
            {
                string qry = @"SELECT *
                                      FROM PMS.STATUS_MF
                                      WHERE DOC_TYPE = 'JO' AND PROG_CODE='NS' AND UPDFLAG <>'D'";
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                dr = db.GetRow();
                
            }
            catch (Exception exc)
            {

            }
                return dr;
        }
        /*Update current RH in Equipment table*/
        public void SetJobOrder(RunningHour RH)
        {
            try
            {
                DataRow dr = GetStatusCode();
                string stCode = dr["ST_CODE"].ToString();

                string qry = @"SELECT JO_CODE,JP_CODE,PLAN_DUE_HRS
                                      FROM PMS.JOB_ORDER
                                      WHERE VSLCODE = '"+RH.vessel_code+"' AND FQ_TYPE ='H' AND PLAN_DUE_HRS >=0 AND JO_ST_CODE ='"+stCode+
                                      "' AND UPDFLAG <> 'D' AND EQ_CODE IN (SELECT EQ_CODE FROM PURCHASE.EQ_MF WHERE VSLCODE = '"+RH.vessel_code+"' AND(EQ_CODE = '"+RH.eq_code+"'OR INH_RUNHRS_EQ = '"+RH.eq_code+"') AND UPDFLAG <> 'D')";

                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();

                foreach (DataRow row in tbl.Rows)
                {
                    decimal planDueHrs = decimal.Parse(row["PLAN_DUE_HRS"].ToString(), CultureInfo.InvariantCulture);
                    decimal roundOff = ((planDueHrs - RH.rh_current) / RH.avg_per_day);
                    TimeSpan ts = TimeSpan.FromHours(Decimal.ToDouble(roundOff));

                    DateTime dt = DateTime.ParseExact(RH.data_entered_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    
                    DateTime readingAt = dt + ts;

                    int result;
                    string parentQry = @"UPDATE PMS.JOB_ORDER SET 
                                                RHPLAN_DUE_DT = '" + RH.rh_current +
                                                "',PLAN_DUE_DATE = '"+ RH.rh_current +
                                                "',JO_START_DT = '" + RH.rh_current +
                                                "',DE_AT = '" + RH.data_entered_date +
                                                "',DE_BY =$'" + RH.data_entered_by +
                                                "',UPDFLAG = 'C' WHERE VSLCODE = '" + RH.vessel_code + "' AND JO_CODE = '" + row["JO_CODE"].ToString();
                    DBOperations dbO = new DBOperations();
                    result = dbO.OperationsOnSourceDB(parentQry);


                    //                    UPDATE pms.job_plan
                    //SET     FQ_LENGTH = CFQ_LENGTH / :frmRHEntry.tblRH.colAvgPerDay,
                    //       	next_due_date = :frmRHEntry.dtReadingAt ,
                    //	de_at = SYSDATETIME,
                    //	de_by = :strUser
                    //WHERE                jp_code =            :frmRHEntry.strJP_Code
                    //And                        vslcode =             :frmRHEntry.strVslCode ")


                    string jobPlanQry = @"UPDATE PMS.JOB_PLAN SET 
                                                CFQ_LENGTH = '" + RH.rh_current +
                                                "',NEXT_DUE_DATE = '" + readingAt +
                                                "',DE_AT = '" + RH.data_entered_date +
                                                "',DE_BY =$'" + RH.data_entered_by +
                                                "'WHERE VSLCODE = '" + RH.vessel_code + "' AND JP_CODE = '" + row["JP_CODE"].ToString();
                    result = dbO.OperationsOnSourceDB(jobPlanQry);

                }
            }
            catch (Exception exc)
            {

            }
        }


    }
}
