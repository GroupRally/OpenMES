using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestMESCloudClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MESCloud.Client.ModuleConfiguration.ServicePoint = "http://win-cb091ph1ltf:8919/Services/MESService.svc";
            MESCloud.Client.ModuleConfiguration.AuthorizationHeaderValue = "MES:M(S@OMSG.msft";

            string[] results = new MESCloud.Client.Manager(false, null).NewResult(Guid.NewGuid().ToString(), new Dictionary<string, string>() { { "item01", "item-01" }, { "item02", "item-02" } }, new byte[1024]);

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }

            //string result = new MESCloud.Client.Manager(false, null).Test();

            //Console.WriteLine(result);

            Console.Read();
        }
    }
}
