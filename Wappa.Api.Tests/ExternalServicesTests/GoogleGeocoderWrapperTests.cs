using AutoFixture;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Wappa.Api.ExternalServices;
using Wappa.Api.ExternalServices.Exceptions;
using Wappa.Api.Models;
using Xunit;

namespace Wappa.Api.Tests.ExternalServicesTests
{
    public class GoogleGeocoderWrapperTests
    {
		private static IConfigurationRoot configuration;
		private static Fixture fixture;

		static GoogleGeocoderWrapperTests()
		{
			//deve ser alterado sempre que clonar o projeto para refletir o user secrets do desenvolvedor atual, definido em Wapp.Api.csproj se nao existir criar um click direito -> manage user secrets
			var userSecretsId = "30cfa5cd-dbf3-4d4d-8f62-3da6a947de53"; 

			var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("test-settings.json")
			.AddUserSecrets(userSecretsId);

			configuration = builder.Build();

			fixture = new Fixture();
		}

		[Fact]
		public async Task When_send_an_address_to_GoogleGeocoderWrapper_its_return_must_not_be_null()
		{
			//Arrange
			var googleGeocoderWrapper = new GoogleGeocoderWrapper(configuration);
			var address = new Address
			{
				AddressLine = "Rua Cananeia 38",
				City = "Barueri",
				State = "SP"
			};

			//Act
			var result = await googleGeocoderWrapper.GetAddress(address.ToString());

			//Assert
			Assert.NotNull(result);
		}

		[Fact]
		public async Task When_send_an_address_to_GoogleGeocoderWrapper_must_return_a_list_of_GoogleAddress()
		{
			//Arrange
			var googleGeocoderWrapper = new GoogleGeocoderWrapper(configuration);
			var address = new Address
			{
				AddressLine = "Rua Cananeia 38",
				City = "Barueri",
				State = "SP"
			};

			//Act
			var result = await googleGeocoderWrapper.GetAddress(address.ToString());

			//Assert
			Assert.IsAssignableFrom<IList<GoogleAddress>>(result);
		}

		[Fact]
		public async Task When_send_an_invalid_address_to_GoogleGeocoderWrapper_must_throw_an_AddressNotFoundException()
		{
			//Arrange
			var googleGeocoderWrapper = new GoogleGeocoderWrapper(configuration);
			var address = fixture.Create<Address>();

			//Act -> Assert
			await Assert.ThrowsAsync<AddressNotFoundException>(async () => await googleGeocoderWrapper.GetAddress(address.ToString()));
		}

		[Fact]
		public async Task When_send_a_NullOrEmpty_address_to_GoogleGeocoderWrapper_must_throw_an_ArgumentNullException()
		{
			//Arrange
			var googleGeocoderWrapper = new GoogleGeocoderWrapper(configuration);
			var address = new Address();

			//Act -> Assert
			await Assert.ThrowsAsync<ArgumentNullException>(() => googleGeocoderWrapper.GetAddress(address.ToString()));
		}
	}
}
