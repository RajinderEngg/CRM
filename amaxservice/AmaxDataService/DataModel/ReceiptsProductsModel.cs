using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
    public class ReceiptsProductsModel
    {
        public int ReceiptsProductsId { get; set; }
        public int ReceiptType { get; set; }
        public string ReceiptNo { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public decimal Total { get; set; }
        public string RawDate { get; set; }
        public int RowNo { get; set; }
    }
}
