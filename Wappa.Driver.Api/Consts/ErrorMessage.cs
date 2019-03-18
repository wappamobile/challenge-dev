
namespace Wappa.Driver.Api.Consts
{
    public class ErrorMessage
    {
        /// <summary>
        /// Mensagem genérica de errro
        /// </summary>
        public const string GeneralErrorMessage =
            "Serviço temporariamente indisponível. Tente novamente mais tarde.";

        #region .: Error Messages
        /// <summary>
        /// Campo obrigatório
        /// </summary>
        public const string FieldRequired = "O campo {0} é obrigatório";
        /// <summary>
        /// Motorista não encontrado
        /// </summary>
        public const string DriverNotFound = "Motorista não encontrado na base de dados";
        /// <summary>
        /// Endereço não encontrado
        /// </summary>
        public const string AddressNotFound = "Endereço não encontrado";
        #endregion
    }
}
