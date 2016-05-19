using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sqlBase
{
    public class DBOperations
    {
        public int OperationsOnSourceDB(string qry, SQLBaseDB db = SQLBaseDB.DBPURCHV)
        {
            int res = 0;

            try
            {
                OleDbHelper sourcedb = new OleDbHelper(db);
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
        public object ExecuteScalarOnSourceDB(string qry)
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
