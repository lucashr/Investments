using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Investments.Domain;
using Xunit;

namespace Investments.Tests
{
    public class CepTestServiceTest
    {
        const string ApiCep = "https://cep.awesomeapi.com.br/json/";

        [Theory]
        [InlineData("05513300")]
        public async Task DeveretornarInformacoesDeUmCepValido(string cep)
        {

            var result = await GetCepInfoAsync(cep);

            result.Item2.Should().NotBeNull();
            result.Item2.Should().HaveStatusCode(HttpStatusCode.OK);

            result.Item1.Should().NotBeNull();
            result.Item1.ZipCode.Should().Be("05513300");
            result.Item1.Address.Should().Be("Avenida Professor Francisco Morato");
            result.Item1.State.Should().Be("SP");
            result.Item1.District.Should().Be("Butantã");
            result.Item1.City.Should().Be("São Paulo");

        }

        [Theory]
        [InlineData("05513399")]
        public async Task DeveretornarStatusCode404(string cep)
        {

            var result = await GetCepInfoAsync(cep);

            result.Item2.Should().NotBeNull();
            result.Item2.Should().HaveStatusCode(HttpStatusCode.NotFound);

        }

        [Theory]
        [InlineData("030320320320321")]
        public async Task DeveretornarStatusCode400(string cep)
        {

            var result = await GetCepInfoAsync(cep);

            result.Item2.Should().NotBeNull();
            result.Item2.Should().HaveStatusCode(HttpStatusCode.BadRequest);

        }

        private async Task<(UserAddress, HttpResponseMessage)> GetCepInfoAsync(string cep)
        {

            var url = $"{ApiCep}/{cep}";
            (UserAddress, HttpResponseMessage) result = (null, null);

            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.StatusCode != HttpStatusCode.OK)
                {

                    result.Item1 = null;
                    result.Item2 = response;

                    return result;

                }
                    
                var content = await response.Content.ReadAsStringAsync();

                result.Item1 = JsonSerializer.Deserialize<UserAddress>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                result.Item2 = response;
            }
                
            return result;
        }
    }
}