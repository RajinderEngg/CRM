using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
    public class ReseiptsModel
    {
        public int RecieptType { get; set; }
        public string RecieptNo { get; set; }
        public string RecieptDate { get; set; }
        public int CustomerId { get; set; }
        public int AddressId { get; set; }
        public int RecievedCustId { get; set; }
        public string WhatFor { get; set; }
        public string CurrencyId { get; set; }
        public string TotalInWords { get; set; }
        public decimal Total { get; set; }
        public int associationId { get; set; }
        public int EmployeeId { get; set; }

        public bool ThanksLetter { get; set; }
        public int ThanksLetterId { get; set; }
        public string Credit4Digit { get; set; }
        public int PrinterId { get; set; }
        public bool OriginalWasPrinted { get; set; }
        public string StateId { get; set; }
        public string CityName { get; set; }
        public string CountryCode { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string Zip { get; set; }
        public string fname { get; set; }

        public string lname { get; set; }
        public string Titel { get; set; }
        public string MiddleName { get; set; }
        public string Company { get; set; }
        public string Safix { get; set; }
        public string Address_Remark { get; set; }
        public string WhatForInThanksLet { get; set; }
        public string LeadCurrency { get; set; }
        public decimal TotalInLeadCurrent { get; set; }
        public string CustomizeLine { get; set; }
        public string ReceiptNoKeva { get; set; }
        public int ReceiptTypeKeva { get; set; }

        public int KeVaHistoryId { get; set; }
        public int digitalEmployeeId { get; set; }
        public string digitalfileName { get; set; }
        public string digitalPath { get; set; }
        public string digitalDate { get; set; }
        public string RowDate { get; set; }
        public int isCredit { get; set; }
        public string CustomerName { get; set; }

        public int RecieptId { get; set; }
        public string FreeLine { get; set; }
        public string DepositedBy { get; set; }
        public string PaymentRequest { get; set; }
        public int CustomerNoteId { get; set; }
        public List<ReceiptLineModel> ReceiptLines { get; set; }
        public List<ReceiptsProductsModel> ReceiptProducts { get; set; }
        public decimal ProductTotal { get; set; }
    }
}
