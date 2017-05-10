using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
    public class ProductsModel
    {
        public int ProductId { get; set; }
        public int ProdCatId { get; set; }

        public string ProdName { get; set; }
        public string ProdNameEng { get; set; }
        public int PiecesPerBox { get; set; }
        public int PiecesPerCase { get; set; }
        public int BoxPerCase { get; set; }
        public int Pieces { get; set; }
        public int Cases { get; set; }
        public int Boxes { get; set; }
        public decimal Price { get; set; }
        public string CurrencyId { get; set; }
        public string MinPieces { get; set; }
        public string Comments { get; set; }
        public bool Deleted { get; set; }
        public string ValidDate { get; set; }

        public string PartNumber { get; set; }
        public string Unitofmeasure { get; set; }
        public decimal PricePerUnitBeforeVat { get; set; }
        public int HideFromStock { get; set; }

        public string ProdNameDis { get; set; }
        public string ProductCategory { get; set; }
    }
}
