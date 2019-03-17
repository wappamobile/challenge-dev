using System;

namespace WappaMobile.ChallengeDev.Models
{
    public class Entidade
    {
        public Guid Id { get; set; }

        public Entidade()
        {
            Id = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            if(obj is Entidade entidade)
                return entidade.Id.Equals(Id);

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
