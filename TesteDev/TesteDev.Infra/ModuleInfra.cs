using Autofac;

namespace TesteDev.Infra
{
    /// <summary>
    /// Módulo para gerar o container que será utilizado na DI
    /// </summary>
    public class ModuleInfra : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Cria container para IoC e DI
            builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
                   .Where(t => t.Name.EndsWith("Repositorio"))
                   .AsImplementedInterfaces();
            base.Load(builder);
        }
    }
}
