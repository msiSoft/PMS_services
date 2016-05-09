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
    }
}
