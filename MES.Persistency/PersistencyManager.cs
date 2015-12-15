using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Core;
using MES.Persistency.Helper;

namespace MES.Persistency
{
    public class PersistencyManager
    {
        IHelper helper;

        public object Persist(DataPair DataPair, int PersistencyType, IDictionary<string, object> Parameters) 
        {
            switch (PersistencyType)
            {
                case 0: 
                    {
                        this.helper = new FileSystemHelper();
                        this.helper.Parameters = Parameters;
                        break;
                    }
                case 1:
                    {
                        this.helper = new RDBMSHelper();
                        this.helper.Parameters = Parameters;
                        break;
                    }
                default:
                    {
                        this.helper = null;
                        break;
                    }
            }

            return this.helper == null ? null : this.helper.Save(DataPair);
        }

        public object Record(string TransactionID, string SerialNumber, int PersistencyType, IDictionary<string, object> Parameters) 
        {
            switch (PersistencyType)
            {
                case 0:
                    {
                        this.helper = new FileSystemHelper();
                        this.helper.Parameters = Parameters;
                        break;
                    }
                case 1:
                    {
                        this.helper = new RDBMSHelper();
                        this.helper.Parameters = Parameters;
                        break;
                    }
                default:
                    {
                        this.helper = null;
                        break;
                    }
            }

            return this.helper == null ? null : this.helper.Add(TransactionID, SerialNumber);
        }
    }
}
