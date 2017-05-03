using AmaxDataService.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxService.HelperClasses
{
    public class ReceiptsProductsHelper
    {
        public string SecurityconString { get; set; }
        public Dictionary<string, object> GetReceiptsProductsDict(ReceiptsProductsModel RecProdobj)
        {
            Dictionary<string, object> RecProdDictList = new Dictionary<string, object>();
            //try
            //{
            if (RecProdobj != null)
            {
                RecProdDictList.Add("ReceiptsProductsId", RecProdobj.ReceiptsProductsId);
                RecProdDictList.Add("ReceiptType", RecProdobj.ReceiptType);
                RecProdDictList.Add("ReceiptNo", RecProdobj.ReceiptNo);
                RecProdDictList.Add("ProductNo", RecProdobj.ProductNo);
                RecProdDictList.Add("ProductName", RecProdobj.ProductName);
                RecProdDictList.Add("Price", RecProdobj.Price);
                RecProdDictList.Add("Qty", RecProdobj.Qty);
                RecProdDictList.Add("Total", RecProdobj.Total);
                RecProdDictList.Add("RawDate", RecProdobj.RawDate);
            }
            //}
            //catch (Exception ex)
            //{
            //}
            return RecProdDictList;
        }
        public List<ReceiptsProductsModel> GetReceiptsProductsListFromDS(DataSet recprodobj)
        {
            List<ReceiptsProductsModel> FinalDictList = new List<ReceiptsProductsModel>();

            //try
            //{
            for (int i = 0; i < recprodobj.Tables[0].Rows.Count; i++)
            {
                ReceiptsProductsModel RecProdDictList = new ReceiptsProductsModel();
                RecProdDictList.ReceiptsProductsId = Convert.ToInt32(recprodobj.Tables[0].Rows[i]["ReceiptsProductsId"]);
                RecProdDictList.ReceiptType = Convert.ToInt32(recprodobj.Tables[0].Rows[i]["ReceiptType"]);
                RecProdDictList.ReceiptNo = Convert.ToString(recprodobj.Tables[0].Rows[i]["ReceiptNo"]);
                RecProdDictList.ProductNo = Convert.ToString(recprodobj.Tables[0].Rows[i]["ProductNo"]);
                RecProdDictList.ProductName = Convert.ToString(recprodobj.Tables[0].Rows[i]["ProductName"]);
                RecProdDictList.Price = Convert.ToDecimal(recprodobj.Tables[0].Rows[i]["Price"]);
                RecProdDictList.Qty = Convert.ToDecimal(recprodobj.Tables[0].Rows[i]["Qty"]);
                RecProdDictList.Total = Convert.ToDecimal(recprodobj.Tables[0].Rows[i]["Total"]);
                RecProdDictList.RawDate = Convert.ToDateTime(recprodobj.Tables[0].Rows[i]["RawDate"]).ToString("dd-MM-yyyy");
                
                FinalDictList.Add(RecProdDictList);
            }
            //}
            //catch (Exception ex)
            //{
            //}
            return FinalDictList;
        }
    }
}
