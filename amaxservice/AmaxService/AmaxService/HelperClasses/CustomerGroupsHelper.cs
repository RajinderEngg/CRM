﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmaxDataService.DataModel;
namespace AmaxService.HelperClasses
{
    public class CustomerGroupsHelper
    {
        public Dictionary<string, object> GetCustomerGroupsDict(CustomerGroupsGeneralSetModel custGroupsobj)
        {
         Dictionary<string,object> CustDictList=new Dictionary<string,object>();
         try
         {
             if (custGroupsobj != null)
             {
                 CustDictList.Add("CustomerId", custGroupsobj.CustomerId);
                 CustDictList.Add("CustomerGeneralGroupId", custGroupsobj.CustomerGeneralGroupId);
                 //CustDictList.Add("OperationNumber", custGroupsobj.OperationNumber);
                 //CustDictList.Add("MountToCharge", custGroupsobj.MountToCharge);
                 //CustDictList.Add("HokDonationTypeId", custGroupsobj.HokDonationTypeId);
                 //CustDictList.Add("AccountID", custGroupsobj.AccountID);
                 //CustDictList.Add("KEVAStart", custGroupsobj.KEVAStart);
                 //CustDictList.Add("KEVAEnd", custGroupsobj.KEVAEnd);

                 //CustDictList.Add("KEVANAME", custGroupsobj.KEVANAME);
                 //CustDictList.Add("SmallRemark", custGroupsobj.SmallRemark);
                 //CustDictList.Add("BankCode", custGroupsobj.BankCode);
                 //CustDictList.Add("AccountType", custGroupsobj.AccountType);
                 //CustDictList.Add("AccountNo", custGroupsobj.AccountNo);
                 //CustDictList.Add("SnifNo", custGroupsobj.SnifNo);
                 //CustDictList.Add("ShortComment", custGroupsobj.ShortComment);
                 //CustDictList.Add("ID", custGroupsobj.ID);


                 //CustDictList.Add("MountToChargeUS", custGroupsobj.MountToChargeUS);
                 //CustDictList.Add("CreditCardId", custGroupsobj.CreditCardId);
                 //CustDictList.Add("TotalLeftToCharge", custGroupsobj.TotalLeftToCharge);
                 //CustDictList.Add("TotalMonthtoCharge", custGroupsobj.TotalMonthtoCharge);
                 //CustDictList.Add("TotalChargedMonth", custGroupsobj.TotalChargedMonth);
                 //CustDictList.Add("CurrencyId", custGroupsobj.CurrencyId);
                 //CustDictList.Add("EmployeeId", custGroupsobj.EmployeeId);
                 //CustDictList.Add("HokProjectId", custGroupsobj.HokProjectId);

                 //CustDictList.Add("KEVAJoinDate", custGroupsobj.KEVAJoinDate);
                 //CustDictList.Add("KEVACancleDate", custGroupsobj.KEVACancleDate);
                 //CustDictList.Add("ChargeDay", custGroupsobj.ChargeDay);
                 //CustDictList.Add("Moved", custGroupsobj.Moved);
                 //CustDictList.Add("JoinDate", custGroupsobj.JoinDate);
                 //CustDictList.Add("n1", custGroupsobj.n1);
                 //CustDictList.Add("n2", custGroupsobj.n2);
                 //CustDictList.Add("n3", custGroupsobj.n3);
                 //CustDictList.Add("n4", custGroupsobj.n4);
                 //CustDictList.Add("n5", custGroupsobj.n5);
                 //CustDictList.Add("n6", custGroupsobj.n6);
                 //CustDictList.Add("n7", custGroupsobj.n7);
                 //CustDictList.Add("n8", custGroupsobj.n8);
                 //CustDictList.Add("n9", custGroupsobj.n9);
                 //CustDictList.Add("n10", custGroupsobj.n10);
                 //CustDictList.Add("n11", custGroupsobj.n11);
                 //CustDictList.Add("n12", custGroupsobj.n12);
                 //CustDictList.Add("n13", custGroupsobj.n13);
                 //CustDictList.Add("n14", custGroupsobj.n14);
                 //CustDictList.Add("n15", custGroupsobj.n15);
                 //CustDictList.Add("n16", custGroupsobj.n16);
                 //CustDictList.Add("n17", custGroupsobj.n17);
                 //CustDictList.Add("n18", custGroupsobj.n18);
                 //CustDictList.Add("n19", custGroupsobj.n19);
                 //CustDictList.Add("n20", custGroupsobj.n20);
             }
         }
         catch (Exception ex)
         {
         }
            return CustDictList;
        }
    }
}