using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace AmaxDataService.DataModel
{
    public class CustomersModel
    {
        public int CustomerId { get; set; }

        public int employeeid { get; set; }
        [Required(ErrorMessage ="Please enter first name")]
        public string fname { get; set; }
        [Required(ErrorMessage = "Please enter last name")]
        public string lname { get; set; }
        
        public string MiddleName { get; set; }
        //[Required(ErrorMessage = "Please select customer type")]
        public int CustomerType { get; set; }
        //[Required(ErrorMessage = "Please enter source")]
        public int CameFromCustomer { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerCode2 { get; set; }
        public string BirthDate { get; set; }
        public string BirthDate2 { get; set; }
        public string jobtitlePartner { get; set; }
        public int Titel { get; set; }
        public int Safixid { get; set; }
        public string Suffix { get; set; }
        public int Gender { get; set; }

        public List<CustomerAddressModel> CustomerAddresses { get; set; }
        public List<CustomerEmailsModel> CustomerEmails { get; set; }
        public List<CustomerPhonesModel> CustomerPhones { get; set; }
        public List<CustomerGroupsGeneralSetModel> CustomerGroups { get; set; }

        //public int CardType { get; set; }
        //public string RowDate { get; set; }
        
        
        //public bool Potentianl { get; set; }
        
        //public int RelatedCustomer { get; set; }
        //public int RelationType { get; set; }
        //public int ActiveStatus { get; set; }
        
        public string Remark { get; set; }
        //public string CustPosition { get; set; }


        [Required(ErrorMessage = "Please enter company name")]
        //public string SpouseName { get; set; }
        public string Company { get; set; }
        //public bool Deleted { get; set; }
        //public Nullable<int> TempId { get; set; }
        //public int tmp { get; set; }
        //public string assistant { get; set; }
        public string FileAs { get; set; }
        //public int PermitionLevel { get; set; }
        //public string Salutation { get; set; }
        //public bool SearchHide { get; set; }
        //public bool SecurityLock { get; set; }
        
        
        //public int CardStatus { get; set; }

        
        public string Title { get; set; }
        //public string ID { get; set; }
        //public string BankCode { get; set; }
        //public string SnifNo { get; set; }
        //public string AccountType { get; set; }
        //public string AccountNo { get; set; }
        //public string ShortComment { get; set; }
        //public int Kids { get; set; }
        //public int FamlyStat { get; set; }
        //public string ForeignName { get; set; }
        //public string HALIA { get; set; }
        //public bool Deceased { get; set; }
        //public string DeceasedYear { get; set; }
        //public string BornPlace { get; set; }
        //public bool AfterSunset1 { get; set; }
        //public bool AfterSunset2 { get; set; }

        
        //public int MemberID { get; set; }
        //public bool SpecialCust { get; set; }
        //public int SpouseID { get; set; }
        //public string MemberStart { get; set; }
        //public string MemberEnd { get; set; }
        //public string KEVAStart { get; set; }
        //public string KEVAEnd { get; set; }
        //public string KEVANAME { get; set; }
        //public string SmallRemark { get; set; }
        //public string Remark2 { get; set; }
        //public int HbirthMonthVal { get; set; }
        //public int HbirthMonthVal2 { get; set; }
        //public int HbirthDayVal { get; set; }
        //public int HbirthDayVal2 { get; set; }

        //public string HebDate1 { get; set; }
        //public string HebDate2 { get; set; }
        //public string LastUpdate { get; set; }
        //public string Mothername { get; set; }
        //public string Fathername { get; set; }
        //public int UpdateEmp { get; set; }
        //public string ArriveDate { get; set; }
        
        //public string IDSTR { get; set; }
        //public string CardIsueDate { get; set; }
        //public string TMPINFO { get; set; }
        //public string TitleSpouse { get; set; }
        //public string TMPINFO2 { get; set; }
        //public string TMPINFO3 { get; set; }
        //public string TMPINFO4 { get; set; }
        //public string xx { get; set; }

        //public int xxlen { get; set; }
        //public string ImageFileName { get; set; }
        //public int IsNewsLetter { get; set; }
        //public string n1 { get; set; }
        //public string n2 { get; set; }
        //public string n3 { get; set; }
        //public string n4 { get; set; }
        //public string n5 { get; set; }
        //public string n6 { get; set; }
        //public string n7 { get; set; }
        //public string n8 { get; set; }
        //public string n9 { get; set; }
        //public string n10 { get; set; }
        //public string n11 { get; set; }
        //public string n12 { get; set; }
        //public string n13 { get; set; }
        //public string n14 { get; set; }
        //public string n15 { get; set; }
        //public string n16 { get; set; }
        //public string n17 { get; set; }
        //public string n18 { get; set; }
        //public string n19 { get; set; }
        //public string n20 { get; set; }

        
    }
}
