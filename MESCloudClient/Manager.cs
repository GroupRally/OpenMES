using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using MES.Core;
using ResourceIntegrator;

namespace MESCloud.Client
{
    public class Manager : IManager
    {
        IResourceRouter router;

        public Manager (bool IsTracingEnabled, string TraceSourceName)
        {
            this.router = new ResourceRouter(IsTracingEnabled, TraceSourceName);
        }

        public string Test()
        {
            string url = ModuleConfiguration.ServicePoint + ModuleConfiguration.UrlTest;

            object result = this.router.Get(url, new Authentication() { Type = AuthenticationType.Custom }, new Dictionary<string, string>() { { HttpRequestHeader.Authorization.ToString(), ModuleConfiguration.AuthorizationHeaderValue } });

            if (result != null)
            {
                //return result.ToString();

                return new DataContractSerializer(typeof(String), "string", "http://schemas.microsoft.com/2003/10/Serialization/").ReadObject(new MemoryStream(System.Text.Encoding.GetEncoding(ModuleConfiguration.EncodingName).GetBytes(result.ToString()))) as String;
            }

            return null;
        }

        public string[] NewResult(string TransactionID, Dictionary<string, string> ResultItems, byte[] RawData)
        {
            DataPair pair = new DataPair()
            {
                Identifier = new DataIdentifier()
                {
                    DataUniqueID = TransactionID,
                    RawData = RawData
                },

                Items = new List<DataItem>(new DataItem[]{ new DataItem()
                {
                     CreationTime = DateTime.Now,
                     DataBytes = RawData,
                     Size = (RawData != null) ? RawData.Length : 0,
                     DataParameters = new List<DataParameter>()
                }})
            };

            foreach (string itemName in ResultItems.Keys)
            {
                pair.Items[0].DataParameters.Add(new DataParameter() { Name = itemName, Value = ResultItems[itemName] });
            }

            string url = ModuleConfiguration.ServicePoint + ModuleConfiguration.UrlNewResult;

            string body = null;//new XmlUtility().XmlSerialize(pair, new Type[] {typeof(DataIdentifier), typeof(DataItem), typeof(DataParameter) }, "utf-8");

            MemoryStream stream = new MemoryStream();

            new DataContractSerializer(typeof(DataPair), "DataPair", "http://schemas.datacontract.org/2004/07/MES.Core").WriteObject(stream, pair);

            stream.Seek(0, System.IO.SeekOrigin.Begin);

            using (StreamReader reader = new System.IO.StreamReader(stream))
            {
                body = reader.ReadToEnd();
            }

            object result = this.router.Post(url, body, new Authentication() { Type = AuthenticationType.Custom }, new Dictionary<string, string>() { { HttpRequestHeader.Authorization.ToString(), ModuleConfiguration.AuthorizationHeaderValue } });

            if (result != null)
            {
                return new DataContractSerializer(typeof(string[]), "ArrayOfstring", "http://schemas.microsoft.com/2003/10/Serialization/Arrays").ReadObject(new MemoryStream(System.Text.Encoding.GetEncoding(ModuleConfiguration.EncodingName).GetBytes(result.ToString()))) as string[];
            }

            return null;
        }
    }
}
