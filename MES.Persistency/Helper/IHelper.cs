using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Core;

namespace MES.Persistency.Helper
{
    interface IHelper
    {
        object Save(DataPair DataPair);

        object Add(string TransactionID, string SerialNumber);

        IDictionary<string, object> Parameters { get; set; }
    }
}
