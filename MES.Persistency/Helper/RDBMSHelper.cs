using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Core;

namespace MES.Persistency.Helper
{
    class RDBMSHelper : IHelper
    {
        public IDictionary<string, object> Parameters { get; set; }

        public object Save(DataPair DataPair) 
        {
            DBDataIdentifier identifier = new DBDataIdentifier()
            {
                DataUniqueID = DataPair.Identifier.DataUniqueID,
                DataType = ((DBDataType)(DataPair.Identifier.DataType)),
                RawData = DataPair.Identifier.RawData as byte[]
            };

            DBDataItem dataItem;

            List<DBDataItem> dataItems = new List<DBDataItem>();

            List<string> dataItemIDList = null;

            for (int i = 0; i < DataPair.Items.Count; i++)
            {
                dataItem = new DBDataItem()
                {
                    DataItemID = Guid.NewGuid().ToString(),
                    DataIdentifier = identifier,
                    DataUniqueID = DataPair.Identifier.DataUniqueID,
                    CreationTime = DataPair.Items[i].CreationTime,
                    DataBytes = DataPair.Items[i].DataBytes,
                    Description = DataPair.Items[i].Description,
                    DeviceID = DataPair.Items[i].DeviceID,
                    LocationID = DataPair.Items[i].LocationID,
                    Size = DataPair.Items[i].Size
                };

                List<DBDataParameter> dataParameters = new List<DBDataParameter>();


                if (DataPair.Items[i].DataParameters != null)
                {
                    for (int j = 0; j < DataPair.Items[i].DataParameters.Count; j++)
                    {
                        dataParameters.Add(new DBDataParameter()
                        {
                            ID = Guid.NewGuid().ToString(),
                            Name = DataPair.Items[i].DataParameters[j].Name,
                            Value = DataPair.Items[i].DataParameters[j].Value,
                            DataItem = dataItem,
                            DataItemID = dataItem.DataItemID
                        });
                    }
                }

                dataItem.DataParameters = dataParameters.ToArray();
                dataItems.Add(dataItem);
            }

            identifier.DataItems = dataItems.ToArray();

            using (DBModelContainer context = new DBModelContainer())
            {
                var dbident = context.DBDataIdentifiers.FirstOrDefault((o)=>(o.DataUniqueID.ToLower() == identifier.DataUniqueID.ToLower()));

                if (dbident != null)
                {
                    foreach (var item in identifier.DataItems)
                    {
                        //dbident.DataItems.Add(item);
                        item.DataIdentifier = dbident;
                        context.DBDataItems.Add(item);
                    }

                    //identifier = dbident;
                }
                else
                {
                    identifier = context.DBDataIdentifiers.Add(identifier);
                }

                context.SaveChanges();
            }

            if (identifier != null)
            {
                dataItemIDList = new List<string>();

                foreach (var item in identifier.DataItems)
                {
                    dataItemIDList.Add(item.DataItemID);
                }
            }

            return dataItemIDList;
        }

        public object Add(string TransactionID, string SerialNumber)
        {
            Guid pairID = Guid.NewGuid();

            using (DBModelContainer context = new DBModelContainer())
            {
                context.ProductKeyIDSerialNumberPairs.Add(new ProductKeyIDSerialNumberPairs()
                {
                    PairID = pairID,
                    TransactionID = TransactionID,
                    SerialNumber = SerialNumber,
                    CreationTime = DateTime.Now,
                    SeriesID = ((this.Parameters.ContainsKey("SeriesID")) && (this.Parameters["SeriesID"] != null)) ? this.Parameters["SeriesID"].ToString() : "",
                    SeriesName = ((this.Parameters.ContainsKey("SeriesName")) && (this.Parameters["SeriesName"] != null)) ? this.Parameters["SeriesName"].ToString() : "",
                    ModelID = ((this.Parameters.ContainsKey("ModelID")) && (this.Parameters["ModelID"] != null)) ? this.Parameters["ModelID"].ToString() : "",
                    ModelName = ((this.Parameters.ContainsKey("ModelName")) && (this.Parameters["ModelName"] != null)) ? this.Parameters["ModelName"].ToString() : "",
                    DeviceID = ((this.Parameters.ContainsKey("DeviceID")) && (this.Parameters["DeviceID"] != null)) ? this.Parameters["DeviceID"].ToString() : "",
                    DeviceName = ((this.Parameters.ContainsKey("DeviceName")) && (this.Parameters["DeviceName"] != null)) ? this.Parameters["DeviceName"].ToString() : "",
                    OperatorID = ((this.Parameters.ContainsKey("OperatorID")) && (this.Parameters["OperatorID"] != null)) ? this.Parameters["OperatorID"].ToString() : "",
                    OperatorName = ((this.Parameters.ContainsKey("OperatorName")) && (this.Parameters["OperatorName"] != null)) ? this.Parameters["OperatorName"].ToString() : "",
                    StationID = ((this.Parameters.ContainsKey("StationID")) && (this.Parameters["StationID"] != null)) ? this.Parameters["StationID"].ToString() : "",
                    StationName = ((this.Parameters.ContainsKey("StationName")) && (this.Parameters["StationName"] != null)) ? this.Parameters["StationName"].ToString() : "",
                    LineID = ((this.Parameters.ContainsKey("LineID")) && (this.Parameters["LineID"] != null)) ? this.Parameters["LineID"].ToString() : "",
                    LineName = ((this.Parameters.ContainsKey("LineName")) && (this.Parameters["LineName"] != null)) ? this.Parameters["LineName"].ToString() : "",
                    BusinessID = ((this.Parameters.ContainsKey("BusinessID")) && (this.Parameters["BusinessID"] != null)) ? this.Parameters["BusinessID"].ToString() : "",
                    BusinessName = ((this.Parameters.ContainsKey("BusinessName")) && (this.Parameters["BusinessName"] != null)) ? this.Parameters["BusinessName"].ToString() : "",
                    OrderID = ((this.Parameters.ContainsKey("OrderID")) && (this.Parameters["OrderID"] != null)) ? this.Parameters["OrderID"].ToString() : ""
                });

                context.SaveChanges();
            }

            return pairID.ToString();
        }
    }
}
