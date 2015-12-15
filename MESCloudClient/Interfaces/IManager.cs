using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MESCloud.Client
{
    public interface IManager
    {
        string Test();

        string[] NewResult(string TransactionID, Dictionary<string, string> ResultItems, byte[] RawData);
    }
}
