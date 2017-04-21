using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
    public class ResponseData
    {
        public ResponseData()
        {
            IsError = false;
            ErrMsg = "";
        }
        public object Data { get; set; }
        public bool IsError { get; set; }
        public string ErrMsg { get; set; }
    }
}
