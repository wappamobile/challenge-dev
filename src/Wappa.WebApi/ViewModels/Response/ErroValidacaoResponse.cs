using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wappa.WebApi.ViewModels.Common;

namespace Wappa.WebApi.ViewModels.Response
{
    public class ErroValidacaoResponse
    {
        public CampoInvalido[] CamposInvalidos { get; set; }

        public ErroValidacaoResponse()
        {

        }

        public ErroValidacaoResponse(ValidationResult validation)
        {
            this.CamposInvalidos =
                           validation.Errors.Select(x => new CampoInvalido()
                           {
                               Campo = x.PropertyName,
                               Mensagem = x.ErrorMessage
                           }).ToArray();
        }
        public ErroValidacaoResponse(ValidationResult validation, string campoAdicional, string mensagem)
        {
            var camposInvalidos = validation.Errors.Select(x => new CampoInvalido()
            {
                Campo = x.PropertyName,
                Mensagem = x.ErrorMessage
            }).ToList();
            camposInvalidos.Add(new CampoInvalido()
            {
                Campo = campoAdicional,
                Mensagem = mensagem
            });
            this.CamposInvalidos = camposInvalidos.ToArray();


        }
    }
}
