using Autofac;

namespace TesteDev.Servicos
{
    /// <summary>
    /// Módulo para gerar o container que será utilizado na DI
    /// </summary>
    public class ModuleServicos : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Cria container para IoC e DI
            builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
                   .Where(t => t.Name.EndsWith("Servico"))
                   .AsImplementedInterfaces();
            base.Load(builder);
        }
    }
}
