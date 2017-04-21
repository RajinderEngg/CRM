using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace AmaxDataService.DataModel
{
    public class RecieptTypeModel
    {
        public int RecieptTypeId { get; set; }

        public string RecieptName { get; set; }
        public string RecieptNameEng { get; set; }
        public int StartFromNum { get; set; }
        public string CurrencyId { get; set; }
        public bool ForCanclation { get; set; }
        public string TopTitleBig { get; set; }
        public string TopTitleSmall { get; set; }
        public string ButtomTitleBig { get; set; }
        public string ButtomTitleSmall { get; set; }
        public string SignitureImage { get; set; }
        public int ThanksLetterId { get; set; }
        public bool UseAsCreditReceipt { get; set; }
        public string DatePrintFormat { get; set; }
        public bool Preview { get; set; }
        public int ReceiptCancelID { get; set; }
        public bool DonationReceipt { get; set; }

        public bool NotForTaxReport { get; set; }
        public int SecurityLevel { get; set; }
        public bool FORKEVA { get; set; }
        public bool IsSupport { get; set; }
        public int Num2WordLng { get; set; }
        public int ToRecordPaysForSupp { get; set; }

        public bool HandReciept { get; set; }
        public bool RecieptForInvoice { get; set; }
        public int HideIt { get; set; }
       
        public string RecieptTypeText { get; set; }
    }
}
