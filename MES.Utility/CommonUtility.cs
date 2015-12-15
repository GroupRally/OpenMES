using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MES.Utility
{
    public class CommonUtility
    {
        public static object EnitObject(String AssemblyName, String TypeName)
        {
            ObjectHandle objectHandle = Activator.CreateInstance(AssemblyName, TypeName);

            return objectHandle.Unwrap();
        }

        public static object EnitObject(String AssemblyName, String TypeName, object[] arguments)
        {
            Assembly assembly = Assembly.Load(AssemblyName);

            Type type = assembly.GetType(TypeName);

            return Activator.CreateInstance(type, arguments);
        }

        public static byte[] BinarySerialize(object objectToSerialize) 
        {
            byte[] returnValue = null;

            IFormatter formater = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream())
            {
                formater.Serialize(stream, objectToSerialize);

                returnValue = stream.GetBuffer();
            }

            return returnValue;
        }

        public static object BinaryDeserialize(byte[] objectBytes) 
        {
            object returnValue = null;

            IFormatter formater = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream(objectBytes))
            {
                returnValue = formater.Deserialize(stream);
            }

            return returnValue;
        }

        public static IList<IDictionary<string, object>> ConvertDataTableToList(DataTable table)
        {
            IList<IDictionary<string, object>> returnValue = null;

            if ((table != null) && (table.Rows.Count > 0))
            {
                returnValue = new List<IDictionary<string, object>>();

                Dictionary<string, object> dataEntry = null;

                foreach (DataRow row in table.Rows)
                {
                    if (row != null)
                    {
                        dataEntry = new Dictionary<string, object>();

                        foreach (DataColumn column in table.Columns)
                        {
                            dataEntry.Add(column.ColumnName, row[column.ColumnName]);
                        }

                        returnValue.Add(dataEntry);
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Converts the current date time value to a millisecond value with the reference starting date specified 
        /// </summary>
        /// <param name="refDateTimeString">The string representation of the reference date time value(The default value is: 1970-01-01 00:00:00)</param>
        /// <returns>The milliseconds between the current date time value and the reference date time value</returns>
        public static string GetMillisecondsOfCurrentDateTime(string refDateTimeString)
        {
            string returnValue = String.Empty;

            if (String.IsNullOrEmpty(refDateTimeString))
            {
                refDateTimeString = "1970-01-01 00:00:00";
            }

            if (!String.IsNullOrEmpty(refDateTimeString))
            {
                DateTime start = DateTime.Now;
                DateTime end = DateTime.Now;

                if ((DateTime.TryParse(refDateTimeString, out start)))
                {
                    TimeSpan span = end.Subtract(start);

                    returnValue = span.TotalMilliseconds.ToString();
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Converts a date time value to a millisecond value with the reference starting date specified 
        /// </summary>
        /// <param name="dateTimeEnd">The date time value to be converted</param>
        /// <param name="refDateTimeString">The string representation of the reference date time value(The default value is: 1970-01-01 00:00:00)</param>
        /// <returns>The milliseconds between the date time value and the reference date time value</returns>
        public static string GetMillisecondsByDateTime(DateTime dateTimeEnd, string refDateTimeString)
        {
            string returnValue = String.Empty;

            if (String.IsNullOrEmpty(refDateTimeString))
            {
                refDateTimeString = "1970-01-01 00:00:00";
            }

            if (!String.IsNullOrEmpty(refDateTimeString))
            {
                DateTime start = DateTime.Now;

                if (DateTime.TryParse(refDateTimeString, out start))
                {
                    TimeSpan span = dateTimeEnd.Subtract(start);

                    returnValue = span.TotalMilliseconds.ToString();
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Determines if a sequence takes a character according to the mask code specified
        /// </summary>
        /// <param name="sequenceString">The string representation of the sequence</param>
        /// <param name="seperatorString">The seperator in the string representation</param>
        /// <param name="maskCode">The mask code that describes and summarizes the charater of a sequence</param>
        /// <returns>Result of computing(true: the squence matches the character that the mask code describes; false: the squence does NOT matche the character that the mask code describes)</returns>
        public static bool IsSequenceMask(string sequenceString, string seperatorString, string maskCode)
        {
            bool returnValue = false;

            int maskCount = 0;

            string[] seperatorStringArray = new string[] { seperatorString };
            string[] sequenceStringArray = sequenceString.Split(seperatorStringArray, StringSplitOptions.RemoveEmptyEntries);

            if ((sequenceStringArray != null) && (sequenceStringArray.Length > 0))
            {
                for (int i = 0; i < sequenceStringArray.Length; i++)
                {
                    if (sequenceStringArray[i] == maskCode)
                    {
                        maskCount++;
                    }
                }

                if ((maskCount > 0) && (maskCount == sequenceStringArray.Length))
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }
    }
}
