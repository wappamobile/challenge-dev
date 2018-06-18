using Infra.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using WebApi.ViewModels.Request;
using Xunit;

namespace Test.API
{
    public class CadastroMotoristaTest : IClassFixture<TestFixture>
    {
        public CadastroMotoristaTest(TestFixture fixture)
        {
            _client = fixture.Client;
            _context = fixture.Context;
        }

        public HttpClient _client { get; }
        public Context _context { get; }

        [Fact]
        public async void Post_DadosValidos_DeveCadastrar()
        {
            var modelRequest = ModelHelper.ObterMotoristaPostRequest();

            var request = new HttpRequestMessage(HttpMethod.Post, "api/cadastroMotorista");
            request.Content = new JsonContent(modelRequest);

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal("success", content);
        }

        [Fact]
        public async void Post_DadosNulos_NaoDeveCadastrar()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/cadastroMotorista");
            request.Content = new JsonContent(null);

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void Post_DadosInvalidos_NaoDeveCadastrar()
        {
            var modelRequest = ModelHelper.ObterMotoristaPostRequest();
            modelRequest.Cidade = null;
            modelRequest.Nome = string.Empty;

            var request = new HttpRequestMessage(HttpMethod.Post, "api/cadastroMotorista");
            request.Content = new JsonContent(modelRequest);

            var response = await _client.SendAsync(request);

            Assert.Equal((HttpStatusCode)422, response.StatusCode);
        }

        [Fact]
        public async void Put_DadosValidos_DeveAtualizar()
        {
            var modelRequest = ModelHelper.ObterMotoristaPutRequest(_context);

            var request = new HttpRequestMessage(HttpMethod.Put, "api/cadastroMotorista");
            request.Content = new JsonContent(modelRequest);

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("success", content);
        }

        [Fact]
        public async void Put_DadosInvalidos_NaoDeveAtualizar()
        {
            var modelRequest = new MotoristaCadastroPutRequest();

            var request = new HttpRequestMessage(HttpMethod.Put, "api/cadastroMotorista");
            request.Content = new JsonContent(modelRequest);

            var response = await _client.SendAsync(request);
            Assert.Equal((HttpStatusCode)422, response.StatusCode);
        }

        [Fact]
        public async void Put_MotoristaInvalido_NaoDeveAtualizar()
        {
            var modelRequest = ModelHelper.ObterMotoristaPutRequest(_context);
            modelRequest.MotoristaId = 94384384;

            var request = new HttpRequestMessage(HttpMethod.Put, "api/cadastroMotorista");
            request.Content = new JsonContent(modelRequest);

            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
        }

        [Fact]
        public async void Delete_IdValido_DeveRemover()
        {
            var modelRequest = ModelHelper.ObterMotoristaPutRequest(_context);

            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/cadastroMotorista/{modelRequest.MotoristaId}");

            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("deleted", content);

            var motoristaDb = _context.Motoristas
                .Where(x => x.MotoristaId == modelRequest.MotoristaId)
                .Where(x => x.Ativo);

            Assert.False(motoristaDb.Any());
        }

        [Fact]
        public async void Delete_IdInvalido_NaoDeveRemover()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/cadastroMotorista/3248");

            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }

    }
}
