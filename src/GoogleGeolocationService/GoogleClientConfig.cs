using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleGeolocationService
{
    public class GoogleClientConfig
    {
        public GoogleClientConfig(string uriAddress, string accessKey)
        {
            UriAddress = uriAddress;
            AccessKey = accessKey;
        }

        public string UriAddress { get; }
        public string AccessKey { get; }
    }
}
