using Newtonsoft.Json.Converters;

namespace TesteDev.Infra.Utils
{
    /// <summary>
    /// Classe para converter DateTime no formato desejado. (retorno de pesquisa)
    /// </summary>
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "dd/MM/yyyy HH:mm";
        }
    }
}
