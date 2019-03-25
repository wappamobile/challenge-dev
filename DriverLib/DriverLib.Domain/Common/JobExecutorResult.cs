using System;
using System.Collections.Generic;
using System.Text;

namespace DriverLib.Domain.Common
{
    public class JobExecutorResult
    {
        public bool Success { get; set; }
        public IList<string> Messages { get; set; }
    }
}
