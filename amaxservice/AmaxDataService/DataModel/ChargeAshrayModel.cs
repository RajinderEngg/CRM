using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
    public class ChargeAshrayModel
    {
        public string oNumOfPayments { get; set; }
        public string oTerminalNumber { get; set; }
        public string oSumToBill { get; set; }
        public string oCardValidityMonth { get; set; }
        public string oCardValidityYear { get; set; }

        public string oCardNumber { get; set; }
        public string oCardOwnerId { get; set; }
        public string oapprovalnumber { get; set; }
        public string ofirstpaymentsum { get; set; }
        public string oCurrency { get; set; }

        public string odealtype { get; set; }
        public string ouserpassword { get; set; }
        public string ousername { get; set; }
        public string oconstpatment { get; set; }
        public string DealCode { get; set; }

        public string CVV { get; set; }
        public string CustomerName { get; set; }
        public bool UseToken { get; set; }
        public string UniqAsmachta { get; set; }

        public int CustomerId { get; set; }
        public string CreditType { get; set; }
        public string ChargeType { get; set; }
        public string CompanySlika { get; set; }
        public string DealNumber { get; set; }
    }
}
