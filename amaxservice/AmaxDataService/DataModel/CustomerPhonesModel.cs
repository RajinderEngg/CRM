using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
    public class CustomerPhonesModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int PhoneTypeId { get; set; }
        public string PhoneType { get; set; }
        public string Prefix { get; set; }
        public string Area { get; set; }
        public string Phone { get; set; }
        public string Comments { get; set; }
        public int IsSms { get; set; }
        public int phpublish { get; set; }

        public bool IsShowRemarks { get; set; }
        public string SMSOrder { get; set; }
        public string PublishOrder { get; set; }
    }
}
