﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sqlBase.Classes;

namespace sqlBase
{
    class Program
    {
        public static string grvno_auto, cgrv_no, vslcode;

        static void Main(string[] args)
        {
            //ZonesAndEquipments ZE = new ZonesAndEquipments();
            //ZE.GetEquipments("400.0000000024");
            //Common MasterData = new Common();
            //MasterData.GetAllVendorNames("400.0000000024");// Get all the available vendors for the vessel
            //Purchase PR = new Purchase();
            //PR.GetPONumbers("400.0000000024"); // Get all the PO numbers for the vessel
            //PR.GetAllPODetails("400.0000000024"); // Get all the item details for the vessel
            ////PR.ReceiveGoods();


            //KeyGenForInsert();

            ////Declaring an object of purchase header class 
            //POHdr purchaseHdr = new POHdr();

            //// Assigning dummy values to purchase header class variables
            //purchaseHdr.po_number = "0000002254";
            //purchaseHdr.cpo_number = " AST002/P16";
            //purchaseHdr.vd_code = "0000000154";
            //purchaseHdr.po_date = "15-MAR-2016";
            //purchaseHdr.challan_number = 1234;
            //purchaseHdr.receipt_date = "15-MAR-2016";
            //purchaseHdr.remarks = "Test";
            //purchaseHdr.data_entered_date = "15-MAR-2016";
            //purchaseHdr.data_entered_by = "SBN";
            //purchaseHdr.is_updated_on_server = true;
            //// purchaseHdr.SavePurchaseHdr(vslcode,grvno_auto, cgrv_no,purchaseHdr);

            ////Declaring an object of purchase header class 
            //POFinal pdtl = new POFinal();
            //pdtl.po_number = "0000002254";
            //pdtl.item_code = "$BNT730102";
            //pdtl.requested_qty = 10;
            //pdtl.ordered_qty = 10;
            //pdtl.received_qty = 10;
            //pdtl.accepted_qty = 8;
            //pdtl.code_type = "S";
            //pdtl.data_entered_date = "15-MAR-2016";
            //pdtl.data_entered_by = "SBN";
            //pdtl.is_updated_on_server = true;
            //pdtl.SavePurchaseDtl(vslcode, grvno_auto, pdtl, purchaseHdr.vd_code);

            //for Take Stock
            //TakeStockDisplay TD = new TakeStockDisplay();
            //TD.GetStoreItems('I');
            //TD.GetSpareItems("400.0000000024");
            //TD.GetStock("400.0000000024");

            //Save
            //Stock UPDS = new Stock();

            //UPDS.rob_qty = "10";
            //UPDS.trans_type_code = "00004";
            //UPDS.data_entered_by = "#SBNT";
            //UPDS.data_entered_date = "11-MAY-2016 09:34:17";
            //UPDS.total_in = "1";
            //UPDS.updflag = "C";
            //UPDS.item_code = "UNIX.00015138";
            //UPDS.vessel_code = "400.0000000024";
            //UPDS.trans_no = "AST.0000000025";
            //UPDS.ctrans_no = "0025/TR16";
            //UPDS.trans_date = "11-MAY-2016 09:34:17";
            //UPDS.trans_from = "PS";
            //UPDS.trans_qty = "10";
            //UPDS.plan_qty = "0";
            //UPDS.im_rem = "";
            //UPDS.trans_rem = "";
            //UPDS.code_type = "S";

            //UPDS.SaveStockUpd(UPDS);
            //UPDS.SaveStockInsertHd(UPDS);
            //UPDS.SaveStockInsertTd(UPDS);

            // Requisition
            // RequisitionSelect RS = new RequisitionSelect();
            // RS.GetLastCodes("COMMON");
            // RS.GetIndDt("400.0000000024");
            // RS.GetIndHd("400.0000000024");

            RequisitionSave RS = new RequisitionSave();

            RS.aid_no = "0";
            RS.id_number = "0000000009";
            RS.eq_number = " 43";
            RS.zone = "UNIX";
            RS.item_code = "UNIX.00017186";
            RS.required_qty = "3";
            RS.deliver_before = "18-MAR-2016 09:34:17";
            RS.cc_code = "Z";
            RS.ccm_code = "Z";
            RS.rob_qty = "1";
            RS.updflag = "C";
            RS.code_type = "I";
            RS.order_no = "1";
            RS.vessel_code = "400.0000000024";
            RS.requisition_number = "-16-0007";
            RS.requisition_date = "18-MAR-2016 09:34:17";
            RS.id_level = "N";
            RS.id_reqby = "MASTER";
            RS.forw_flag = "Z";
            RS.po_no = "Z";
            RS.send = "1";
            RS.tp_code = "004";
            RS.im_category = "G";
            RS.dept_code = "7";
            RS.attachment = "0";
            RS.fromoff = "V";


            // RS.SaveRequisitionUpd(RS);
            //RS.SaveRequisitionInsrtDt(RS);
            //RS.SaveRequisitionInsrtHd(RS);
            RecieveGoods();
        }
        public static void RecieveGoods()
        {
            KeyGenForInsert();

            //Declaring an object of purchase header class 
            POHdr purchaseHdr = new POHdr();

            // Assigning dummy values to purchase header class variables
            purchaseHdr.po_number = "0000002254";
            purchaseHdr.cpo_number = " AST002/P16";
            purchaseHdr.vd_code = "0000000154";
            purchaseHdr.po_date = "15-MAR-2016";
            purchaseHdr.challan_number = 1234;
            purchaseHdr.receipt_date = "15-MAR-2016";
            purchaseHdr.remarks = "Test";
            purchaseHdr.data_entered_date = "15-MAR-2016";
            purchaseHdr.data_entered_by = "SBN";
            purchaseHdr.is_updated_on_server = true;
            // purchaseHdr.SavePurchaseHdr(vslcode,grvno_auto, cgrv_no,purchaseHdr);

            //Declaring an object of purchase header class 
            POFinal pdtl = new POFinal();
            pdtl.po_number = "0000002254";
            pdtl.item_code = "$BNT730102";
            pdtl.requested_qty = 10;
            pdtl.ordered_qty = 10;
            pdtl.received_qty = 10;
            pdtl.accepted_qty = 8;
            pdtl.code_type = "S";
            pdtl.data_entered_date = "15-MAR-2016";
            pdtl.data_entered_by = "SBN";
            pdtl.is_updated_on_server = true;
            pdtl.SavePurchaseDtl(vslcode, grvno_auto, pdtl, purchaseHdr.vd_code);
            pdtl.UpdStock(pdtl, vslcode);



        }

        public static void KeyGenForInsert()
        {
            string p_vslcode = "'COMMON'";
            vslcode = "400.0000000024";
            string strInitial, strPrefix;
            OperationsOnSourceDB("UPDATE PURCHASE.lastcodes SET grv_no = grv_no + 1 WHERE p_vslcode =" + p_vslcode);

            //OperationsOnSourcecDB("UPDATE PURCHASE.lastcodes SET agrv_no = agrv_no + 1 WHERE p_vslcode = " + vslcode);

            int grv_no = (int)ExecuteScalarOnSourceDB("SELECT grv_no FROM PURCHASE.lastcodes WHERE p_vslcode = " + p_vslcode);
            int agrv_no = (int)ExecuteScalarOnSourceDB("SELECT agrv_no FROM PURCHASE.lastcodes WHERE p_vslcode =" + vslcode);

            if (ExecuteScalarOnSourceDB("SELECT INITIAL FROM PMS.PMS_VESSELMF WHERE VSLCODE =" + vslcode + " AND P_LATEST = 1 AND UPDFLAG <> 'D'") != DBNull.Value)
            {
                strInitial = (string)ExecuteScalarOnSourceDB("SELECT INITIAL FROM PMS.PMS_VESSELMF WHERE VSLCODE =" + vslcode + " AND P_LATEST = 1 AND UPDFLAG <> 'D'");
            }
            else
            {
                strInitial = "";
            }
            if (ExecuteScalarOnSourceDB("SELECT GRV_PREFIX FROM PURCHASE.SETUP") != DBNull.Value)
            {
                strPrefix = (string)ExecuteScalarOnSourceDB("SELECT GRV_PREFIX FROM PURCHASE.SETUP");
            }
            else
            {
                strPrefix = "";
            }
            string strSuffix = (string)ExecuteScalarOnSourceDB("SELECT GRV_SUFFIX FROM PURCHASE.SETUP");
            string yr = DateTime.Now.Year.ToString();
            int l = strSuffix.Length;
            int k = 0;
            for (int i = 0; i < l; i++)
            {
                if (strSuffix.Substring(i, 1) == "Y")
                {
                    k++;
                }
            }
            if (k == 2)
            {
                yr = yr.Substring(2, 2);
                strSuffix = strSuffix.Replace("YY", yr);

            }
            else if (k == 4)
                strSuffix = strSuffix.Replace("YYYY", yr);

            grvno_auto = "F" + grv_no;
            cgrv_no = strInitial + strPrefix + grv_no + strSuffix;

        }
        public static void Method2Ins()
        {
            OperationsOnSourceDB("INSERT INTO PMS.SETUP (CODE_PREFIX) VALUES ('TEST')");
            //  Method2DisplayRowCount("insertion");

        }
        public static int OperationsOnSourceDB(string qry)
        {
            int res = 0;

            try
            {
                OleDbHelper sourcedb = new OleDbHelper();

                sourcedb.createCommand();
                sourcedb.command.CommandText = qry;
                res = sourcedb.ExecuteQuery();

                sourcedb.command.CommandText = "COMMIT";
                res = sourcedb.ExecuteQuery();

                sourcedb.commit();
                sourcedb = null;
                return res;
            }
            catch
            {
                throw;
            }
        }
        public static object ExecuteScalarOnSourceDB(string qry)
        {
            object res;
            try
            {
                OleDbHelper sourcedb = new OleDbHelper();

                sourcedb.createCommand();
                sourcedb.command.CommandText = qry;
                res = sourcedb.command.ExecuteScalar();
                sourcedb.commit();

                sourcedb = null;
                return res;
            }
            catch
            {
                throw;
            }
        }
    }
}
