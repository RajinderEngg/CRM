using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
   public class DevQueryModel
    {
        public string uqery { get; set; }
        public object parameters { get; set; }
    }

    public class DevQueryModelList
    {
        public DevQueryModel SmsCompanyList { get; set; }
        public DevQueryModel PhoneTypeList { get; set; }
    }
}
