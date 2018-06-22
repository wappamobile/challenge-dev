using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vitor.Application;
using Vitor.Domain.Messages.Response;
using Vitor.Domain.Service;

namespace Vitor.Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestAcessToDriverService()
        {
            IDriverService component = Shared.CreateDriverService();

            Assert.IsNotNull(component);
            Assert.IsInstanceOfType(component, typeof(DriverService));
        }

        [TestMethod]
        public void TestGetDriver()
        {
            IDriverService component = Shared.CreateDriverService();
            var response = component.GetDriver(1).Result;

            Assert.IsInstanceOfType(response, typeof(GetDriverResponse));
        }

        [TestMethod]
        public void TestCrudDriver()
        {
            IDriverService component = Shared.CreateDriverService();

            var insertRequest = Shared.GenerateDriverToInsert();
            var insertResponse = component.InsertDriver(insertRequest).Result;
            Assert.IsInstanceOfType(insertResponse, typeof(InsertDriverResponse));

            var getDriverByEmailRequest = insertRequest.Driver.Email;
            var getDriverByEmaiResponse = component.Getdriverbyemail(getDriverByEmailRequest).Result;
            Assert.IsInstanceOfType(getDriverByEmaiResponse, typeof(GetDriverResponse));

            var updateDriverRequest = Shared.GenerateDriverToUpdate(getDriverByEmaiResponse.Driver);
            var updateDriverResponse = component.UpdateDriver(updateDriverRequest).Result;
            Assert.IsInstanceOfType(updateDriverResponse, typeof(UpdateDriverResponse));

            var deleteDriverRequest = updateDriverRequest.Driver.Id;
            var deleteDriverResponse = component.DeleteDriver(deleteDriverRequest).Result;
            Assert.IsInstanceOfType(deleteDriverResponse, typeof(DeleteDriverResponse));
        }
    }
}
