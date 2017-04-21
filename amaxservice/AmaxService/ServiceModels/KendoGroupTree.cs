using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxService.ServiceModels
{
    public class KendoGroupTree
    {
        public int id { get; set; }
        public string text { get; set; }
        public bool expanded { get; set; }
        public List<KendoGroupTree> items { get; set; }

        public KendoGroupTree()
        {
            items = new List<KendoGroupTree>();
        }
        //public KendoGroupTree(int Id,string Text, bool Expanded, List<KendoGroupTree> Items=null)
        //{
        //    this.id = Id;
        //    this.text = Text;
        //    this.expanded = Expanded;
        //    this.items = items!=null ? Items : new List<KendoGroupTree>();
        //}
    }
}
