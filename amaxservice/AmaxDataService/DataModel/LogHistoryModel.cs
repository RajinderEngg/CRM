using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
    public class LogHistoryModel
    {
        public int id { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public string ExeptionType { get; set; }
        public string Error { get; set; }
        public Nullable<int> ExcLine { get; set; }
        public Nullable<int> ExcPlace { get; set; }
        public Nullable<System.DateTime> OnDate { get; set; }
        public string Action { get; set; }
        public string XmlData { get; set; }
        public string FromPage { get; set; }
        public string FullDescription { get; set; }
        public string APIVersion { get; set; }
        public Exception ex { get; set; }
        public string OrgId { get; set; }
        public string fname { get; set; }
    }
}
