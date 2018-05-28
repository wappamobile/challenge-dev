namespace Wappa.Dominio.Resultado
{
    public class ResultadoComando : IResultadoComando
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }

        public object Data { get; set; }

        public ResultadoComando()
        {
        }

        public ResultadoComando(bool sucesso, string mensagem, object data)
        {
            this.Sucesso = sucesso;
            this.Mensagem = mensagem;
            this.Data = data;
        }
    }
}