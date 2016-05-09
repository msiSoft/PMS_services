using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace sqlBase
{
    public class Common
    {
        public void GetAllVendorNames(string VSLCode) // selecting all the vendor names 
        {
            try
            {
                string qry = @"SELECT DISTINCT V.VD_CODE,CVD_CODE,VD_NAME,VD_ADD1,VD_ADD2,VD_CITY,VD_STATE,CY_CODE
                                      FROM PURCHASE.VEND_MF V,
                                           PURCHASE.PO_HD P
                                      WHERE V.VD_CODE=P.VD_CODE
                                      AND P.VSLCODE	= " + VSLCode;
                SqlBase_OleDb db = new SqlBase_OleDb(qry);
                DataTable tbl = db.GetTable();
            }
            catch (Exception exc)
            {

            }
        }
    }
}
