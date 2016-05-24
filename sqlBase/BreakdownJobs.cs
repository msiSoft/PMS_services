using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sqlBase
{
    class BreakdownJobs
    {
        public string next_due_date { get; set; }
        public string last_done_date { get; set; }
        public char fq_type { get; set; }
        public string fq_code { get; set; }
        public string jq_code { get; set; }
        public string updflag { get; set; }
        public string next_due_hrs { get; set; }
        public string last_done_hrs { get; set; }
        public string cjo_code { get; set; }
        public string jo_title { get; set; }
        public string jo_description { get; set; }
        public string condition_before { get; set; }
        public string condition_after { get; set; }
        public string jo_status_code { get; set; }
        public string jo_start_date { get; set; }
        public string jo_end_date { get; set; }
        public string jo_assigned_to { get; set; }
        public string resp_crew_name { get; set; }
        public string data_entered_by { get; set; }
        public string data_entered_date { get; set; }
        public string jo_code { get; set; }
        public string qty_consumed { get; set; }
        public string code_type { get; set; }
        public string file_code { get; set; }
        public string doc_type { get; set; }
        public string vessel_code { get; set; }
        public string item_code { get; set; }
        public string rob_qty { get; set; }
        public string total_out { get; set; }
        public string trans_no { get; set; }
        public string ctrans_no { get; set; }
        public string trans_type_code { get; set; }
        public string dept_code { get; set; }
        public string eq_code { get; set; }
        public string trans_qty { get; set; }
        public string plan_qty { get; set; }
        public string jp_code { get; set; }
        public string jo_catst_code { get; set; }
        public string ra_required { get; set; }
        public string jb_class_code { get; set; }
        public string jb_type_code { get; set; }
        public string priority_st_code { get; set; }
        public string rankcode { get; set; }
        public string jo_proc { get; set; }
        public string est_duration { get; set; }
        public string plan_due_date { get; set; }
        public string plan_due_hrs { get; set; }
        public string jo_duration_hrs { get; set; }
        public string jo_done_hrs { get; set; }
        public string jo_estend_dt { get; set; }
        public string fq_length { get; set; }
        public string cfq_length { get; set; }
        public string jb_code { get; set; }
        public string jo_org_date { get; set; }
        public string jo_org_hrs { get; set; }
        public string fq_name { get; set; }
        public string priority_code { get; set; }
        public string file { get; set; }

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
                                                                "'," + BreakdownJobs.priority_st_code +
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
                string qry = @"UPDATE PURCHASE.LASTCODES  SET        TRANS_NO      =   '" + BreakdownJobs.trans_no +
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
