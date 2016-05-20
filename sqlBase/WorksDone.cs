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
        public string next_due_date     { get; set; }
        public string last_done_date    { get; set; }
        public char   fq_type           { get; set; }
        public string fq_code           { get; set; }
        public string jq_code           { get; set; }
        public string updflag           { get; set; }
        public string next_due_hrs      { get; set; }
        public string last_done_hrs     { get; set; }
        public string cjo_code          { get; set; }
        public string jo_title          { get; set; }
        public string jo_description    { get; set; }
        public string condition_before  { get; set; }
        public string condition_after   { get; set; }
        public string jo_status_code    { get; set; }
        public string jo_start_date     { get; set; }
        public string jo_end_date       { get; set; }
        public string jo_assigned_to    { get; set; }
        public string resp_crew_name    { get; set; }
        public string data_entered_by   { get; set; }
        public string data_entered_date { get; set; }
        public string jo_code           { get; set; }
        public string qty_consumed      { get; set; }
        public string code_type         { get; set; }
        public string file_code         { get; set; }
        public string doc_type          { get; set; }
        public string vessel_code       { get; set; }
        public string item_code         { get; set; }
        public string rob_qty           { get; set; }
        public string total_out         { get; set; }
        public string trans_no          { get; set; }
        public string ctrans_no         { get; set; }
        public string trans_type_code   { get; set; }
        public string dept_code         { get; set; }
        public string eq_code           { get; set; }
        public string trans_qty         { get; set; }
        public string plan_qty          { get; set; }
        public string jp_code           { get; set; }
        public string jo_catst_code     { get; set; }
        public string ra_required       { get; set; }
        public string jb_class_code     { get; set; }
        public string jb_type_code      { get; set; }
        public string priority_st_code  { get; set; }
        public string rankcode          { get; set; }
        public string jo_proc           { get; set; }
        public string est_duration      { get; set; }
        public string plan_due_date     { get; set; }
        public string plan_due_hrs      { get; set; }
        public string jo_duration_hrs   { get; set; }
        public string jo_done_hrs       { get; set; }
        public string jo_estend_dt      { get; set; }
        public string fq_length         { get; set; }
        public string cfq_length        { get; set; }
        public string jb_code           { get; set; }
        public string jo_org_date       { get; set; }
        public string jo_org_hrs        { get; set; }

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


                if (tbl.Rows[0]["FQ_TYPE"].ToString() == "C")

                {                          
                    string qry = @"UPDATE PMS.JOB_PLAN  SET       NEXT_DUE_DATE   = '" + WorksDone.next_due_date +
                                                              "', LAST_DONE_DATE  = '" + WorksDone.jo_end_date +
                                                              "', UPDFLAG         = '" + WorksDone.updflag +
                                                        "' WHERE  JP_CODE         = '" + WorksDone.jp_code + "'";
                    DBOperations UI = new DBOperations();
                    int result = UI.OperationsOnSourceDB(qry);
                }

                else if (tbl.Rows[0]["FQ_TYPE"].ToString() == "H")

                {
                    string qry = @"UPDATE PMS.JOB_PLAN  SET       NEXT_DUE_DATE   = '" + WorksDone.next_due_date +
                                                              "', LAST_DONE_DATE  = '" + WorksDone.last_done_date +
                                                              "', NEXT_DUE_HRS    = '" + WorksDone.next_due_hrs +
                                                              "', LAST_DONE_HRS   = '" + WorksDone.last_done_hrs +
                                                              "', UPDFLAG         = '" + WorksDone.updflag + 
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
                string qry = @"UPDATE PMS.JOB_ORDER SET               CJO_CODE              =   '" + WorksDone.cjo_code +
                                                      "',             JO_TITLE              =   '" + WorksDone.jo_title +
                                                      "',             JO_DESC	            =   '" + WorksDone.jo_description +
                                                      "',             CONDITION_BEFORE      =   '" + WorksDone.condition_before +
                                                      "',             CONDITION_AFTER	    =   '" + WorksDone.condition_after +
                                                      "',             JO_ST_CODE            =   '" + WorksDone.jo_status_code +
                                                      "',             JO_START_DT           =   '" + WorksDone.jo_start_date +
                                                      "',             JO_END_DT             =   '" + WorksDone.jo_end_date +
                                                      "',             JO_ASSIGNEDTO         =   '" + WorksDone.jo_assigned_to +
                                                      "',             RESP_CREW_NAME        =   '" + WorksDone.resp_crew_name +
                                                      "',             DE_BY                 =   '" + WorksDone.data_entered_by +
                                                        "',           DE_AT                 =   '" + WorksDone.data_entered_date +
                                                     "' WHERE         JO_CODE               =   '" + WorksDone.jo_code + "'";
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
                                                                           DE_BY, 
                                                                           DE_AT  )
                                     VALUES            ('"    + WorksDone.file_code +
                                                        "',"  + WorksDone.jo_code +
                                                        "' , 'JO' , 'A' , '"
                                                              + WorksDone.vessel_code +
                                                        "',"  + WorksDone.data_entered_by +
                                                        "',"  + WorksDone.data_entered_date + "')";
                                                        
                DBOperations UI = new DBOperations();
                //int result = UI.OperationsOnSourceDB(qry, SQLBaseDB.DBIMAGE);
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
                                                                                     )
                                     VALUES                ('"   + WorksDone.jo_code +
                                                        "'," + WorksDone.item_code +
                                                        "'," + WorksDone.code_type +
                                                        "'," + WorksDone.qty_consumed +
                                                         "'," + WorksDone.vessel_code +
                                                        "'," + WorksDone.data_entered_by +
                                                        "'," + WorksDone.data_entered_date + "')";

                DBOperations UI = new DBOperations();
                //int result = UI.OperationsOnSourceDB(qry, SQLBaseDB.DBIMAGE);
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
                string qry = @"UPDATE PURCHASE.STOCK SET               ROB_QTY              =   '" + WorksDone.rob_qty +
                                                      "',             TOTAL_OUT              =   '" + WorksDone.total_out +
                                                      "',             DE_BY                 =   '" + WorksDone.data_entered_by +
                                                        "',           DE_AT                 =   '" + WorksDone.data_entered_date +
                                                     "' WHERE        IM_CODE               =   '" + WorksDone.item_code+ "'";
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
                string qry = @"UPDATE PURCHASE.LASTCODES  SET        TRANS_NO      =   '" + WorksDone.trans_no +
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
                                                        "'," + WorksDone.trans_no +
                                                        "'," + WorksDone.ctrans_no +
                                                        "'," + WorksDone.trans_type_code +
                                                         "'," + WorksDone.dept_code +
                                                           "'," + WorksDone.eq_code +
                                                        "'," + WorksDone.data_entered_by +
                                                        "'," + WorksDone.data_entered_date + "')";

                DBOperations UI = new DBOperations();
                //int result = UI.OperationsOnSourceDB(qry, SQLBaseDB.DBIMAGE);
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
                                                        "'," + WorksDone.trans_no +
                                                        "'," + WorksDone.item_code +
                                                        "'," + WorksDone.code_type +
                                                         "'," + WorksDone.trans_qty +
                                                         "'," + WorksDone.plan_qty +
                                                        "'," + WorksDone.data_entered_by +
                                                        "'," + WorksDone.data_entered_date + "')";

                DBOperations UI = new DBOperations();
                //int result = UI.OperationsOnSourceDB(qry, SQLBaseDB.DBIMAGE);
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
        string qry = @"INSERT INTO   PMS.JOB_ORDER (                JO_CODE,
                                                                    CJO_CODE,
                                                                    VSLCODE,
                                                                    EQ_CODE,
                                                                    JP_CODE,
                                                                    JO_CATST_CODE,
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
                                                                    RA_REQUIRED
                                                                        )
                                     VALUES            (  '" + WorksDone.jo_code +
                                                        "'," + WorksDone.cjo_code +
                                                        "'," + WorksDone.vessel_code +
                                                        "'," + WorksDone.eq_code +
                                                        "'," + WorksDone.jp_code +
                                                        "'," + WorksDone.jo_catst_code +
                                                        "'," + WorksDone.jb_class_code +
                                                        "'," + WorksDone.jb_type_code +
                                                        "'," + WorksDone.priority_st_code +
                                                        "'," + WorksDone.jo_title +
                                                        "'," + WorksDone.jo_description +
                                                        "'," + WorksDone.jo_status_code +
                                                        "'," + WorksDone.jo_start_date +
                                                        "'," + WorksDone.jo_end_date +
                                                        "'," + WorksDone.dept_code +
                                                        "'," + WorksDone.jo_assigned_to +
                                                        "'," + WorksDone.rankcode +
                                                        "'," + WorksDone.resp_crew_name +
                                                        "'," + WorksDone.jo_proc +
                                                        "'," + WorksDone.est_duration +
                                                        "'," + WorksDone.plan_due_date +
                                                        "'," + WorksDone.plan_due_hrs +
                                                        "'," + WorksDone.jo_duration_hrs +
                                                        "'," + WorksDone.jo_done_hrs +
                                                        "'," + WorksDone.data_entered_by +
                                                        "'," + WorksDone.data_entered_date +
                                                        "'," + WorksDone.updflag +
                                                        "'," + WorksDone.jo_estend_dt +
                                                        "'," + WorksDone.fq_length +
                                                        "'," + WorksDone.fq_code +
                                                        "'," + WorksDone.cfq_length +
                                                        "'," + WorksDone.fq_type +
                                                        "'," + WorksDone.jb_code +
                                                        "'," + WorksDone.last_done_date +
                                                        "'," + WorksDone.jo_org_date +
                                                        "'," + WorksDone.jo_org_hrs +
                                                        "'," + WorksDone.ra_required + "')";

                DBOperations UI = new DBOperations();
                //int result = UI.OperationsOnSourceDB(qry, SQLBaseDB.DBIMAGE);
            }
            catch (Exception exc)

            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public void WorksDonesCalculation(string fq_name, decimal cfq_length, string jp_code)
        {
            switch (fq_name)
            {
                case "YEAR":
                    break;
                case "DAY":
                    break;
                case "WEEK":
                    break;
                case "MONTH":
                    break;
            }
            return;
        }

    }
}
