using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
    public class CardChargeModel
    {
        public int id { get; set; }
        public int customerid { get; set; }
        public string theDate { get; set; }
        public decimal Mount { get; set; }
        public int EmployeeId { get; set; }
    }
}
