using System;
using WappaChallenge.Util.Entidades;

namespace WappaChallenge.Dominio.Entidades
{
    public abstract class BaseDominio : ObjetoValidavel
    {
        public int Id { get; set; }
        public DateTime? DataCadastrado { get; protected set; } = DateTime.Now;
        
    }
}
