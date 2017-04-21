using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AmaxDataService.DataModel
{
    public class CurrencyModel
    {
        public string CurrencyId { get; set; }
        public string CurrencyLead { get; set; }
        public string FrmUpdateCurrecy_Enm { get; set; }
        //public string FrmUpdateCurrecy_Enm { get; set; }
        public string CurrencySaveDate { get; set; }
    }
    //public class CURRENCY
    //{
    //    public string NAME { get; set; }
    //    public int UNIT { get; set; }
    //    public string CURRENCYCODE { get; set; }
    //    public string COUNTRY { get; set; }

    //    public decimal RATE { get; set; }
    //    public decimal CHANGE { get; set; }
    //}
    //public class CURRENCIES
    //{
    //    public CURRENCY[] Currency { get; set; }
    //    public string LAST_UPDATE { get; set; }
    //}




    [XmlRoot(ElementName = "CURRENCY")]
    public class CURRENCY
    {
        [XmlElement(ElementName = "NAME")]
        public string NAME { get; set; }
        [XmlElement(ElementName = "UNIT")]
        public string UNIT { get; set; }
        [XmlElement(ElementName = "CURRENCYCODE")]
        public string CURRENCYCODE { get; set; }
        [XmlElement(ElementName = "COUNTRY")]
        public string COUNTRY { get; set; }
        [XmlElement(ElementName = "RATE")]
        public string RATE { get; set; }
        [XmlElement(ElementName = "CHANGE")]
        public string CHANGE { get; set; }
    }

    [XmlRoot(ElementName = "CURRENCIES")]
    public class CURRENCIES
    {
        [XmlElement(ElementName = "LAST_UPDATE")]
        public string LAST_UPDATE { get; set; }
        [XmlElement(ElementName = "CURRENCY")]
        public List<CURRENCY> CURRENCY { get; set; }
    }
}
