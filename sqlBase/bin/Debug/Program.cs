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


            //for Take Stock
            TakeStockDisplay TD = new TakeStockDisplay();
            TD.GetStoreItems('I');

            TakeStockDisplay TDS = new TakeStockDisplay();
            TDS.GetSpareItems("400.0000000024");

            TakeStockDisplay TS = new TakeStockDisplay();
            TS.GetStock("400.0000000024");
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
    }
}
