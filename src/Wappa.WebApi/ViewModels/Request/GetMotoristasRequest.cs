using System;
using Wappa.Domain.Common;

namespace Wappa.WebApi.ViewModels.Request
{
    public class GetMotoristasRequest
    {
        public string OrdenarPor { get; set; }

        public OrdenarPor ObterOrdenacao()
        {
            if (string.IsNullOrWhiteSpace(this.OrdenarPor)) throw new InvalidOperationException("parametro invalido");

            this.OrdenarPor = this.OrdenarPor.ToLower();

            switch (this.OrdenarPor)
            {
                case "nome":
                    return Domain.Common.OrdenarPor.Nome;
                case "sobrenome":
                    return Domain.Common.OrdenarPor.Sobrenome;
                default:
                    throw new InvalidOperationException("parametro invalido");
            }
        }
    }
}
