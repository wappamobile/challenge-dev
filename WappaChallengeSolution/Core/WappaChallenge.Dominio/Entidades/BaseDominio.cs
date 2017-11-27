using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WappaChallenge.Dominio.Exceptions;

namespace WappaChallenge.Dominio.Entidades
{
    public abstract class BaseDominio
    {
        public int Id { get; set; }
        public DateTime? DataCadastrado { get; protected set; } = DateTime.Now;
        public ICollection<ValidationResult> Erros { get; protected set; }

        protected virtual void ValidarEntidade()
        {
            this.Erros = new HashSet<ValidationResult>();

            if (!Validator.TryValidateObject(this, new ValidationContext(this), this.Erros, true))
                throw new EntidadeInvalidaException(this.GetType(), this.Erros);
        }
    }
}
