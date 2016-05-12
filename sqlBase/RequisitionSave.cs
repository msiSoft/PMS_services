using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sqlBase
{
    class RequisitionSave
    {
        public string aid_no { get; set; }
        public string id_number { get; set; }
        public string eq_number { get; set; }
        public string zone { get; set; }
        public string vessel_code { get; set; }
        public string item_code { get; set; }
        public string required_qty { get; set; }
        public string deliver_before { get; set; }
        public string updflag { get; set; }
        public string code_type { get; set; }
        public string cc_code { get; set; }
        public string ccm_code { get; set; }
        public string rob_qty { get; set; }
        public string order_no { get; set; }
        public string requisition_number { get; set; }
        public string requisition_date { get; set; }
        public string id_level { get; set; }
        public string id_reqby { get; set; }
        public string forw_flag { get; set; }
        public string po_no { get; set; }
        public string send { get; set; }
        public string tp_code { get; set; }
        public string im_category { get; set; }
        public string dept_code { get; set; }
        public string attachment { get; set; }
        public string fromoff { get; set; }




        public void SaveRequisitionUpd(RequisitionSave Requisition)

        {
            try
            {
                string qry = @"UPDATE PURCHASE.LASTCODES SET    AID_NO  =   '" + Requisition.aid_no +
                                                            "', ID_NO   ='" + Requisition.id_number +
                                                     "' WHERE   EQ_NO   ='" + Requisition.eq_number + "'";
                Update_Insert UI = new Update_Insert();
                int result = UI.OperationsOnSourcecDB(qry);
            }
            catch (Exception exc)
            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public void SaveRequisitionInsrtDt(RequisitionSave Requisition)

        {
            try
            {
                string qry = @"INSERT INTO   PURCHASE.IND_DT (      ZONE, 
                                                                 VSLCODE, 
                                                                   ID_NO, 
                                                                 IM_CODE, 
                                                                 REQ_QTY, 
                                                                DLV_BEFR, 
                                                                 CC_CODE,  
                                                                CCM_CODE, 
                                                                 ROB_QTY, 
                                                                 UPDFLAG, 
                                                               CODE_TYPE, 
                                                                 ORDER_NO )
                                      VALUES   (  '" + Requisition.zone + 
                                        "','" + Requisition.vessel_code + 
                                          "','" + Requisition.id_number + 
                                          "','" + Requisition.item_code + 
                                       "','" + Requisition.required_qty +
                                     "','" + Requisition.deliver_before + 
                                            "','" + Requisition.cc_code + 
                                           "','" + Requisition.ccm_code + 
                                            "','" + Requisition.rob_qty + 
                                            "','" + Requisition.updflag + 
                                          "','" + Requisition.code_type + 
                                                       "','" + order_no + "')";
                Update_Insert UI = new Update_Insert();
                int result = UI.OperationsOnSourcecDB(qry);


            }
            catch (Exception exc)
            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }

        public void SaveRequisitionInsrtHd(RequisitionSave Requisition)

        {
            try
            {
                   string qry = @"INSERT INTO   PURCHASE.IND_HD (                   ZONE,	   
                                                                                   ID_NO,
                                                                                 VSLCODE,
                                                                                  CID_NO,      
                                                                                 ID_DATE, 
                                                                               CODE_TYPE,
                                                                                ID_LEVEL,
                                                                                ID_REQBY,
                                                                                 UPDFLAG,
                                                                               FORW_FLAG,
                                                                                   PO_NO,
                                                                                   EQ_NO,
                                                                                 CC_CODE,
                                                                                CCM_CODE,
                                                                                    SEND,
                                                                                 TP_CODE,
                                                                             IM_CATEGORY,
                                                                               DEPT_CODE,
                                                                              ATTACHMENT,
                                                                                  FROMOFF )
                                       VALUES           (         '" + Requisition.zone +
                                                          "','" + Requisition.id_number +
                                                      "','" + Requisition.vessel_code + 
                                                 "','" + Requisition.requisition_number + 
                                                 "','" + Requisition.requisition_date +
                                                          "','" + Requisition.code_type +
                                                           "','" + Requisition.id_level +
                                                           "','" + Requisition.id_reqby +
                                                            "','" + Requisition.updflag +
                                                          "','" + Requisition.forw_flag +
                                                              "','" + Requisition.po_no +
                                                          "','" + Requisition.eq_number +
                                                            "','" + Requisition.cc_code +
                                                           "','" + Requisition.ccm_code +
                                                               "','" + Requisition.send +
                                                            "','" + Requisition.tp_code +
                                                        "','" + Requisition.im_category +
                                                          "','" + Requisition.dept_code +
                                                         "','" + Requisition.attachment +
                                                            "','" + Requisition.fromoff + "')";
                Update_Insert UI = new Update_Insert();
                int result = UI.OperationsOnSourcecDB(qry);


            }
            catch (Exception exc)
            {
                Console.WriteLine("{ 0} Exception caught.", exc);
            }
        }


    }
}
