using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace sqlBase
{
    public enum SQLBaseDB
    {
        DBPURCHV,
        DBIMAGE
    }

    public class OleDbHelper
    {
        string connStr;
        OleDbConnection connection;
        public OleDbCommand command;

        public OleDbHelper(SQLBaseDB db = SQLBaseDB.DBPURCHV)
        {

            if (db == SQLBaseDB.DBPURCHV)
                connStr = "provider = SQLBASEOLEDB;Data Source=DBPURCHV;User Id=REPORT;Password=REPORT; ini=.\\Sql.ini";
            else if(db == SQLBaseDB.DBIMAGE)
                connStr = "provider = SQLBASEOLEDB;Data Source=DBIMAGE;User Id=REPORT;Password=REPORT; ini=.\\Sql.ini";

        }

        public IDataReader ExecuteDataReader(string qry)
        {
            try
            {
                var conn = new OleDbConnection(connStr);
                var cmd = conn.CreateCommand();
                {
                    conn.Open();
                    cmd.CommandText = qry;
                    return cmd.ExecuteReader();
                }
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<IDataRecord> GetRows(string qry)
        {
            using (var conn = new OleDbConnection(connStr))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = qry;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return reader;
                    }
                }
            }
        }

        public DataTable GetTable(string qry, string tblName)
        {
            OleDbDataAdapter adptr = new OleDbDataAdapter(qry, connStr);
            DataTable tbl = new DataTable(tblName);
            try
            {
                adptr.Fill(tbl);
            }
            catch
            {
                throw;
            }

            adptr = null;

            return tbl;
        }

        public bool createCommand()
        {
            try
            {
                if (connection == null)
                {
                    connection = new OleDbConnection(connStr);
                }
                if (connection.State != ConnectionState.Open)
                {
                    command = connection.CreateCommand();
                    connection.Open();
                }
                return true;
            }
            catch (Exception exc)
            {
                throw;
            }
        }

        public int ExecuteQuery()
        {
            try
            {
                return command.ExecuteNonQuery();
            }
            catch
            {
                return -1;//for error
            }
        }


        public bool commit()
        {
            try
            {
                if (connection.State != ConnectionState.Closed)
                {
                    command.Dispose();
                    connection.Close();
                    connection.Dispose();
                    return true;
                }
            }
            catch
            {
                throw;
            }
            return false;
        }
    }
}
