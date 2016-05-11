using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sqlBase
{
    class RequisitionSave
    {
        public string aid_no { get; set; }
        public string id_no { get; set; }
        public string eq_number { get; set; }
        public string adid_no { get; set; }




        public void SaveRequisitionUpd(RequisitionSave Requisition)

        {
            try
            {
                string qry = @"UPDATE PURCHASE.LASTCODES SET   AID_NO  ='" + Requisition.aid_no +
                                                      "',      ID_NO ='" + Requisition.id_no +
                                                     "' WHERE  EQ_NO  ='" + Requisition.eq_number + "'";
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
