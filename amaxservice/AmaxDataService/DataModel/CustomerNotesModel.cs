using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
    public class CustomerNotesModel
    {
        public int id { get; set; }
        public int subjectid { get; set; }
        public int employeeId { get; set; }
        public string Note { get; set; }

    }
}
