using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Test
{
    public class JsonContent : StringContent
    {
        public JsonContent(object value)
            : base(JsonConvert.SerializeObject(value), Encoding.UTF8,
            "application/json")
        {
        }

    }
}
