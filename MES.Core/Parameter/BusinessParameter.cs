using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Core.Parameter
{
    public class BusinessParameter : ParameterBase
    {
        private string businessID;
        private string businessName;
        private string seriesID;
        private string seriesName;
        private string modelID;
        private string modelName;
        private string deviceID;
        private string deviceName;
        private string stationID;
        private string stationName;
        private string lineID;
        private string lineName;
        private string operatorID;
        private string operatorName;
        private string orderID;

        public string BusinessID { get { return this.businessID; } set { this.businessID = value; } }
        public string BusinessName { get { return this.businessName; } set { this.businessName = value; } }
        public string SeriesID { get { return this.seriesID; } set { this.seriesID = value; } }
        public string SeriesName { get { return this.seriesName; } set { this.seriesName = value; } }
        public string ModelID { get { return this.modelID; } set { this.modelID = value; } }
        public string ModelName { get { return this.modelName; } set { this.modelName = value; } }
        public string DeviceID { get { return this.deviceID; } set { this.deviceID = value; } }
        public string DeviceName { get { return this.deviceName; } set { this.deviceName = value; } }
        public string StationID { get { return this.stationID; } set { this.stationID = value; } }
        public string StationName { get { return this.stationName; } set { this.stationName = value; } }
        public string LineID { get { return this.lineID; } set { this.lineID = value; } }
        public string LineName { get { return this.lineName; } set { this.lineName = value; } }
        public string OperatorID { get { return this.operatorID; } set { this.operatorID = value; } }
        public string OperatorName { get { return this.operatorName; } set { this.operatorName = value; } }
        public string OrderID { get { return this.orderID; } set { this.orderID = value; } }

        public override string ToString()
        {
            //return base.ToString();

            return "Business";
        }
    }
}
