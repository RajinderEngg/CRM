using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
    public class ReceiptLineModel
    {
        public int RecieptRoWID { get; set; }
        public int RecieptType { get; set; }
        public string RecieptNo { get; set; }
        public int ProjectId { get; set; }
        public int AddressId { get; set; }
        public int PayTypeId { get; set; }
        public decimal Amount { get; set; }
        public decimal USDVal { get; set; }
        //public string TotalInWords { get; set; }
        public string ValueDate { get; set; }
        public string CheckNo { get; set; }
        public string BranchNo { get; set; }

        public string AccountNo { get; set; }
        public string details { get; set; }
        public int DonationTypeId { get; set; }
        public string ImageName { get; set; }
        public int AccountId { get; set; }
        public int CameFrom { get; set; }
        public string Bank { get; set; }
        public decimal AmountInLeadCurrent { get; set; }
        public string ReferenceDate { get; set; }
        public int OldReceiptId { get; set; }
        public bool Payed { get; set; }
        public string For_Invoice { get; set; }

        public bool IsExport { get; set; }
        public bool WasDeposit { get; set; }
        public int DepositeNo { get; set; }
        public string DepositeDate { get; set; }
        public int DepositeToAccountId { get; set; }
        public string DepositeRemark { get; set; }
        //public string WhatForInThanksLet { get; set; }
        public double TotalDeposit { get; set; }
        //public decimal TotalInLeadCurrent { get; set; }
        public string KevaInstitute { get; set; }
        //public string ReceiptNoKeva { get; set; }
        public string CreditCardType { get; set; }
      //  public int KeVaHistoryId { get; set; }
        public string RowDate { get; set; }
        public int BankId { get; set; }
        public int ProjectCategoryId { get; set; }
        public string Involved { get; set; }
        
        public string PayType { get; set; }
        public string DonationType { get; set; }
        public string ProjectCategory { get; set; }
        public string Project { get; set; }
        public string Account { get; set; }
    }
}
