using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WappaMobile.Driver.BackgroundTasks
{
    public class BackgroundTaskSettings
    {
        public string ConnectionString { get; set; }

        public string Database { get; set; }

        public int TaskDelay { get; set; }

        public string GeocodeUrl { get; set; }

        public string GeocodeKey { get; set; }
    }
}
