namespace WappaMobile.ChallengeDev.Models
{
    public interface IMotoristas : ILeitura<Motorista>, IEscrita<Motorista>
    {
        Motorista BuscarPeloNome(Nome nome);
    }
}
