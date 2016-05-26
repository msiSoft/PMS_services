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
        public string jo_assigned_to { get; set; }
        public string jo_status_code { get; set; }
        public string data_entered_by { get; set; }
        public string data_entered_date { get; set; }
        public string file_code { get; set; }
        public string file { get; set; }
        public string type { get; set; }
        public string jp_code { get; set; }
        public string vessel_code { get; set; }

        DBOperations sq = new DBOperations();

        //Inserting into job_order table
        public void BreakdownJobsInsPJO(BreakdownJobs BreakdownJobs)

        {
            try
            {
                string qry = @"INSERT INTO   PMS.JOB_ORDER (         JO_CODE,
                                                                     JP_CODE,
                                                                    JO_TITLE,	
                                                                     JO_DESC,
                                                                  JO_ST_CODE,
                                                                 JO_START_DT,
                                                               JO_ASSIGNEDTO,
                                                                       DE_BY,		
                                                                      DE_AT )
                                     VALUES   ('"   + BreakdownJobs.jo_code +
                                               "'," + BreakdownJobs.jp_code +
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

        //Insert to file_storage
        public void BreakdownJobsInsPFS(BreakdownJobs BreakdownJobs)

        {
            try
            {
                string qry = @"INSERT INTO   PMS.FILE_STORAGE (      FILE_CODE,
	                                                                  REF_CODE,
	                                                                  DOC_TYPE,
	                                                                 FILE_TYPE,
	                                                                  FILENAME,
	                                                                     DE_BY,
	                                                                     DE_AT,
	                                                                   UPDFLAG,
	                                                               SORT_ORDER )
                                     VALUES    ( '" + BreakdownJobs.file_code +
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

        //Updating to job_order table
        public void BreakdownJobdUpdPJO(BreakdownJobs BreakdownJobs)
        {
            try
            {
                string qry = @"UPDTAE PMS.JOB_ORDER  SET        DRAWING_EXIST   =  '1', 
                                                                UPDFLAG         =  'C'  
                                                     WHERE      JO_CODE         =  '" + BreakdownJobs.jo_code +
                                                     "' AND     UPDFLAG         <> 'D'";
                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry);
            }
            catch (Exception exc)
            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

       //Updating lastcodes table
        public void BreakdownJobsUpdPL(BreakdownJobs BreakdownJobs)

        {
            try
            {
                /*TRANS_NO*/
                //To get values for trans_no calculations     
                object trans_noL = sq.ExecuteScalarOnSourceDB(" SELECT TRANS_NO FROM PURCHASE.LASTCODES WHERE P_VSLCODE = 'COMMON'  ");
                object codeprefix = sq.ExecuteScalarOnSourceDB("SELECT  CODE_PREFIX from PURCHASE.SETUP");
                string trans_no = codeprefix + "." + "0000000000" + trans_noL;

                string qry = @"UPDATE PURCHASE.LASTCODES  SET        TRANS_NO      =   '" + trans_no +
                                                          "' WHERE   P_VSLCODE     =   'COMMON'";
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
