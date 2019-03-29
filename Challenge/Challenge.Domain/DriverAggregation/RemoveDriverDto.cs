using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Domain.DriverAggregation
{
    public class RemoveDriverDto : IQueryableById
    {
        public string Id { get; set; }
    }
}
