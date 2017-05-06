using AmaxDataService.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace AmaxService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICrmService" in both code and config file together.
    //[ServiceContract]
    public interface ICrmService
    {
        #region Production services
        string SecurityConnection { get; set; }
        /// <summary>
        /// Sql data Execution method
        /// </summary>
        /// <param name="Parameter">input stream from the client end</param>
        /// <returns>Json result</returns>
        //[OperationContract, WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        object AmaxReportingService(dynamic Parameter);


        /// <summary>
        /// Validate User by the console
        /// </summary>
        /// <param name="Parameter">input stream from client end</param>
        /// <returns>Json string</returns>
        //[OperationContract, WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        object Login(LoginModel obj);


        /// <summary>
        /// Execute JSON use to save Data on the server
        /// </summary>
        /// <param name="Parameter">input stream from the client end</param>
        /// <returns>Json result</returns>
        //[OperationContract, WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        object ExecuteJson(dynamic Parameter);


        /// <summary>
        /// Returns a kendo tree for the tree view
        /// </summary>
        /// <param name="Parameter">Request input stream</param>
        /// <returns>Returns the group tree</returns>
        //[OperationContract, WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        object GetTreeData(dynamic Parameter);


        /// <summary>
        /// Use to send sms
        /// </summary>
        /// <param name="Parameter">Input stream of the request</param>
        /// <returns>Returns the sms send status</returns>
        //[OperationContract, WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        object SendSms(dynamic Parameter);

        /// <summary>
        /// Use to fetch teh data from server for the predefined queryes
        /// </summary>
        /// <param name="Parameter">Input stream of the request</param>
        /// <returns>Json responce for the data</returns>
        //[OperationContract, WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        object ExecuteDataService(dynamic Parameter);

        #endregion


        Dictionary<string, object> currentUser { get; set; }

        #region Dev methods Works only on debug library
#if DEBUG

        //[OperationContract, WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        object DevQuery(object Parameter);

#endif
        #endregion
    }
}
