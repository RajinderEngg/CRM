using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
    public class CustomerAddressModel
    {
        public int AddressId { get; set; }
        public int CustomerId { get; set; }
        
        public string StateId { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string Zip { get; set; }
        public string remark { get; set; }
        public bool ForDelivery { get; set; }
        public int MaxYearDelivery { get; set; }
        public int MaxMonthlyDelivery { get; set; }
        public string LastDelivery { get; set; }
        public string AddToName { get; set; }
        public int AddressTypeId { get; set; }
        public bool MainAddress { get; set; }

        public string SrteetOnly { get; set; }
        public string StreetNo { get; set; }
        public string Entrance { get; set; }
        public string DeliveryCode { get; set; }

        public string MainOrder { get; set; }
        public string DelvryOrder { get; set; }

    }
}
