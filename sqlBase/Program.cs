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
