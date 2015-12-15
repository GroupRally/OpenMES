using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Xml;
using MES.Core;
using MES.Utility;

namespace MES.Persistency.Helper
{
    class FileSystemHelper : IHelper
    {
        private IDictionary<string, object> parameters;

        public IDictionary<string, object> Parameters
        { 
            get 
            { 
                return this.parameters; 
            } 
            set 
            { 
                this.parameters = value; 
            }
        }

        public object Save(DataPair DataPair) 
        {
            string imageStore = this.parameters["DataStore"].ToString(); //ConfigurationManager.AppSettings.Get("DefaultImageStore");

            imageStore = imageStore.EndsWith("\\") ? imageStore.Substring(0, (imageStore.Length - 1)) : imageStore;

            string imageDirectory = String.Format("{0}\\{1}", imageStore, DataPair.Identifier.DataUniqueID);

            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            string filePath;

            List<string> currentFilePaths = new List<string>();

            string imageFileNameExtension = this.parameters["FileExtension"].ToString();

            imageFileNameExtension = imageFileNameExtension.ToLower();

            FileStream fileStream;

            //string[] barcodes;

            StreamWriter writer;

            for (int i = 0; i < DataPair.Items.Count; i++)
            {
                filePath = String.Format("{0}\\{1}.{2}", imageDirectory, CommonUtility.GetMillisecondsByDateTime(DataPair.Items[i].CreationTime, null), imageFileNameExtension);

                using (fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    fileStream.Write(DataPair.Items[i].DataBytes, 0, DataPair.Items[i].DataBytes.Length);
                }

                currentFilePaths.Add(filePath);

                if ((DataPair.Items[i].DataParameters != null) && (DataPair.Items[i].DataParameters.Count > 0) && (DataPair.Items[i].DataParameters[0] != null))
                {
                    //barcodes = DataPair.Items[i].DataParameters[0].Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    //using (fileStream = new FileStream(filePath + ".txt", FileMode.Create, FileAccess.Write, FileShare.Write))
                    //{
                    //    using (writer = new StreamWriter(fileStream))
                    //    {
                    //        foreach (string barcode in barcodes)
                    //        {
                    //            writer.WriteLine(barcode);
                    //        }
                    //    }
                    //}

                    using (fileStream = new FileStream(filePath + ".info.xml", FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        using (writer = new StreamWriter(fileStream, Encoding.UTF8))
                        {
                            XmlUtility xmlUtility = new XmlUtility();
                            string imageInfoXml = xmlUtility.XmlSerialize(DataPair, new Type[] { typeof(DataPair), typeof(DataIdentifier), typeof(DataItem), typeof(DataParameter), typeof(DataType) }, "utf-8");

                            if ((imageInfoXml.Length - imageInfoXml.LastIndexOf(">")) > 1)
                            {
                                imageInfoXml = imageInfoXml.Remove(imageInfoXml.LastIndexOf(">") + 1);
                            }

                            if (imageInfoXml.StartsWith("<?xml version=\"1.0\"?>"))
                            {
                                imageInfoXml = imageInfoXml.Insert(imageInfoXml.IndexOf("?>"), " encoding=\"utf-8\"");
                            }
                            
                            writer.Write(imageInfoXml);
                        }
                    }
                }
            }

            return currentFilePaths;
        }

        public object Add(string TransactionID, string SerialNumber)
        {
            FileStream stream = null;
            XmlWriter writer = null;

            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Encoding = System.Text.Encoding.UTF8,
                CloseOutput = true,
                WriteEndDocumentOnClose = true,
                Indent = true,
                ConformanceLevel = ConformanceLevel.Document
            };

            string pairID = Guid.NewGuid().ToString();

            string dataStore = this.parameters["DataStore"].ToString(); 

            dataStore = dataStore.EndsWith("\\") ? dataStore.Substring(0, (dataStore.Length - 1)) : dataStore;

            string filePath = dataStore + "\\Pairs.xml";

            if (!File.Exists(filePath))
            {
                using (stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    using (writer = XmlWriter.Create(stream, settings))
                    {
                        writer.WriteStartDocument(true);

                        writer.WriteStartElement("Pairs");
                        writer.WriteEndElement();

                        writer.WriteEndDocument();

                        //writer.Flush();
                    }
                }
            }

            XmlDocument document = new XmlDocument();

            using (stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                document.Load(stream);
            }

            XmlElement elementPair = document.CreateElement("Pair");

            XmlAttribute attributePairID = document.CreateAttribute("ID");
            attributePairID.Value = pairID;
            elementPair.Attributes.Append(attributePairID);

            XmlElement elementTransactionID = document.CreateElement("TransactionID");
            elementTransactionID.InnerText = TransactionID;
            elementPair.AppendChild(elementTransactionID);

            XmlElement elementProductKeyID = document.CreateElement("ProductKeyID");
            elementProductKeyID.InnerText = "";
            elementPair.AppendChild(elementProductKeyID);

            XmlElement elementSerialNumber = document.CreateElement("SerialNumber");
            elementSerialNumber.InnerText = SerialNumber;
            elementPair.AppendChild(elementSerialNumber);

            if (this.parameters.ContainsKey("SeriesID") && (this.parameters["SeriesID"] != null))
            {
                XmlElement elementSeriesID = document.CreateElement("SeriesID");
                elementSeriesID.InnerText = this.parameters["SeriesID"].ToString();
                elementPair.AppendChild(elementSeriesID);
            }

            if (this.parameters.ContainsKey("SeriesName") && (this.parameters["SeriesName"] != null))
            {
                XmlElement elementSeriesName = document.CreateElement("SeriesName");
                elementSeriesName.InnerText = this.parameters["SeriesName"].ToString();
                elementPair.AppendChild(elementSeriesName);
            }

            if (this.parameters.ContainsKey("ModelID") && (this.parameters["ModelID"] != null))
            {
                XmlElement elementModelID = document.CreateElement("ModelID");
                elementModelID.InnerText = this.parameters["ModelID"].ToString();
                elementPair.AppendChild(elementModelID);
            }

            if (this.parameters.ContainsKey("ModelName") && (this.parameters["ModelName"] != null))
            {
                XmlElement elementModelName = document.CreateElement("ModelName");
                elementModelName.InnerText = this.parameters["ModelName"].ToString();
                elementPair.AppendChild(elementModelName);
            }

            if (this.parameters.ContainsKey("DeviceID") && (this.parameters["DeviceID"] != null))
            {
                XmlElement elementDeviceID = document.CreateElement("DeviceID");
                elementDeviceID.InnerText = this.parameters["DeviceID"].ToString();
                elementPair.AppendChild(elementDeviceID);
            }

            if (this.parameters.ContainsKey("DeviceName") && (this.parameters["DeviceName"] != null))
            {
                XmlElement elementDeviceName = document.CreateElement("DeviceName");
                elementDeviceName.InnerText = this.parameters["DeviceName"].ToString();
                elementPair.AppendChild(elementDeviceName);
            }

            if (this.parameters.ContainsKey("OperatorID") && (this.parameters["OperatorID"] != null))
            {
                XmlElement elementOperatorID = document.CreateElement("OperatorID");
                elementOperatorID.InnerText = this.parameters["OperatorID"].ToString();
                elementPair.AppendChild(elementOperatorID);
            }

            if (this.parameters.ContainsKey("OperatorName") && (this.parameters["OperatorName"] != null))
            {
                XmlElement elementOperatorName = document.CreateElement("OperatorName");
                elementOperatorName.InnerText = this.parameters["OperatorName"].ToString();
                elementPair.AppendChild(elementOperatorName);
            }

            if (this.parameters.ContainsKey("StationID") && (this.parameters["StationID"] != null))
            {
                XmlElement elementStationID = document.CreateElement("StationID");
                elementStationID.InnerText = this.parameters["StationID"].ToString();
                elementPair.AppendChild(elementStationID);
            }

            if (this.parameters.ContainsKey("StationName") && (this.parameters["StationName"] != null))
            {
                XmlElement elementStationName = document.CreateElement("StationName");
                elementStationName.InnerText = this.parameters["StationName"].ToString();
                elementPair.AppendChild(elementStationName);
            }

            if (this.parameters.ContainsKey("LineID") && (this.parameters["LineID"] != null))
            {
                XmlElement elementLineID = document.CreateElement("LineID");
                elementLineID.InnerText = this.parameters["LineID"].ToString();
                elementPair.AppendChild(elementLineID);
            }

            if (this.parameters.ContainsKey("LineName") && (this.parameters["LineName"] != null))
            {
                XmlElement elementLineName = document.CreateElement("LineName");
                elementLineName.InnerText = this.parameters["LineName"].ToString();
                elementPair.AppendChild(elementLineName);
            }

            if (this.parameters.ContainsKey("BusinessID") && (this.parameters["BusinessID"] != null))
            {
                XmlElement elementBusinessID = document.CreateElement("BusinessID");
                elementBusinessID.InnerText = this.parameters["BusinessID"].ToString();
                elementPair.AppendChild(elementBusinessID);
            }

            if (this.parameters.ContainsKey("BusinessName") && (this.parameters["BusinessName"] != null))
            {
                XmlElement elementBusinessName = document.CreateElement("BusinessName");
                elementBusinessName.InnerText = this.parameters["BusinessName"].ToString();
                elementPair.AppendChild(elementBusinessName);
            }

            if (this.parameters.ContainsKey("OrderID") && (this.parameters["OrderID"] != null))
            {
                XmlElement elementOrderID = document.CreateElement("OrderID");
                elementOrderID.InnerText = this.parameters["OrderID"].ToString();
                elementPair.AppendChild(elementOrderID);
            }

            XmlElement elementCreationTime = document.CreateElement("CreationTime");
            elementCreationTime.InnerText = DateTime.Now.ToString();
            elementPair.AppendChild(elementCreationTime);

            XmlElement elementReadCount = document.CreateElement("ReadCount");
            elementReadCount.InnerText = "0";
            elementPair.AppendChild(elementReadCount);

            document.FirstChild.NextSibling.AppendChild(document.ImportNode(elementPair, true));

            using (stream = new FileStream(filePath, FileMode.Open, FileAccess.Write, FileShare.Write))
            {
                using (writer = XmlWriter.Create(stream, settings))
                {
                    document.Save(writer);
                }
            }

            return pairID;
        }
    }
}
