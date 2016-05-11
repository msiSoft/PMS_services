using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlBase
{
    class Program
    {
        static void Main(string[] args)
        {
            //ZonesAndEquipments ZE = new ZonesAndEquipments();
            //ZE.GetEquipments("400.0000000024");
            //Common MasterData = new Common();
            //MasterData.GetAllVendorNames("400.0000000024");// Get all the available vendors for the vessel
            //Purchase PR = new Purchase();
            //PR.GetPONumbers("400.0000000024"); // Get all the PO numbers for the vessel
            //PR.GetAllPODetails("400.0000000024"); // Get all the item details for the vessel
            //KeyGenForInsert();

            //for Take Stock
            //TakeStockDisplay TD = new TakeStockDisplay();
            //TD.GetStoreItems('I');
            //TD.GetSpareItems("400.0000000024");
            //TD.GetStock("400.0000000024");

            //Save
            Stock UPDS = new Stock();

            UPDS.rob_qty = "10";
            UPDS.trans_type_code = "00004";
            UPDS.data_entered_by = "#SBNT";
            UPDS.data_entered_date = "11-MAY-2016 09:34:17";
            UPDS.total_in = "1";
            UPDS.updflag = "C";
            UPDS.item_code = "UNIX.00015138";
            UPDS.vessel_code = "400.0000000024";
            UPDS.trans_no = "AST.0000000025";
            UPDS.ctrans_no = "0025/TR16";
            UPDS.trans_date = "11-MAY-2016 09:34:17";
            UPDS.trans_from = "PS";
            UPDS.trans_qty = "10";
            UPDS.plan_qty = "0";
            UPDS.im_rem = "";
            UPDS.trans_rem = "";
            UPDS.code_type = "S";

            //UPDS.SaveStockUpd(UPDS);
           // UPDS.SaveStockInsertHd(UPDS);
            UPDS.SaveStockInsertTd(UPDS);

        }
        public static void KeyGenForInsert()
        {
            string p_vslcode = "'COMMON'";
            string vslcode = "400.0000000024";
            string strInitial, strPrefix;
            OperationsOnSourcecDB("UPDATE PURCHASE.lastcodes SET grv_no = grv_no + 1 WHERE p_vslcode =" +  p_vslcode );

            //OperationsOnSourcecDB("UPDATE PURCHASE.lastcodes SET agrv_no = agrv_no + 1 WHERE p_vslcode = " + vslcode);

            int grv_no = (int)ExecuteScalarOnSourcecDB("SELECT grv_no FROM PURCHASE.lastcodes WHERE p_vslcode = " + p_vslcode);
            int agrv_no = (int)ExecuteScalarOnSourcecDB("SELECT agrv_no FROM PURCHASE.lastcodes WHERE p_vslcode =" + vslcode);

            if (ExecuteScalarOnSourcecDB("SELECT INITIAL FROM PMS.PMS_VESSELMF WHERE VSLCODE =" + vslcode + " AND P_LATEST = 1 AND UPDFLAG <> 'D'") != DBNull.Value)
            {
                strInitial = (string)ExecuteScalarOnSourcecDB("SELECT INITIAL FROM PMS.PMS_VESSELMF WHERE VSLCODE =" + vslcode + " AND P_LATEST = 1 AND UPDFLAG <> 'D'");
            }
            else
            {
                strInitial = "";
            }
            if (ExecuteScalarOnSourcecDB("SELECT GRV_PREFIX FROM PURCHASE.SETUP")!=DBNull.Value )
            {
                strPrefix= (string)ExecuteScalarOnSourcecDB("SELECT GRV_PREFIX FROM PURCHASE.SETUP");
            }
            else
            {
                strPrefix ="";
            }
            string strSuffix = (string)ExecuteScalarOnSourcecDB("SELECT GRV_SUFFIX FROM PURCHASE.SETUP");
            string yr = DateTime.Now.Year.ToString ();
            int l = strSuffix.Length;
            int k=0;  
            for (int i=0;i< l; i++)
            {
                if (strSuffix .Substring (i,1)=="Y")
                {
                    k++;
                }
            }
            if (k == 2)
            {
                yr = yr.Substring(2, 2);
                strSuffix = strSuffix.Replace("YY", yr);

            }
            else if (k==4)
                strSuffix = strSuffix.Replace("YYYY", yr);

            string grvno_auto = "F" + grv_no;
            string cgrv_no = strInitial + strPrefix +grv_no+strSuffix;
        }
        public static void Method2Ins()
        {
            OperationsOnSourcecDB("INSERT INTO PMS.SETUP (CODE_PREFIX) VALUES ('TEST')");
          //  Method2DisplayRowCount("insertion");

        }
        public static int OperationsOnSourcecDB(string qry)
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
            public static object  ExecuteScalarOnSourcecDB(string qry)
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
