using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace AmaxDataService.DataModel
{
    public class RecieptThnksLetterModel
    {
        public int ThanksLetterId { get; set; }

        public string ThanksLetterName { get; set; }
        public string ThanksLetterNameEng { get; set; }
        public string ThanksLetterfileName { get; set; }
        //[Required(ErrorMessage = "Please select receipt type")]
        public int ReceiptId { get; set; }
        [Required(ErrorMessage = "Please enter body")]
        public string MailBody { get; set; }
        [Required(ErrorMessage = "Please enter subject")]
        public string MailSubj { get; set; }
        public bool IsRtl { get; set; }
        public int langNum { get; set; }

        public string RecieptThnksLetterText { get; set; }
    }
}
