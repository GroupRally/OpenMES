using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MES.Utility
{
    public class TracingUtility
    {
        public static string DefaultTraceSourceName = "MESTraceSource";

        public static void Trace(object[] data, string traceSourceName) 
        {
            try
            {
                string sourceName = !String.IsNullOrEmpty(traceSourceName) ? traceSourceName : TracingUtility.DefaultTraceSourceName; 

                System.Diagnostics.TraceSource trace = new System.Diagnostics.TraceSource(sourceName);

                trace.TraceData(System.Diagnostics.TraceEventType.Information, new Random().Next(), data);

                trace.Flush();
            }
            catch (Exception)
            {
                //If you want to handle this exception, add your exception handling code here, else you may uncomment the following line to throw this exception out.
                throw;
            }
        }
    }
}
