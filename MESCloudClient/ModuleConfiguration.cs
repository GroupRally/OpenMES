using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MESCloud.Client
{
    public class ModuleConfiguration
    {
        public static string ServicePoint;

        public static string UrlTest = "/Test/";

        public static string UrlNewResult = "/Result/New/";

        public static string AuthorizationHeaderValue = "MES:M(S@OMSG.msft";

        public static string EncodingName = "utf-8";

        public static bool IsTracingEnabled = true;

        public static string TraceSourceName = "MESCloudClientTraceSource";

        public static string GetServicePoint(string serverAddress, string servicePoint) 
        {
            if ((!serverAddress.EndsWith("/")) && (!servicePoint.StartsWith("/")))
            {
                return serverAddress + "/" + servicePoint;
            }

            if (serverAddress.EndsWith("/") && servicePoint.StartsWith("/"))
            {
                return serverAddress + servicePoint.Substring(1);
            }

            return serverAddress + servicePoint;
        }
    }
}
