using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Core.Parameter
{
    [System.Xml.Serialization.XmlRoot("AcquisitionParameterSet")]
    public class AcquisitionParameterSet : List<ParameterBase>
    {
        public AcquisitionParameterSet() : base() 
        {

        }

        public AcquisitionParameterSet(bool IsInitializing)
        {
            if (IsInitializing)
            {
                base.AddRange(new ParameterBase[] 
                { 
                    new NetworkParameter(), 
                    new CameraParameter(),
                    new BarcodeImagingParameter(),
                    new PersistencyParameter(),
                    new BusinessParameter()
                });
            }
        }

        public NetworkParameter GetNetworkParameter() 
        {
            foreach (ParameterBase parameter in this)
            {
                if (parameter is NetworkParameter)
                {
                    return parameter as NetworkParameter;
                }
            }

            return null;
        }

        public CameraParameter GetCameraParameter()
        {
            foreach (ParameterBase parameter in this)
            {
                if (parameter is CameraParameter)
                {
                    return parameter as CameraParameter;
                }
            }

            return null;
        }

        public BarcodeImagingParameter GetBarcodeImagingParameter()
        {
            foreach (ParameterBase parameter in this)
            {
                if (parameter is BarcodeImagingParameter)
                {
                    return parameter as BarcodeImagingParameter;
                }
            }

            return null;
        }

        public PersistencyParameter GetPersistencyParameter()
        {
            foreach (ParameterBase parameter in this)
            {
                if (parameter is PersistencyParameter)
                {
                    return parameter as PersistencyParameter;
                }
            }

            return null;
        }

        public BusinessParameter GetBusinessParameter() 
        {
            foreach (ParameterBase parameter in this)
            {
                if (parameter is BusinessParameter)
                {
                    return parameter as BusinessParameter;
                }
            }

            return null;
        }
    }
}
