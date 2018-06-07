using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFC.Wappa.Teste.Base.Core.Models
{
    public class Motoristas
    {

        [Key]
        public Guid IdMotorista { get; set; }

        public string Nome { get; set; }

        public string SegundoNome { get; set;  }

        public string Lat { get; set;  }

        public string Long { get; set;  }

        public string Endereco { get; set; }

        public DateTime DataCadastro { get; set;  }
        public virtual ICollection<Carros> Carro { get; set;  }
    }
}
