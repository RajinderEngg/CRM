using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
    public class CustomerTypeModel
    {
        public int TypeId { get; set; }

        public string TypeNameHeb { get; set; }
        public string TypeNameEng { get; set; }
        public bool Iron { get; set; }
        public string RGBCOLOR { get; set; }
    }
}
