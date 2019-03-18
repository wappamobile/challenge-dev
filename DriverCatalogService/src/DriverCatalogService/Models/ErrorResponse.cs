using System.Collections.Generic;

namespace DriverCatalogService.Models
{
    public class ErrorResponse
    {
        public Error[] Errors { get; set; }
    }

    public class Error
    {
        public string Where { get; set; }
        public string Problem { get; set; }
    }
}