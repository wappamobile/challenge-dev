namespace TesteDev.Infra.Entidades
{
    public class EnderecoCompleto : EntidadeBase
    {
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }

        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }

    }
}
