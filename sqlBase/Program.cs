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
            ZonesAndEquipments ZE = new ZonesAndEquipments();
            ZE.GetEquipments("400.0000000024");
            Common MasterData = new Common();
            MasterData.GetAllVendorNames("400.0000000024");// Get all the available vendors for the vessel
            Purchase PR = new Purchase();
            PR.GetPONumbers("400.0000000024"); // Get all the PO numbers for the vessel
            PR.GetAllPODetails("400.0000000024"); // Get all the item details for the vessel
            gettingRdyForInsert();

        }
        public static void gettingRdyForInsert()
        {
            string p_vslcode = "'COMMON'";
            string vslcode = "400.0000000024";
            object res;
            //OperationsOnSourcecDB(" UPDATE PURCHASE.lastcodes SET grv_no = grv_no + 1 WHERE p_vslcode =" +  p_vslcode );

            //OperationsOnSourcecDB("UPDATE PURCHASE.lastcodes SET agrv_no = agrv_no + 1 WHERE p_vslcode = " + vslcode);
           
            int grv_no = (int)ExecuteScalarOnSourcecDB("SELECT grv_no FROM PURCHASE.lastcodes WHERE p_vslcode = " + p_vslcode);
            int agrv_no = (int)ExecuteScalarOnSourcecDB("SELECT agrv_no FROM PURCHASE.lastcodes WHERE p_vslcode =" + vslcode);
            //string Initial=(string)ExecuteScalarOnSourcecDB("SELECT INITIAL FROM PMS.PMS_VESSELMF WHERE VSLCODE =" + vslcode + " AND P_LATEST = 1 AND UPDFLAG <> 'D'");
            //string Prefix = (string)ExecuteScalarOnSourcecDB("SELECT GRV_PREFIX FROM PURCHASE.SETUP");
            string Suffix = (string)ExecuteScalarOnSourcecDB("SELECT GRV_SUFFIX FROM PURCHASE.SETUP");

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
