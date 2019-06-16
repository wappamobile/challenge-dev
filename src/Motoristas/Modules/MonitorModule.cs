using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motoristas.Modules
{
    public class MonitorModule : NancyModule
    {
        public MonitorModule() : base("/_monitor")
        {
            Get("/shallow", _ => Negotiate.WithStatusCode(HttpStatusCode.OK));
        }
    }
}
