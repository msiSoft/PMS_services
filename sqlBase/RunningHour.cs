using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace sqlBase
{
    public class RunningHour
    {
        public string vessel_code { get; set; }
        public string eq_code { get; set; }
        public string eq_name { get; set; }
        public decimal rh_add { get; set; }
        public decimal rh_previous { get; set; }
        public decimal rh_current { get; set; }
        public string data_entered_date { get; set; }
        public string data_entered_by { get; set; }

        public string last_rh_add { get; set; }
        public string last_data_entered_date { get; set; }
        public string last_data_entered_by { get; set; }

        public decimal avg_per_day{ get; set; }
        public decimal max_per_day { get; set; }


        /*Insert or update Running Hour in running hour related tables*/
        public void SetRH(RunningHour RH)
        {
            try
            {
                decimal avgPerDay = SaveInRHEntry(RH);
                SetRHInEquipment(RH);
                SetJobOrder(RH, avgPerDay);
            }
            catch (Exception exc)
            {

            }
        }
        /*Insert or Update values in PMS.RH_ENTRY table*/
        public decimal SaveInRHEntry(RunningHour RH)
        {
            decimal avgPerDay;

            DBOperations db = new DBOperations();
            object avgDailyHrs = db.ExecuteScalarOnSourceDB("SELECT AVG_DAILY_HRS FROM PMS.SETUP");
            decimal setUpAvgDailyHrs = Convert.ToDecimal(avgDailyHrs != null ? avgDailyHrs : 0);

            if (GetEquipmentDetails(RH.eq_code) <= 0)
            {
                avgPerDay = setUpAvgDailyHrs;
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
                                            "','" + avgPerDay +
                                            "','" + avgPerDay + "')";
                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry);
            }
            else
            {

                DateTime dataEnteredDate = DateTime.ParseExact(RH.data_entered_date, "dd-MMM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime lastDataEnteredDate = DateTime.ParseExact(RH.last_data_entered_date, "dd-MMM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                decimal diffInDate = (dataEnteredDate - lastDataEnteredDate).Days;
                avgPerDay = (RH.rh_current - RH.rh_previous) / (diffInDate);

                if (avgPerDay > 24)
                    avgPerDay = 24;
                else if (avgPerDay < setUpAvgDailyHrs)
                    avgPerDay = setUpAvgDailyHrs;


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
                                                        "',MAX_PER_DAY = '" + avgPerDay +
                                                        "',AVG_PER_DAY = '" + avgPerDay +
                                            "' WHERE   EQ_CODE   ='" + RH.eq_code + "'";

                int result = db.OperationsOnSourceDB(qry);
            }
            return avgPerDay;
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
                                            "','" + RH.last_data_entered_by + "')";
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
                                                    "' WHERE VSLCODE = '" + RH.vessel_code + "' AND INH_RUNHRS_EQ = '" + RH.eq_code + "'";
                result = dbO.OperationsOnSourceDB(childEqQry);

            }
            catch (Exception exc)
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
        public void SetJobOrder(RunningHour RH, decimal avgPerDay)
        {
            try
            {
                DataRow dr = GetStatusCode();
                string stCode = dr["ST_CODE"].ToString();

                string qry = @"SELECT JO_CODE,JP_CODE,PLAN_DUE_HRS
                                      FROM PMS.JOB_ORDER
                                      WHERE VSLCODE = '" + RH.vessel_code + "' AND FQ_TYPE ='H' AND PLAN_DUE_HRS >=0 AND JO_ST_CODE ='" + stCode +
                                      "' AND UPDFLAG <> 'D' AND EQ_CODE IN (SELECT EQ_CODE FROM PURCHASE.EQ_MF WHERE VSLCODE = '" + RH.vessel_code + "' AND(EQ_CODE = '" + RH.eq_code + "'OR INH_RUNHRS_EQ = '" + RH.eq_code + "') AND UPDFLAG <> 'D')";

                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();

                foreach (DataRow row in tbl.Rows)
                {
                    decimal planDueHrs = decimal.Parse(row["PLAN_DUE_HRS"].ToString(), CultureInfo.InvariantCulture);
                    decimal roundOff = ((planDueHrs - RH.rh_current) / avgPerDay);
                    TimeSpan ts = TimeSpan.FromHours(Decimal.ToDouble(roundOff));

                    DateTime dt = DateTime.ParseExact(RH.data_entered_date, "dd-MMM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                    DateTime readingAt = dt + ts;

                    int result;
                    string parentQry = @"UPDATE PMS.JOB_ORDER SET 
                                                RHPLAN_DUE_DT = '" + readingAt +
                                                "',PLAN_DUE_DATE = '" + readingAt +
                                                "',JO_START_DT = '" + readingAt +
                                                "',DE_AT = '" + RH.data_entered_date +
                                                "',DE_BY ='$" + RH.data_entered_by +
                                                "',UPDFLAG = 'C' WHERE VSLCODE = '" + RH.vessel_code + "' AND JO_CODE = '" + row["JO_CODE"].ToString() + "'";
                    DBOperations dbO = new DBOperations();
                    result = dbO.OperationsOnSourceDB(parentQry);


                    decimal cfqLength = Convert.ToDecimal(dbO.ExecuteScalarOnSourceDB("SELECT CFQ_LENGTH FROM PMS.JOB_PLAN"));
                    decimal fqLength = cfqLength / avgPerDay;

                    string jobPlanQry = @"UPDATE PMS.JOB_PLAN SET 
                                                FQ_LENGTH = '" + fqLength +
                                                "',NEXT_DUE_DATE = '" + readingAt +
                                                "',DE_AT = '" + RH.data_entered_date +
                                                "',DE_BY ='$" + RH.data_entered_by +
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
