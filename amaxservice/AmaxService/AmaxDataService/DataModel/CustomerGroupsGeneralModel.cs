using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
    public class CustomerGroupsGeneralModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupNameEng { get; set; }
        public int GroupParenCategory { get; set; }
        public int CategoryId { get; set; }
        public bool bolTmp { get; set; }
        public int SortOrder { get; set; }
        public bool isWork { get; set; }
    }
}
