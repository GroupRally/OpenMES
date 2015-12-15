using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Core;

namespace MES.Acquirer
{
    public abstract class Acquirer : IAcquirer
    {
        public virtual void Open() 
        {
            throw new NotImplementedException("This method has not been implemented!");
        }

        public virtual void Start() 
        {
            throw new NotImplementedException("This method has not been implemented!");
        }

        public virtual DataItem Acquire() 
        {
            return null;
        }

        public virtual void Stop()
        {
            throw new NotImplementedException("This method has not been implemented!");
        }

        public virtual void Close()
        {
            throw new NotImplementedException("This method has not been implemented!");
        }

        public virtual byte[] Test(out object[] OutputData)
        {
            throw new NotImplementedException("This method has not been implemented!");
        }

        public virtual object[] Get() 
        {
            throw new NotImplementedException("This method has not been implemented!");
        }
    }
}
