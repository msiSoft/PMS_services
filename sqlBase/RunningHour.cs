using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sqlBase
{
    public class RunningHour
    {
        public string vessel_code { get; set; }
        public string eq_code { get; set; }
        public string eq_name { get; set; }
        public decimal rh_add { get; set; }
        public decimal rh_previous { get; set; }
        public decimal rh_current { get; set; }
        public string data_entered_date { get; set; }
        public string data_entered_by { get; set; }

        public string last_rh_add { get; set; }
        public string last_data_entered_date { get; set; }
        public string last_data_entered_by { get; set; }

        public decimal avg_per_day{ get; set; }
        public decimal max_per_day { get; set; }
    }
}
