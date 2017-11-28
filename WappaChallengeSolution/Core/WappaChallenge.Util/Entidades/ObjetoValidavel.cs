using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WappaChallenge.Dominio.Exceptions;

namespace WappaChallenge.Util.Entidades
{
    public abstract class ObjetoValidavel
    {
        public ICollection<ValidationResult> Erros { get; protected set; }

        public virtual void ValidarEntidade()
        {
            this.Erros = new HashSet<ValidationResult>();

            if (!Validator.TryValidateObject(this, new ValidationContext(this), this.Erros, true))
                throw new EntidadeInvalidaException(this.GetType(), this.Erros);
        }
    }
}
