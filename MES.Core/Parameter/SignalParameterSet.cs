using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Core.Parameter
{
    [System.Xml.Serialization.XmlRoot("SignalParameterSet")]
    public class SignalParameterSet : List<ParameterBase>
    {
        //private NetworkParameter networkParameter;
        //private SerialPortParameter serialPortParameter;
        //private TimerParameter timerParameter;
        //private PersistencyParameter persistencyParameter;

        public SignalParameterSet() : base() 
        {

        }

        public SignalParameterSet(bool IsInitializing) 
        {
            if (IsInitializing)
            {
                //this.networkParameter = new NetworkParameter();
                //this.serialPortParameter = new SerialPortParameter();
                //this.timerParameter = new TimerParameter();
                //this.persistencyParameter = new PersistencyParameter();

                this.AddRange(new ParameterBase[] 
                {
                    //this.networkParameter,
                    //this.serialPortParameter,
                    //this.timerParameter,
                    //this.persistencyParameter
                    
                    new NetworkParameter(),
                    new SerialPortParameter(),
                    new TimerParameter(),
                    new PersistencyParameter(),
                    new BusinessParameter()
                });
            }
        }

        public NetworkParameter GetNetworkParameter() 
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i] is NetworkParameter)
                {
                    return this[i] as NetworkParameter;
                }
            }

            return null;
        }

        public SerialPortParameter GetSerialPortParameter() 
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i] is SerialPortParameter)
                {
                    return this[i] as SerialPortParameter;
                }
            }

            return null;
        }

        public TimerParameter GetTimerParameter() 
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i] is TimerParameter)
                {
                    return this[i] as TimerParameter;
                }
            }
            return null;
        }

        public PersistencyParameter GetPersistencyParameter() 
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i] is PersistencyParameter)
                {
                    return this[i] as PersistencyParameter;
                }
            }

            return null;
        }

        public BusinessParameter GetBusinessParameter()
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i] is BusinessParameter)
                {
                    return this[i] as BusinessParameter;
                }
            }

            return null;
        }
    }
}
