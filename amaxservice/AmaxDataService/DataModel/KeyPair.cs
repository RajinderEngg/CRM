using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
    public class KeyPair
    {
        public KeyPair(string val, string text)
        {
            Value = val;
            Text = text;
        }
        public string Value { get; set; }
        public string Text { get; set; }
    }

    public class GroupTree
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupNameEng { get; set; }
        public int GroupParenCategory { get; set; }
    }
}
