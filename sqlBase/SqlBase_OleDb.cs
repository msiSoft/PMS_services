using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace sqlBase
{
   public class SqlBase_OleDb
    {
        OleDbDataAdapter adptr;
        OleDbCommandBuilder bldr;
        DataSet ds;

        public SqlBase_OleDb(string qry)
        {
            string connStr = "Provider = SQLBASEOLEDB;Data Source=DBPURCHV;User Id=REPORT;Password=REPORT; ini=Sql.ini";
            adptr          = new OleDbDataAdapter ( qry, connStr );
            ds             = new DataSet ( );

            adptr.Fill ( ds );
        }

        public DataRow GetRow(int indx = 0)
        {
            return ds.Tables[0].Rows[indx];
        }

        public DataTable GetTable()
        {
            return ds.Tables[0];
        }

        public long RowCount()
        {
            return ds.Tables[0].Rows.Count;
        }
        public static int updSrcDb(string qry)
        {
            int res = 0;

            try
            {
                //OleDbHelper sourcedb = new OleDbHelper();

                //sourcedb.createCommand();
                //sourcedb.command.CommandText = qry;
                //res = sourcedb.ExecuteQuery();

                //sourcedb.command.CommandText = "COMMIT";
                //res = sourcedb.ExecuteQuery();

                //sourcedb.commit();
                //sourcedb = null;
                return res;
            }
            catch
            {
                throw;
            }
        }
    }
}
