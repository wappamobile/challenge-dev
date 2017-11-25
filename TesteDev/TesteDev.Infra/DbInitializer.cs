namespace TesteDev.Infra
{
    public class DbInitializer
    {
        /// <summary>
        /// Inicializa o bando de dados criando as tabelas
        /// </summary>
        /// <param name="contexto"></param>
        public static void Initialize(Contexto contexto)
        {
            contexto.Database.EnsureCreated();
        }
    }
}
