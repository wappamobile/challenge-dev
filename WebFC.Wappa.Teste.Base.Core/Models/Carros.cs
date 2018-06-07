using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFC.Wappa.Teste.Base.Core.Models
{
    public class Carros
    {

        [Key]
        public Guid IdCarro { get; set; }

        public string Marca { get; set;  }

        public string Modelo { get; set;  }

        public string Placa { get; set; }

        public string Ano { get; set; }

        public int Ativo { get; set; }
    
        public Guid IdMotorista { get; set;  }
        [ForeignKey("IdMotorista")]


        [JsonIgnore]
        public virtual Motoristas Motorista { get; set;  }
    }
}
