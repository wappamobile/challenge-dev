using Wappa.Middleware.Application.Drivers;
using Wappa.Middleware.Application.Drivers.Dto;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Wappa.Middleware.Test
{
    public class DriverAppService_Tests : IClassFixture<TestClientProvider>
    {
        private readonly IDriverAppService _driverAppService;

        private ServiceProvider _serviceProvider;

        public DriverAppService_Tests(TestClientProvider fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
            _driverAppService = _serviceProvider.GetService<IDriverAppService>();
        }

        [Fact]
        public async Task GetAll_Test()
        {
            var result = await _driverAppService.GetAll();
            result.Count.ShouldBe(0);
        }

        [Fact]
        public async Task Create_Update_And_Delete_Test()
        {
            //CREATE
            var resultId = await _driverAppService.Create(
                new CreateOrEditDriverDto
                {
                    Id = 0,
                    Address = "Rua Mozelos",
                    Number = 389,
                    ZipCode = "02073100",
                    District = "Vila Paiva",
                    City = "Sao Pauo",
                    State = "SP",
                    Complement = "",
                    FirtName = "Fernando",
                    LastName = "Guilherme"
                });

            var drivers = await _driverAppService.GetAll();
                drivers.Exists(t => t.FirtName == "Fernando").ShouldBe(true);
                drivers.Exists(t => t.LastName == "Guilherme").ShouldBe(true);

            //EDIT
            var edit = await _driverAppService.GetForEdit(resultId.Data.Value);

            edit.FirtName.ShouldBe("Fernando");
            edit.LastName.ShouldBe("Guilherme");

            edit.FirtName = "Fernando-UPD";
            edit.LastName = "Guilherme-UPD";

            await _driverAppService.Update(edit);

            drivers = await _driverAppService.GetAll();
            drivers.Exists(t => t.FirtName == "Fernando-UPD").ShouldBe(true);
            drivers.Exists(t => t.LastName == "Guilherme-UPD").ShouldBe(true);

            //DELETE
            await _driverAppService.Delete(edit.Id);

            drivers = await _driverAppService.GetAll();
            drivers.Find(t => t.FirtName == "Fernando-UPD").ShouldBe(null);
        }

        //[MultiTenantFact]
        //public async Task Create_Update_And_Delete_Tenant_Test()
        //{
        //    //CREATE --------------------------------

        //    //Act
        //    await _tenantAppService.CreateTenant(
        //        new CreateTenantInput
        //        {
        //            TenancyName = "testTenant",
        //            Name = "Tenant for test purpose",
        //            AdminEmailAddress = "admin@testtenant.com",
        //            AdminPassword = "123qwe",
        //            IsActive = true
        //        });

        //    //Assert
        //    var tenant = await GetTenantOrNullAsync("testTenant");
        //    tenant.ShouldNotBe(null);
        //    tenant.Name.ShouldBe("Tenant for test purpose");
        //    tenant.IsActive.ShouldBe(true);

        //    await UsingDbContextAsync(tenant.Id, async context =>
        //    {
        //        //Check static roles
        //        var staticRoleNames = Resolve<IRoleManagementConfig>().StaticRoles.Where(r => r.Side == MultiTenancySides.Tenant).Select(role => role.RoleName).ToList();
        //        foreach (var staticRoleName in staticRoleNames)
        //        {
        //            (await context.Roles.CountAsync(r => r.TenantId == tenant.Id && r.Name == staticRoleName)).ShouldBe(1);
        //        }

        //        //Check default admin user
        //        var adminUser = await context.Users.FirstOrDefaultAsync(u => u.TenantId == tenant.Id && u.UserName == User.AdminUserName);
        //        adminUser.ShouldNotBeNull();

        //        //Check notification registration
        //        (await context.NotificationSubscriptions.FirstOrDefaultAsync(ns => ns.UserId == adminUser.Id && ns.NotificationName == AppNotificationNames.NewUserRegistered)).ShouldNotBeNull();
        //    });

        //    //GET FOR EDIT -----------------------------

        //    //Act
        //    var editDto = await _tenantAppService.GetTenantForEdit(new EntityDto(tenant.Id));

        //    //Assert
        //    editDto.TenancyName.ShouldBe("testTenant");
        //    editDto.Name.ShouldBe("Tenant for test purpose");
        //    editDto.IsActive.ShouldBe(true);

        //    // UPDATE ----------------------------------

        //    editDto.Name = "edited tenant name";
        //    editDto.IsActive = false;
        //    await _tenantAppService.UpdateTenant(editDto);

        //    //Assert
        //    tenant = await GetTenantAsync("testTenant");
        //    tenant.Name.ShouldBe("edited tenant name");
        //    tenant.IsActive.ShouldBe(false);

        //    // DELETE ----------------------------------

        //    //Act
        //    await _tenantAppService.DeleteTenant(new EntityDto((await GetTenantAsync("testTenant")).Id));

        //    //Assert
        //    (await GetTenantOrNullAsync("testTenant")).IsDeleted.ShouldBe(true);
        //}
    }
}
