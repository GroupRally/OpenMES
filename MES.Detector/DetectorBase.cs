using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Core;
using MES.Utility;
using MES.Communication;

namespace MES.Detector
{
    public abstract class DetectorBase : IDetector
    {
        private CommunicationManager communicationManager;

        public DataIdentifier DataIdentifier { get;set;}

        public virtual void Detect() 
        {
            throw new NotImplementedException("This method has not been implemented!");
        }

        public virtual void Notify(DataIdentifier Identifier) 
        {
            this.DataIdentifier = Identifier;

            if (this.communicationManager == null)
            {
                this.communicationManager = new CommunicationManager()
                {
                     RemoteAddress = ModuleConfiguration.Default_MulticastAddress,
                     RemotePort = ModuleConfiguration.Default_MulticastPort,
                     LocalAddress = ModuleConfiguration.Default_LocalAddress,
                     LocalPort = ModuleConfiguration.Default_LocalPort,
                     TimeToLive = ModuleConfiguration.Default_TimeToLive,
                     PipeName = ModuleConfiguration.Default_PipeName
                };

                //this.communicationManager.Start();

                this.communicationManager.Start((int)(ModuleConfiguration.Default_CommunicationType));
            }

            byte[] data = CommonUtility.BinarySerialize(Identifier); //JsonUtility.JsonSerialize(Identifier, null, null);

            this.communicationManager.Send(data);
        }

        public virtual void Stop() 
        {
            //throw new NotImplementedException("This method has not been implemented!");

            if (this.communicationManager != null)
            {
                this.communicationManager.Close();
            }
        }

        public virtual byte[] Test(out object[] OutputData) 
        {
            //throw new NotImplementedException("This method has not been implemented!");

            if (this.communicationManager != null)
            {
                this.communicationManager.Close();
            }

            OutputData = null;

            return null;
        }
    }
}