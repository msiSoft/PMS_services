using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sqlBase.Classes;
using System.Data;

namespace sqlBase
{
    class WorksDone
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

        public void WorksDoneUpdJP (WorksDone WorksDone)

        {
            try
            {
                string qrry = @"SELECT            FREQ_MF.FQ_TYPE as      FQ_TYPE, 
                                               JOB_PLAN.FQ_LENGTH as    FQ_LENGTH, 
                                           JOB_PLAN.FQ_LENGTH_HRS as FQ_LENGTH_HR,
		                                          FREQ_MF.FQ_NAME as      FQ_NAME, 
                                              JOB_PLAN.CFQ_LENGTH as   CFQ_LENGTH, 
                                                FREQ_MF.PROG_CODE as    PROG_CODE,
                                                 JOB_PLAN.FQ_CODE as       FQ_CODE
                                                                              FROM   PMS.JOB_PLAN , PMS.FREQ_MF
                                                                             WHERE   JOB_PLAN.FQ_CODE  = FREQ_MF.FQ_CODE
                                                                               AND   JP_CODE ='" + WorksDone.jp_code +
                                                                            "' AND   FREQ_MF.UPDFLAG   <> 'D'" +
                                                                             " AND   JOB_PLAN.UPDFLAG  <> 'D'";
                SqlBase_OleDb db = new SqlBase_OleDb(qrry);
                DataTable tbl = db.GetTable();
                decimal cfq_length = Convert.ToDecimal(tbl.Rows[0]["CFQ_LENGTH"]);
                string fq_name = tbl.Rows[0]["FQ_NAME"].ToString();
                string jp_code = WorksDone.jp_code;
               

                if (tbl.Rows[0]["FQ_TYPE"].ToString() == "C")
                {
                  string res = WorksDonesCalculation(fq_name, cfq_length, jp_code).ToString();

                    string qry = @"UPDATE PMS.JOB_PLAN  SET       NEXT_DUE_DATE   = '" + res +
                                                              "', LAST_DONE_DATE  = '" + WorksDone.jo_end_date +
                                                              "', UPDFLAG         = 'C'" +
                                                        "' WHERE  JP_CODE         = '" + WorksDone.jp_code + "'";
                    DBOperations UI = new DBOperations();
                    int result = UI.OperationsOnSourceDB(qry);
                }
                else if (tbl.Rows[0]["FQ_TYPE"].ToString() == "H")
                {
                    string res = cfq_length + jo_end_date;
                    object dfStartAt = sq.ExecuteScalarOnSourceDB("SELECT  jo_done_hrs from PMS.job_order WHERE jp_code ='"+ WorksDone.jp_code +"'");
                    object nFqLengthHrs = sq.ExecuteScalarOnSourceDB("SELECT  FQ_LENGTH_HRS from PMS.job_plan WHERE jp_code ='" + WorksDone.jp_code + "'");
                    decimal next_due_hrs = Convert.ToDecimal(dfStartAt) + Convert.ToDecimal(nFqLengthHrs);
                    string qry = @"UPDATE PMS.JOB_PLAN  SET       NEXT_DUE_DATE   = '" + res +
                                                              "', LAST_DONE_DATE  = '" + WorksDone.jo_end_date +
                                                              "', NEXT_DUE_HRS    = '" + next_due_hrs +
                                                              "', LAST_DONE_HRS   = '" + WorksDone.jo_end_date +
                                                              "', UPDFLAG         = 'C'" +
                                                        "' WHERE  JP_CODE         = '" + WorksDone.jp_code + "'";
                    DBOperations UI = new DBOperations();
                    int result = UI.OperationsOnSourceDB(qry);
                }                           
            }
            catch (Exception exc)
            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public void WorksDoneUpdJO(WorksDone WorksDone)

        {
            try

            {
                string qry = @"UPDATE PMS.JOB_ORDER   SET             CJO_CODE              =   '" + WorksDone.cjo_code +
                                                      "',             JP_CODE               =   '" + WorksDone.jp_code +
                                                      "',             PRIORITY_ST_CODE      =   '" + WorksDone.jp_code +
                                                      "',             JO_TITLE              =   '" + WorksDone.jo_title +
                                                      "',             JO_DESC	            =   '" + WorksDone.jo_description +
                                                      "',             CONDITION_BEFORE      =   '" + WorksDone.condition_before +
                                                      "',             CONDITION_AFTER	    =   '" + WorksDone.condition_after +
                                                      "',             JO_ST_CODE            =   '" + WorksDone.jo_status_code +
                                                      "',             JO_START_DT           =   '" + WorksDone.jo_start_date +
                                                      "',             JO_END_DT             =   '" + WorksDone.jo_end_date +
                                                      "',             PLAN_DUE_DATE         =   '" + WorksDone.jo_start_date +
                                                      "',             JO_ESTEND_DT          =   '" + WorksDone.jo_end_date +
                                                      "',             JO_ASSIGNEDTO         =   '" + WorksDone.jo_assigned_to +
                                                      "',             RESP_CREW_NAME        =   '" + WorksDone.resp_crew_name +
                                                      "',             DE_BY                 =   '" + WorksDone.data_entered_by +
                                                      "',             DE_AT                 =   '" + WorksDone.data_entered_date +
                                                      "' WHERE        JO_CODE               =   '" + WorksDone.jo_code +
                                                      "' AND          JP_CODE               =   '" + WorksDone.jp_code + "'";
                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry);
            }
            catch (Exception exc)
            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public void WorksDoneInsFS(WorksDone WorksDone)

        {
            try

            {
                string qry = @"INSERT INTO   PMS.FILE_STORAGE (        FILE_CODE, 
                                                                        REF_CODE, 
                                                                        DOC_TYPE, 
                                                                       FILE_TYPE, 
                                                                         VSLCODE,
                                                                        FILENAME, 
                                                                           DE_BY, 
                                                                           DE_AT,
                                                                        UPDFLAG)
                                     VALUES            ('" + WorksDone.file_code +
                                                        "'," + WorksDone.jo_code +
                                                        "','JO', 'A', '" + WorksDone.vessel_code +
                                                        "'," + WorksDone.file +
                                                        "'," + WorksDone.data_entered_by +
                                                        "'," + WorksDone.data_entered_date +
                                                        "', 'C')";

                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry, SQLBaseDB.DBIMAGE);
            }
            catch (Exception exc)

            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public void WorksDoneInsJS(WorksDone WorksDone)

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
                                     VALUES                ('" + WorksDone.jo_code +
                                                        "'," + WorksDone.item_code +
                                                        "'," + WorksDone.code_type +
                                                        "'," + WorksDone.qty_consumed +
                                                         "'," + WorksDone.vessel_code +
                                                        "'," + WorksDone.data_entered_by +
                                                        "'," + WorksDone.data_entered_date +
                                                        "', 'C')";

                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry);
            }
            catch (Exception exc)

            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public void WorksDoneUpdPS(WorksDone WorksDone)

        {
            try

            {
                object rob_qtyT = sq.ExecuteScalarOnSourceDB("SELECT  ROB_QTY from PURCHASE.STOCK");

                object total_outT = sq.ExecuteScalarOnSourceDB("SELECT  TOTAL_OUT from PURCHASE.STOCK");

                decimal rob_qty = Convert.ToDecimal(rob_qtyT) - Convert.ToDecimal(WorksDone.qty_consumed) ;
                decimal total_out = Convert.ToDecimal(total_outT) + Convert.ToDecimal(WorksDone.qty_consumed);
                string qry = @"UPDATE PURCHASE.STOCK SET               ROB_QTY              =   '" + rob_qty +
                                                      "',             TOTAL_OUT              =   '" + total_out +
                                                      "',             DE_BY                 =   '" + WorksDone.data_entered_by +
                                                        "',           DE_AT                 =   '" + WorksDone.data_entered_date +
                                                     "' WHERE        IM_CODE               =   '" + WorksDone.item_code + "'";
                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry);
            }
            catch (Exception exc)

            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public void WorksDoneUpdPL(WorksDone WorksDone)

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

        public void WorksDoneInsPTHD(WorksDone WorksDone)

        {
            try

            {
                object trans_noL = sq.ExecuteScalarOnSourceDB(" SELECT TRANS_NO FROM PURCHASE.LASTCODES WHERE P_VSLCODE = 'COMMON'  ");

                object codeprefix = sq.ExecuteScalarOnSourceDB("SELECT  CODE_PREFIX from PURCHASE.SETUP");

                string trans_no = codeprefix + "." + "0000000000" + trans_noL;
                string qry2 = @"UPDATE purchase.lastcodes SET ctrans_no = ctrans_no +1 WHERE	p_vslcode ='" + WorksDone.vessel_code + "'";
                DBOperations UI2 = new DBOperations();
                int result2 = UI2.OperationsOnSourceDB(qry2);

                object leadzero = sq.ExecuteScalarOnSourceDB("select LEAD_ZEROES from pms.setup");

                object nCLastTransNo = sq.ExecuteScalarOnSourceDB("SELECT	ctrans_no FROM	purchase.lastcodes WHERE   p_vslcode = '" + WorksDone.vessel_code + "'");

                int c = Convert.ToString(trans_noL).Length;
                string b = "0";
                int a = Convert.ToInt32(leadzero) - c;
                for (int i = 0; i <= a; i++)
                {
                    b = b + "0";
                }
                object sPrefixTR = sq.ExecuteScalarOnSourceDB("select TR_PREFIX from pms.setup");

                object sSuffixTR = sq.ExecuteScalarOnSourceDB("select TR_SUFFIX from pms.setup");

                string ctrans_no = b + sPrefixTR + nCLastTransNo + sSuffixTR;
                object trans_type_code = sq.ExecuteScalarOnSourceDB("SELECT trans_type_code FROM purchase.trans_type WHERE prog_code = 'IS' AND updflag <> 'D'");

                object dept_code = sq.ExecuteScalarOnSourceDB(" select  DEPT_CODE from purchase.department WHERE DEPT_NAME = DECK");
                string qry = @"INSERT INTO   PURCHASE.TRANS_HD (                VSL_CODE, 
                                                                            TRANS_NO, 
                                                                          CTRANS_NO, 
                                                                       TRANS_TYPE_CODE, 
                                                                            DEPT_CODE,
                                                                            EQ_CODE,
                                                                              DE_BY, 
                                                                              DE_AT,
                                                                        )
                                     VALUES            ('" + WorksDone.vessel_code +
                                                        "'," + trans_no +
                                                        "'," +ctrans_no +
                                                        "'," + trans_type_code +
                                                         "'," + dept_code +
                                                           "'," + WorksDone.eq_code +
                                                        "'," + WorksDone.data_entered_by +
                                                        "'," + WorksDone.data_entered_date + "')";

                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry);
            }
            catch (Exception exc)

            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public void WorksDoneInsPTDT(WorksDone WorksDone)

        {
            try

            {
                object trans_noL = sq.ExecuteScalarOnSourceDB(" SELECT TRANS_NO FROM PURCHASE.LASTCODES WHERE P_VSLCODE = 'COMMON'  ");

                object codeprefix = sq.ExecuteScalarOnSourceDB("SELECT  CODE_PREFIX from PURCHASE.SETUP");

                string trans_no = codeprefix + "." + "0000000000" + trans_noL;

                string qry = @"INSERT INTO   PURCHASE.TRANS_DT (                VSLCODE,
                                                                                TRANS_NO,
                                                                                IM_CODE,
                                                                                CODE_TYPE,
                                                                                TRANS_QTY,
                                                                                PLAN_QTY ,
                                                                                DE_BY,
                                                                                DE_AT
                                                                        )
                                     VALUES            ('" + WorksDone.vessel_code +
                                                        "'," + trans_no +
                                                        "'," + WorksDone.item_code +
                                                        "'," + WorksDone.code_type +
                                                         "'," + WorksDone.qty_consumed +
                                                         "'0','" + WorksDone.data_entered_by +
                                                        "'," + WorksDone.data_entered_date + "')";

                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry);
            }
            catch (Exception exc)

            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public void WorksDoneInsPJO(WorksDone WorksDone)

        {
            try

            {
                object jb_class_code = sq.ExecuteScalarOnSourceDB("select JB_CLASS_CODE from pms.job_class_mf");
                object jb_type_code = sq.ExecuteScalarOnSourceDB("select JB_TYPE_CODE from pms.job_type_mf");
                object dept_code = sq.ExecuteScalarOnSourceDB("select DEPT_CODE from pms.job_plan where JP_CODE =" + WorksDone.jp_code  );
                object est_duration = sq.ExecuteScalarOnSourceDB("select EST_DURATION from pms.job_plan where JP_CODE =" + WorksDone.jp_code);
                object plan_due_date = sq.ExecuteScalarOnSourceDB("select next_due_date  from pms.job_plan where JP_CODE =" + WorksDone.jp_code);
                object plan_due_hrs = sq.ExecuteScalarOnSourceDB("select next_due_hrs from pms.job_plan  where JP_CODE =" + WorksDone.jp_code);
                object fq_length = sq.ExecuteScalarOnSourceDB("select FQ_LENGTH from pms.job_plan  where JP_CODE =" + WorksDone.jp_code);
                object fq_code = sq.ExecuteScalarOnSourceDB("select FQ_CODE from pms.job_plan  where JP_CODE =" + WorksDone.jp_code);
                object cfq_length = sq.ExecuteScalarOnSourceDB("select CFQ_LENGTH from pms.job_plan  where JP_CODE =" + WorksDone.jp_code);
                object fq_type = sq.ExecuteScalarOnSourceDB("SELECT	fq_type	FROM    PMS.freq_mf WHERE   fq_code ='" + fq_code + "' AND updflag <> 'D'");
                object jb_code = sq.ExecuteScalarOnSourceDB("select JB_CODE from pms.job_plan  where JP_CODE =" + WorksDone.jp_code);
                object last_done_date = sq.ExecuteScalarOnSourceDB("select LAST_DONE_DATE from  pms.job_plan  where JP_CODE =" + WorksDone.jp_code);
                object jo_org_date = sq.ExecuteScalarOnSourceDB("select JO_ORG_DATE from  pms.job_plan  where JP_CODE =" + WorksDone.jp_code);
                object jo_org_hrs = sq.ExecuteScalarOnSourceDB("select JO_ORG_HRS from  pms.job_plan  where JP_CODE =" + WorksDone.jp_code);
              

            string qry = @"INSERT INTO   PMS.JOB_ORDER (        JO_CODE,
                                                                CJO_CODE,
                                                                VSLCODE,
                                                                EQ_CODE,
                                                                JP_CODE,
                                                                JB_CLASS_CODE,
                                                                JB_TYPE_CODE,
                                                                PRIORITY_ST_CODE,
                                                                JO_TITLE,	
                                                                JO_DESC,
                                                                JO_ST_CODE,
                                                                JO_START_DT,	
                                                                JO_END_DT,
        	                                                    DEPT_CODE,
                                                                JO_ASSIGNEDTO,
                                                                RANKCODE,
                                                                RESP_CREW_NAME,
                                                                JO_PROC,
                                                                EST_DURATION,
                                                                PLAN_DUE_DATE,	
                                                                PLAN_DUE_HRS,
                                                                JO_DURATION_HRS,
                                                                JO_DONE_HRS,
                                                                DE_BY,		
                                                                DE_AT,
                                                                UPDFLAG,	
                                                                JO_ESTEND_DT,
                                                                FQ_LENGTH,	
                                                                FQ_CODE,
                                                                CFQ_LENGTH,	
                                                                FQ_TYPE,
                                                                JB_CODE,	
                                                                 LAST_DONE_DATE,
                                                                JO_ORG_DATE ,	
                                                                JO_ORG_HRS,
                                                                RA_REQUIRED     )
                                     VALUES            (        '"   + WorksDone.jo_code +
                                                                "'," + WorksDone.cjo_code +
                                                                "'," + WorksDone.vessel_code +
                                                                "'," + WorksDone.eq_code +
                                                                "'," + WorksDone.jp_code +
                                                                "'," + jb_class_code + //first recrd from pms.job_class_mf
                                                                "'," + jb_type_code +  //first recrd from pms.job_type_mf
                                                                "'," + WorksDone.priority_code +
                                                                "'," + WorksDone.jo_title +
                                                                "'," + WorksDone.jo_description +
                                                                "'," + WorksDone.jo_status_code +
                                                                "'," + WorksDone.jo_start_date +
                                                                "'," + WorksDone.jo_end_date +
                                                                "'," + dept_code +
                                                                "','NULL','NULL','NULL','NULL','"  + est_duration +
                                                                "'," + plan_due_date +
                                                                "'," + plan_due_hrs +
                                                                "','NULL','NULL','" + WorksDone.data_entered_by +
                                                                "'," + WorksDone.data_entered_date +
                                                                "','C','NULL'," +fq_length +
                                                                "'," +fq_code +
                                                                "'," + cfq_length +
                                                                "'," +fq_type +
                                                                "'," + jb_code +
                                                                "'," + last_done_date +
                                                                "'," + jo_org_date +
                                                                "'," + jo_org_hrs +
                                                                "',N'')";

                DBOperations UI = new DBOperations();
                int result = UI.OperationsOnSourceDB(qry);
            }
            catch (Exception exc)

            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public string WorksDonesCalculation(string fq_name, decimal cfq_length, string jp_code)
        {
            string res = string.Empty;
            string strDate = jo_end_date;
            string[] arrDate = strDate.Split('-');
            string day = arrDate[0].ToString();
            string month = arrDate[1].ToString();
            string year = arrDate[2].ToString();

            switch (fq_name)

            {               
                case "YEAR":
                    var yr = cfq_length  + year;
                    res = day + "-" + month + "-" + yr;

                    break;
                case "DAY":
                   res = (cfq_length * 1) + jo_end_date;
                    break;
                case "WEEK":
                   res = (cfq_length * 7) + jo_end_date;
                    break;
                case "MONTH":
                    var mnth = cfq_length + month;
                    res = day + "-" + mnth + "-" + year;
                    break;                                    
            }
            return res;
        }

    }
}
