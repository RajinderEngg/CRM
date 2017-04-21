using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxDataService.DataModel
{
  public  class LoginModel
    {
        public string UserName { get;set;}
        public string Password { get; set; }
        public string OrgId { get; set; }
        //public bool RememberMe { get; set; }
        public string Language { get; set; }
    }
}
