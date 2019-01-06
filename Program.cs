using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFClientLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference.TestServiceClient client = new ServiceReference.TestServiceClient();
            client.Endpoint.Behaviors.Add(new InspectorBehavior());
            client.GetData(123);
            var path1 = Globals.Path1;
            var path2 = Globals.Path2;
        }
    }
}
