using Wappa.CoordenadasGeograficas.API.Models;
using Wappa.Core.Messages;

namespace Wappa.CoordenadasGeograficas.API.Application.Events
{
	public class SolicitouCadastroMotoristaEvent : Event
	{
		public Endereco Endereco { get; set; }
	}
}
