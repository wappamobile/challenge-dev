using WappaChallenge.Util.Entidades;

namespace WappaChallenge.DTO
{
    public abstract class BaseDTO : ObjetoValidavel
    {
        public int Id { get; set; }
    }
}
