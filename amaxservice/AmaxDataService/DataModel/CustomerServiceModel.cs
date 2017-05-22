using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
    public class CustomerServiceModel
    {
        public int CustomerServiceID { get; set; }

        public int employeeid { get; set; }
        public string RowDate { get; set; }
        public int EmployId { get; set; }

        public int ServiceTypeId { get; set; }
        public string StartTime { get; set; }
        public string StartHour { get; set; }
        public string StartMinute { get; set; }
        public string Details { get; set; }
        public string MemoDate { get; set; }
        public string MemoHour { get; set; }
        public string MemoMinutes { get; set; }
        public string FileName { get; set; }
        public int customerid { get; set; }
        public bool DoneiT { get; set; }
        public int Employeehandle { get; set; }

        public string DoneDate { get; set; }

        public string CallerName { get; set; }
        public string CallerPhone { get; set; }
        public string CallerPhone1 { get; set; }

        public string CallerEmail { get; set; }
        //[Required(ErrorMessage = "Please select customer type")]
        public int CallerMorInfo { get; set; }
        //[Required(ErrorMessage = "Please enter source")]
        public bool EmployeeMemo { get; set; }
        public int ProjectMajorTaskId { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public double Amount { get; set; }
        public string CurrencyId { get; set; }
        public bool RemindCreator { get; set; }
        public int TaskStatus { get; set; }
        public int FoundationID { get; set; }

        public double AmountReceived { get; set; }
        public string ApplyDate { get; set; }
        public string Answerdate { get; set; }
        public bool DonotJump { get; set; }
        public int IsReminderSms { get; set; }
        public int IsReminderEmail { get; set; }
        public int ParentServiceID { get; set; }
        public string FileName2 { get; set; }
        public string FileName3 { get; set; }
        public bool IsDeleted { get; set; }

        public String ServiceType { get; set; }
    }
}
