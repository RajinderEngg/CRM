using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
    public class CustomerEmailsModel
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string EmailName { get; set; }
        public bool Newslettere { get; set; }
        public bool General { get; set; }
        public int MaxYearDelivery { get; set; }
        public int MaxMonthlyDelivery { get; set; }
        public string LastEmail { get; set; }
        public int tempid { get; set; }
        public int Priority { get; set; }
        public bool EmailSex { get; set; }
        public int publish { get; set; }
    }
}
