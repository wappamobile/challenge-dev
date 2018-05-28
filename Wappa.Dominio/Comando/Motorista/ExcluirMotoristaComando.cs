using Flunt.Notifications;
using Flunt.Validations;

namespace Wappa.Dominio.Comando.Motorista
{
    public class ExcluirMotoristaComando : Notifiable, IComando
    {
        public ExcluirMotoristaComando(int id)
        {
            this.Id = id;
        }

        public int Id { get; private set; }

        public void Validar()
        {
            AddNotifications(new Contract()
                            .Requires()
                            .IsTrue(Id > 0, "Id", "Id é obrigatória"));
        }
    }
}