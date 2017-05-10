using AmaxDataService.DataModel;
using AmaxExtentions.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxService.HelperClasses
{
    public class ProductsHelper
    {
        public string SecurityconString { get; set; }
        public string LangValue { get; set; }
        public Dictionary<string, object> GetProductsDict(ProductsModel prodobj)
        {
            Dictionary<string, object> ProdDictList = new Dictionary<string, object>();
            //try
            //{
            if (prodobj != null)
            {
                ProdDictList.Add("ProductId", prodobj.ProductId);
                ProdDictList.Add("ProdCatId", prodobj.ProdCatId);
                ProdDictList.Add("ProdName", prodobj.ProdName);
                ProdDictList.Add("ProdNameEng", prodobj.ProdNameEng);
                ProdDictList.Add("PiecesPerBox", prodobj.PiecesPerBox);
                ProdDictList.Add("PiecesPerCase", prodobj.PiecesPerCase);
                ProdDictList.Add("BoxPerCase", prodobj.BoxPerCase);
                ProdDictList.Add("Pieces", prodobj.Pieces);
                ProdDictList.Add("Boxes", prodobj.Boxes);
                ProdDictList.Add("Cases", prodobj.Cases);
                ProdDictList.Add("Price", prodobj.Price);
                ProdDictList.Add("CurrencyId", prodobj.CurrencyId);
                ProdDictList.Add("MinPieces", prodobj.MinPieces);
                ProdDictList.Add("Comments", prodobj.Comments);
                ProdDictList.Add("Deleted", prodobj.Deleted);
                ProdDictList.Add("ValidDate", prodobj.ValidDate);

                ProdDictList.Add("PartNumber", prodobj.PartNumber);
                ProdDictList.Add("Unitofmeasure", prodobj.Unitofmeasure);
                ProdDictList.Add("PricePerUnitBeforeVat", prodobj.PricePerUnitBeforeVat);
                ProdDictList.Add("HideFromStock", prodobj.HideFromStock);
            }

            // }
            // catch (Exception ex)
            //{
            //}
            return ProdDictList;
        }
        public List<ProductsModel> GetProductsListFromDS(DataSet prodobj)
        {
            List<ProductsModel> FinalDictList = new List<ProductsModel>();

            //try
            //{
            for (int i = 0; i < prodobj.Tables[0].Rows.Count; i++)
            {
                ProductsModel ProdDictList = new ProductsModel();
                ProdDictList.ProductId = Convert.ToInt32(prodobj.Tables[0].Rows[i]["ProductId"]);
                ProdDictList.ProdCatId = Convert.ToInt32(prodobj.Tables[0].Rows[i]["ProdCatId"]);
                ProdDictList.ProdName = Convert.ToString(prodobj.Tables[0].Rows[i]["ProdName"]);
                ProdDictList.ProdNameEng = Convert.ToString(prodobj.Tables[0].Rows[i]["ProdNameEng"]);
                ProdDictList.PiecesPerBox = Convert.ToInt32(prodobj.Tables[0].Rows[i]["PiecesPerBox"]);
                ProdDictList.PiecesPerCase = Convert.ToInt32(prodobj.Tables[0].Rows[i]["PiecesPerCase"]);
                ProdDictList.BoxPerCase = Convert.ToInt32(prodobj.Tables[0].Rows[i]["BoxPerCase"]);
                ProdDictList.Pieces = Convert.ToInt32(prodobj.Tables[0].Rows[i]["Pieces"]);
                ProdDictList.Boxes = Convert.ToInt32(prodobj.Tables[0].Rows[i]["Boxes"]);
                ProdDictList.Cases = Convert.ToInt32(prodobj.Tables[0].Rows[i]["Cases"]);
                ProdDictList.Price = Convert.ToDecimal(prodobj.Tables[0].Rows[i]["Price"]);
                ProdDictList.CurrencyId = Convert.ToString(prodobj.Tables[0].Rows[i]["CurrencyId"]);
                ProdDictList.MinPieces = Convert.ToString(prodobj.Tables[0].Rows[i]["MinPieces"]);
                ProdDictList.Comments = Convert.ToString(prodobj.Tables[0].Rows[i]["Comments"]);
                ProdDictList.Deleted = Convert.ToBoolean(prodobj.Tables[0].Rows[i]["Deleted"]);
                ProdDictList.ValidDate = Convert.ToDateTime(prodobj.Tables[0].Rows[i]["ValidDate"]).ToString("dd-MM-yyyy");

                ProdDictList.PartNumber = Convert.ToString(prodobj.Tables[0].Rows[i]["PartNumber"]);
                ProdDictList.Unitofmeasure = Convert.ToString(prodobj.Tables[0].Rows[i]["Unitofmeasure"]);
                ProdDictList.PricePerUnitBeforeVat = Convert.ToDecimal(prodobj.Tables[0].Rows[i]["PricePerUnitBeforeVat"]);
                ProdDictList.HideFromStock = Convert.ToInt32(prodobj.Tables[0].Rows[i]["HideFromStock"]);
                FinalDictList.Add(ProdDictList);
            }
            //}
            //catch (Exception ex)
            //{
            //}
            return FinalDictList;
        }
        public List<ProductsModel> GetProductsForSearch(int ProdCatId,string ProdNo,string ProdName)
        {
            List<ProductsModel> returnObj = new List<ProductsModel>();
            // try
            //{
            using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                string Query = "select * from Products where Deleted!=1 ";
                if(ProdCatId!=-1)
                    Query+= " and ProdCatId=" + ProdCatId+" ";
                if(ProdNo!="")
                    Query += " and PartNumber like '%" + ProdNo + "%' ";
                if(ProdName!="")
                    Query += " and (ProdName like '%" + ProdName + "%' or ProdName like '%" + ProdName + "%') ";
                DataSet ds = db.GetDataSet(Query, null, false);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    returnObj = GetProductsListFromDS(ds);
                    foreach (var retobj in returnObj)
                    {
                        Query = "select * from ProdactsCategories where ProdCatId="+retobj.ProdCatId;
                        ds = db.GetDataSet(Query, null, false);
                        if (LangValue == "en")
                        {
                            retobj.ProdNameDis = retobj.ProdNameEng;
                            if(ds.Tables[0].Rows.Count > 0)
                            retobj.ProductCategory = Convert.ToString(ds.Tables[0].Rows[0]["CategoryNameEng"]);
                        }
                        if (LangValue == "he")
                        {
                            retobj.ProdNameDis = retobj.ProdName;
                            if (ds.Tables[0].Rows.Count > 0)
                                retobj.ProductCategory = Convert.ToString(ds.Tables[0].Rows[0]["CategoryName"]);
                        }
                    }
                }
            }
            //}
            //catch (Exception ex)
            // {
            //}
            return returnObj;
        }
    }
}
