using Infra.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net;
using System.Net.Http;
using Xunit;

namespace Test.API
{
    public class MotoristasTest : IClassFixture<TestFixture>
    {
        public MotoristasTest(TestFixture fixture)
        {
            _client = fixture.Client;
            _context = fixture.Context;
        }

        public HttpClient _client { get; }
        public Context _context { get; }

        [Fact]
        public async void Get_DadosExistentes_DeveListar()
        {
            var motorista = ModelHelper.CadastrarMotorista(_context);

            var request = new HttpRequestMessage(HttpMethod.Get, "api/motoristas");

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var json = await response.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(json);
            var itens = jObject["itens"];

            Assert.True(itens.Count() > 0);
            Assert.Contains(itens, x => (int)x["motoristaId"] == motorista.MotoristaId);
        }

        [Fact]
        public async void Get_SortByNome_DeveOrdenar()
        {
            var motorista = ModelHelper.CadastrarMotorista(_context, "aaaaaaa", "dddddd");
            var motorista2 = ModelHelper.CadastrarMotorista(_context, "dddddd", "aaaaaaa");

            var request = new HttpRequestMessage(HttpMethod.Get, "api/motoristas?sortby=nome");

            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(json);
            var itens = jObject["itens"];
            Assert.True(itens.Count() > 0);

            var id = itens.FirstOrDefault(x => (int)x["motoristaId"] == motorista.MotoristaId)["motoristaId"];
            Assert.NotNull(id);
            Assert.Equal(motorista.MotoristaId, (int)id);
        }

        [Fact]
        public async void Get_SortBySobrenome_DeveOrdenar()
        {
            var motorista = ModelHelper.CadastrarMotorista(_context, "dddddddd", "aaaaaaa");
            var motorista2 = ModelHelper.CadastrarMotorista(_context, "aaaaaaa", "dddddddd");

            var request = new HttpRequestMessage(HttpMethod.Get, "api/motoristas?sortby=sobrenome");

            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(json);
            var itens = jObject["itens"];
            Assert.True(itens.Count() > 0);

            var id = itens.FirstOrDefault(x => (int)x["motoristaId"] == motorista2.MotoristaId)["motoristaId"];
            Assert.NotNull(id);
            Assert.Equal(motorista2.MotoristaId, (int)id);
        }

        [Fact]
        public async void Get_SortByInvalido_NaoDeveOrdenar()
        {
            var motorista = ModelHelper.CadastrarMotorista(_context, "dddddddd", "aaaaaaa");
            var motorista2 = ModelHelper.CadastrarMotorista(_context, "aaaaaaa", "dddddddd");

            var request = new HttpRequestMessage(HttpMethod.Get, "api/motoristas?sortby=dakfod");

            var response = await _client.SendAsync(request);
            Assert.Equal((HttpStatusCode)422, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();

        }

    }
}
