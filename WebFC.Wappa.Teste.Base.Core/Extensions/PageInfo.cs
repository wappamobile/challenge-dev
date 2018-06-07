using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace WebFC.Wappa.Teste.Base.Core.Extensions
{
    public class PageInfo
    {
        [NotMapped]
        [ScriptIgnore]
        [JsonIgnore]
        public int RecordsTotal { get; set; }

        [NotMapped]
        [ScriptIgnore]
        [JsonIgnore]
        public int Start { get; set; }

        [NotMapped]
        [ScriptIgnore]
        [JsonIgnore]
        public int Length { get; set; }

        [NotMapped]
        [ScriptIgnore]
        [JsonIgnore]
        public string OrderColumn { get; set; }


        [NotMapped]
        [ScriptIgnore]
        [JsonIgnore]
        public string OrderDirection { get; set; }

        [NotMapped]
        [ScriptIgnore]
        [JsonIgnore]
        public string Buscar { get; set; }

        [NotMapped]
        [ScriptIgnore]
        [JsonIgnore]
        public int draw { get; set; }

        [NotMapped]
        [ScriptIgnore]
        [JsonIgnore]
        public string Status { get; set; }
    }
}
