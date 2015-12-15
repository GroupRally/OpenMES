using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MES.Core;
using MES.Persistency;
using MES.Security.Authorization;
using MES.Processor;

namespace MESCloud.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MESService" in code, svc and config file together.
    public class MESService : IMESService
    {
        //public void DoWork()
        //{
        //}

        [Authorization(IsRequiringAuthentication = true)]
        public string Test()
        {
            return "Hello!";
        }

        [Authorization(IsRequiringAuthentication = true)]
        public ProductKeyIDSerialNumberPairs GetTransaction()
        {
            ProductKeyIDSerialNumberPairs returnValue = null;

            using (DBModelContainer context = new DBModelContainer())
            {
                var rawPairs = context.ProductKeyIDSerialNumberPairs.Where((o) => (o.ProductKeyID == null));

                if (rawPairs != null)
                {
                    ProductKeyIDSerialNumberPairs[] pairs = rawPairs.OrderBy((p) => (p.CreationTime)).ToArray();

                    if ((pairs != null) && (pairs.Length > 0))
                    {
                        returnValue = pairs[0];
                    }
                }
            }

            return returnValue;
        }

        [Authorization(IsRequiringAuthentication = true)]
        public string SetTransaction(string TransactionID, string SerialNumber, string ProductKeyID)
        {
            long prodcutKeyID = -9;

            if (!long.TryParse(ProductKeyID, out prodcutKeyID))
            {
                throw new FormatException("Invalid value supplied for product key ID!");
            }

            string returnValue = "";

            using (DBModelContainer context = new DBModelContainer())
            {
                ProductKeyIDSerialNumberPairs pair = context.ProductKeyIDSerialNumberPairs.FirstOrDefault((o) => ((o.SerialNumber.ToLower().StartsWith(SerialNumber.ToLower())) && (o.TransactionID.ToLower() == TransactionID.ToLower())));

                //ProductKeyIDSerialNumberPairs pair = context.ProductKeyIDSerialNumberPairs.FirstOrDefault((o) => (o.TransactionID.ToLower() == TransactionID.ToLower()));

                if (pair != null)
                {
                    pair.ProductKeyID = prodcutKeyID;
                    pair.ModificationTime = DateTime.Now;

                    returnValue = pair.PairID.ToString();

                    context.SaveChanges();
                }
            }

            return returnValue;
        }

        [Authorization(IsRequiringAuthentication = true)]
        public string[] Bind(string SerialNumber, string ProductKeyID) 
        {
            long prodcutKeyID = -9;

            if (!long.TryParse(ProductKeyID, out prodcutKeyID))
            {
                throw new FormatException("Invalid value supplied for product key ID!");
            }

            string[] returnValue = null;

            List<string> transactionIDs = new List<string>();

            using (DBModelContainer context = new DBModelContainer())
            {
                foreach (var pair in context.ProductKeyIDSerialNumberPairs.Where((o) => (o.SerialNumber.Trim().ToLower() == SerialNumber.ToLower())))
                {
                    if (pair != null)
                    {
                        pair.ProductKeyID = prodcutKeyID;
                        pair.ModificationTime = DateTime.Now;

                        transactionIDs.Add(pair.TransactionID);
                    }
                }

                returnValue = transactionIDs.ToArray();

                context.SaveChanges();
            }

            return returnValue;
        }

        [Authorization(IsRequiringAuthentication = true)]
        public ProductKeyIDSerialNumberPairs GetTransactionByModel(string ModelID)
        {
            ProductKeyIDSerialNumberPairs returnValue = null;

            using (DBModelContainer context = new DBModelContainer())
            {
                var rawPairs = context.ProductKeyIDSerialNumberPairs.Where((o) => ((o.ProductKeyID == null) && (o.ModelID.ToLower() == ModelID.ToLower())));

                if (rawPairs != null)
                {
                    ProductKeyIDSerialNumberPairs[] pairs = rawPairs.OrderBy((p) => (p.CreationTime)).ToArray();

                    if ((pairs != null) && (pairs.Length > 0))
                    {
                        returnValue = pairs[0];
                    }
                }
            }

            return returnValue;
        }

        [Authorization(IsRequiringAuthentication = true)]
        public ProductKeyIDSerialNumberPairs GetTransactionByDevice(string DeviceID)
        {
            ProductKeyIDSerialNumberPairs returnValue = null;

            using (DBModelContainer context = new DBModelContainer())
            {
                var rawPairs = context.ProductKeyIDSerialNumberPairs.Where((o) => ((o.ProductKeyID == null) && (o.DeviceID.ToLower() == DeviceID.ToLower())));

                if (rawPairs != null)
                {
                    ProductKeyIDSerialNumberPairs[] pairs = rawPairs.OrderBy((p) => (p.CreationTime)).ToArray();

                    if ((pairs != null) && (pairs.Length > 0))
                    {
                        returnValue = pairs[0];
                    }
                }
            }

            return returnValue;
        }

        [Authorization(IsRequiringAuthentication = true)]
        public string[] AddTestResult(DataPair DataPair)
        {
            PersistencyManager manager = new PersistencyManager();

            DataPair.Identifier.DataType = DataType.TestResult;

            List<string> dataItemIDList =  manager.Persist(DataPair, 1, null) as List<string>;

            return (dataItemIDList != null) ? dataItemIDList.ToArray() : null;
        }

        [Authorization(IsRequiringAuthentication = true)]
        public string AddTransaction(ProductKeyIDSerialNumberPairs Transaction)
        {
            string returnValue = "";

            using (DBModelContainer context = new DBModelContainer())
            {
                Transaction.CreationTime = DateTime.Now;

                var result = context.ProductKeyIDSerialNumberPairs.Add(Transaction);

                context.SaveChanges();

                returnValue = result.TransactionID;
            }

            return returnValue;
        }

        [Authorization(IsRequiringAuthentication = true, Roles= new string[]{MES.Security.RoleManager.SystemRole_LineOperator, MES.Security.RoleManager.SystemRole_SupperUser})]
        public string PrintSerialNumber(string TransactionID, string SerialNumber)
        {
           string printerName = MES.Processor.ModuleConfiguration.Default_PrinterName; //System.Configuration.ConfigurationManager.AppSettings.Get("SNPrinter");

           if (String.IsNullOrEmpty(printerName))
           {
               System.Drawing.Printing.PrinterSettings printerSetting = new System.Drawing.Printing.PrinterSettings();

               foreach (var printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
               {
                   printerSetting.PrinterName = printer.ToString();

                   if (printerSetting.IsDefaultPrinter)
                   {
                       printerName = printerSetting.PrinterName;
                       break;
                   }
               }
           }

           Processor processor = new Processor();

           DataPair pair = new DataPair() 
           { 
               Identifier = new DataIdentifier() { DataUniqueID = TransactionID, DataType= MES.Core.DataType.PrintingRecord, RawData = Encoding.UTF8.GetBytes(String.Format("{0}:{1}", TransactionID , SerialNumber)) } ,
               Items = new List<DataItem>
                (
                   new DataItem[] 
                   { 
                       new DataItem()
                       { 
                           CreationTime = DateTime.Now,
                           DeviceID = printerName,
                           DataParameters = new List<DataParameter>
                               (
                                 new DataParameter[]{new DataParameter()
                                 { 
                                     Name = "SerialNumber", 
                                     Value = SerialNumber
                                 }}
                               )
                       }
                   }
                )
           };

           pair = processor.Print(pair, TransactionID, "SerialNumber");

           PersistencyManager manager = new PersistencyManager();

           List<string> dataItemIDList = manager.Persist(pair, 1, null) as List<string>;

           return (dataItemIDList != null) ? dataItemIDList[0] : null;
        }

        [Authorization(IsRequiringAuthentication = true)]
        public ProductKeyIDSerialNumberPairs[] GetTransactionsByModel(string ModelID)
        {
            ProductKeyIDSerialNumberPairs[] returnValue = null;

            using (DBModelContainer context = new DBModelContainer())
            {
                var rawPairs = context.ProductKeyIDSerialNumberPairs.Where((o) => (o.ModelID.ToLower() == ModelID.ToLower()));

                if (rawPairs != null)
                {
                    returnValue = rawPairs.OrderBy((p) => (p.CreationTime)).ToArray();
                }
            }

            return returnValue;
        }

        [Authorization(IsRequiringAuthentication = true)]
        public ProductKeyIDSerialNumberPairs[] GetTransactionsByOrder(string OrderID)
        {
            ProductKeyIDSerialNumberPairs[] returnValue = null;

            using (DBModelContainer context = new DBModelContainer())
            {
                var rawPairs = context.ProductKeyIDSerialNumberPairs.Where((o) => (o.OrderID.ToLower() == OrderID.ToLower()));

                if (rawPairs != null)
                {
                    returnValue = rawPairs.OrderBy((p) => (p.CreationTime)).ToArray();
                }
            }

            return returnValue;
        }

        [Authorization(IsRequiringAuthentication = true)]
        public ProductKeyIDSerialNumberPairs[] GetTransactionsByDateRange(string StartTimeUTC, string EndTimeUTC)
        {
            DateTime startTime = DateTime.Parse(StartTimeUTC);

            DateTime endTime = DateTime.Parse(EndTimeUTC);

            ProductKeyIDSerialNumberPairs[] returnValue = null;

            using (DBModelContainer context = new DBModelContainer())
            {
                var rawPairs = context.ProductKeyIDSerialNumberPairs.Where((o) => ((o.CreationTime>= startTime) && (o.CreationTime <= endTime)));

                if (rawPairs != null)
                {
                    returnValue = rawPairs.OrderBy((p) => (p.CreationTime)).ToArray();
                }
            }

            return returnValue;
        }
    }
}