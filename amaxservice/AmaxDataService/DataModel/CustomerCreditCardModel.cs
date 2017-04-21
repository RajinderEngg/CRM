using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
    public class CustomerCreditCardModel
    {
        public int customercreditCardid { get; set; }
        public int creditCardid { get; set; }
        public string creditCardNum { get; set; }
        public string creditCardName { get; set; }
        public int creditCardMonth { get; set; }
        public int creditCardYear { get; set; }
        public string creditCardBack { get; set; }
        public string creditCardOwnerID { get; set; }
        public int customerid { get; set; }
        public string RowDate { get; set; }
        public bool Deleted { get; set; }
        public string TokenNum { get; set; }
        public string TerminalNumber { get; set; }
        public string ParityNum { get; set; }
        public string creditApproveNo { get; set; }
        public string digits6 { get; set; }
        public string creditCardTypeName { get; set; }
    }
}
