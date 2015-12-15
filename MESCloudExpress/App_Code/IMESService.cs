using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using MES.Core;
using MES.Persistency;

namespace MESCloud.Services
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMESService" in both code and config file together.
    [ServiceContract]
    public interface IMESService
    {
        //[OperationContract]
        //void DoWork();

        [OperationContract]
        [WebGet(UriTemplate = "/Test/")]
        string Test();

        [OperationContract]
        [WebGet(UriTemplate = "/Transaction/First/")]
        ProductKeyIDSerialNumberPairs GetTransaction();

        [OperationContract]
        [WebGet(UriTemplate = "/Transaction/Model/{ModelID}")]
        ProductKeyIDSerialNumberPairs GetTransactionByModel(string ModelID);

        [OperationContract]
        [WebGet(UriTemplate = "/Transaction/Device/{DeviceID}")]
        ProductKeyIDSerialNumberPairs GetTransactionByDevice(string DeviceID);

        [OperationContract]
        [WebGet(UriTemplate = "/Transactions/Model/{ModelID}")]
        ProductKeyIDSerialNumberPairs[] GetTransactionsByModel(string ModelID);

        [OperationContract]
        [WebGet(UriTemplate = "/Transactions/Device/{OrderID}")]
        ProductKeyIDSerialNumberPairs[] GetTransactionsByOrder(string OrderID);

        [OperationContract]
        [WebGet(UriTemplate = "/Transactions/Start/{StartTimeUTC}/End/{EndTimeUTC}")]
        ProductKeyIDSerialNumberPairs[] GetTransactionsByDateRange(string StartTimeUTC, string EndTimeUTC);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Transaction/{TransactionID}/{SerialNumber}/{ProductKeyID}/")]
        string SetTransaction(string TransactionID, string SerialNumber, string ProductKeyID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Bind/{SerialNumber}/{ProductKeyID}/")]
        string[] Bind(string SerialNumber, string ProductKeyID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Result/New/")]
        string[] AddTestResult(DataPair DataPair);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Transaction/New/")]
        string AddTransaction(ProductKeyIDSerialNumberPairs Transaction);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Print/{TransactionID}/{SerialNumber}/")]
        string PrintSerialNumber(string TransactionID, string SerialNumber);
    }
}