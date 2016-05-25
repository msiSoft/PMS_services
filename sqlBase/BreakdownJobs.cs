using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sqlBase
{
    class BreakdownJobs
    {
        public string jo_code { get; set; }
        public string cjo_code { get; set; }
        public string eq_code { get; set; }
        public string eq_name { get; set; }
        public string jo_title { get; set; }
        public string jo_description { get; set; }
        public string jo_start_date { get; set; }
        public string jo_end_date { get; set; }
        public string jo_assigned_to { get; set; }
        public string condition_before { get; set; }
        public string condition_after { get; set; }
        public string resp_crew_name { get; set; }
        public string priority_code { get; set; }
        public string jo_status_code { get; set; }
        public string data_entered_by { get; set; }
        public string data_entered_date { get; set; }
        public string item_code { get; set; }
        public string code_type { get; set; }
        public string qty_consumed { get; set; }
        public string file_code { get; set; }
        public string fq_name { get; set; }
        public string file { get; set; }
        public string type { get; set; }
        public string jp_code { get; set; }
        public string vessel_code { get; set; }

        DBOperations sq = new DBOperations();
        public void BreakdownJobsInsPJO(BreakdownJobs BreakdownJobs)

        {
            try

            {
                string qry = @"INSERT INTO   PMS.JOB_ORDER (         JO_CODE,
                                                                    JP_CODE,
                                                                    PRIORITY_ST_CODE,
                                                                    JO_TITLE,	
                                                                    JO_DESC,
                                                                    JO_ST_CODE,
                                                                    JO_START_DT,
                                                                    JO_ASSIGNEDTO,
                                                                    DE_BY,		
                                                                    DE_AT
                                                                        )
                                     VALUES            (        '"   + BreakdownJobs.jo_code +
                                                                "'," + BreakdownJobs.jp_code +
                                                                "'," + BreakdownJobs.priority_code +
                                                                "'," + BreakdownJobs.jo_title +
                                                                "'," + BreakdownJobs.jo_description +
                                                                "'," + BreakdownJobs.jo_status_code +
                                                                "'," + BreakdownJobs.jo_start_date +
                                                                "'," + BreakdownJobs.jo_assigned_to +
                                                                "'," + BreakdownJobs.data_entered_by +
                                                                "'," + BreakdownJobs.data_entered_date +"')";

                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry);
            }
            catch (Exception exc)

            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public void BreakdownJobsInsPFS(BreakdownJobs BreakdownJobs)

        {
            try

            {
                string qry = @"INSERT INTO   PMS.FILE_STORAGE (        FILE_CODE,
	                                                                REF_CODE,
	                                                                DOC_TYPE,
	                                                                FILE_TYPE,
	                                                                FILENAME,
	                                                                DE_BY,
	                                                                DE_AT,
	                                                                UPDFLAG,
	                                                                SORT_ORDER
                                                                        )
                                     VALUES            (        '" + BreakdownJobs.file_code +
                                                                "'," + BreakdownJobs.jo_code +
                                                                "','JO','A','" + BreakdownJobs.file +
                                                                "'," + BreakdownJobs.data_entered_by +
                                                                "'," + BreakdownJobs.data_entered_date + "','C','1')";

                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry, SQLBaseDB.DBIMAGE);
            }
            catch (Exception exc)

            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public void BreakdownJobdUpdPJO(BreakdownJobs BreakdownJobs)
        {
            try
            {
                string qry = @"UPDTAE PMS.JOB_ORDER  SET        DRAWING_EXIST   =  '1', 
                                                                UPDFLAG         =  'C'  
                                                 WHERE          JO_CODE         =  '" + BreakdownJobs.jo_code +
                                                 "' AND         UPDFLAG         <> 'D'";
                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry);
            }
            catch (Exception exc)
            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public void BreakdownJobsInsJS(BreakdownJobs BreakdownJobs)

        {
            try

            {
                string qry = @"INSERT INTO   PMS.JO_SPARES (                JO_CODE, 
                                                                            IM_CODE, 
                                                                          CODE_TYPE, 
                                                                       QTY_CONSUMED,
                                                                            VSLCODE, 
                                                                              DE_BY, 
                                                                              DE_AT,
                                                                             UPDFLAG      )
                                     VALUES                ('" + BreakdownJobs.jo_code +
                                                        "'," + BreakdownJobs.item_code +
                                                        "'," + BreakdownJobs.code_type +
                                                        "'," + BreakdownJobs.qty_consumed + 
                                                         "'," + BreakdownJobs.vessel_code +
                                                        "'," + BreakdownJobs.data_entered_by +
                                                        "'," + BreakdownJobs.data_entered_date +
                                                        "', 'C')";

                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry);
            }
            catch (Exception exc)

            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public void BreakdownJobsUpdPL(BreakdownJobs BreakdownJobs)

        {
            try

            {
                object trans_noL = sq.ExecuteScalarOnSourceDB(" SELECT TRANS_NO FROM PURCHASE.LASTCODES WHERE P_VSLCODE = 'COMMON'  ");

                object codeprefix = sq.ExecuteScalarOnSourceDB("SELECT  CODE_PREFIX from PURCHASE.SETUP");

                string trans_no = codeprefix + "." + "0000000000" + trans_noL;
                string qry = @"UPDATE PURCHASE.LASTCODES  SET        TRANS_NO      =   '" + trans_no +
                                                     "' WHERE        P_VSLCODE     =   'COMMON'";
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
