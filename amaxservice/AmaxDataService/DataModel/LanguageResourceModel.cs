using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
    public class LanguageResourceModel
    {
        public int LRId { get; set; }
        public string FormType { get; set; }
        public string Lang { get; set; }
        public string KeyName { get; set; }
        public string KeyValue { get; set; }
    }
}
