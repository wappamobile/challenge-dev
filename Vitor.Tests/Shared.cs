using Autofac;
using Autofac.Builder;
using Autofac.Core;
using System.Reflection;
using Vitor.Common;
using Vitor.CrossCutting;
using Vitor.Domain.Messages;
using Vitor.Domain.Messages.Request;
using Vitor.Domain.Model;
using Vitor.Domain.Service;

namespace Vitor.Tests
{
    public class Shared
    {
        public static IDriverService CreateDriverService()
        {
            IContainer container = LoadContainersForUnitTests();
            var component = container.Resolve<IDriverService>();
            return component;
        }

        private static IContainer LoadContainersForUnitTests()
        {
            var builder = new ContainerBuilder();

            var options = new InitializeOptions(new Assembly[0],
                                                new IModule[] {
                                                new VitorModule(),
                                                  new ConfigurationModule()
                                                }, ContainerBuildOptions.None);

            var container = IoCWrapper.InitializeContainer(options, builder);
            return container;
        }

        public static InsertDriverRequest GenerateDriverToInsert()
        {
            var driver = new InsertDriverRequest()
            {
                Driver = new Driver()
                {
                    FirstName = "Firstname",
                    LastName = "Lastname Goes",
                    Login = "1234",
                    Email = "teste@teste.unit.insert",
                    Vehicle = new Vehicle() { VehicleId = 1, CarId = 1, LicensePlate = "ABX1234" },
                    Address = new Address() { Street = "Rua Ramos Batista", Number = 198 }
                }
            };

            return driver;
        }

        public static UpdateDriverRequest GenerateDriverToUpdate(Driver driver)
        {
            var response = new UpdateDriverRequest();

            response.Driver = driver;
            response.Driver.FirstName = "UpdateFirstName";     

            return response;
        }
    }
}
