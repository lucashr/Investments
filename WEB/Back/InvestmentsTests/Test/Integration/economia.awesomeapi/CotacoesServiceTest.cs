using System;
using System.Collections.Generic;
using Investments.Domain;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Investments.Tests.Test.Cep.awesomeapi.UnitTest
{
    public class CotacoesServiceTest
    {
        const string ApiCotacao = "https://economia.awesomeapi.com.br/json/last/";

        [Theory]
        [InlineData("USD-BRL")]
        public async Task DeveretornarCotacaoAtualDaMoedaUsd(string moeda)
        {

            var result = await GetCepInfoAsync(moeda);
            string dctCodigoMoeda = moeda.Replace("-", "");

            result.Item2.Should().NotBeNull();
            result.Item2.Should().HaveStatusCode(HttpStatusCode.OK);

            result.Item1.Should().NotBeNull();

            result.Item1[dctCodigoMoeda].code.Should().Be("USD");
            result.Item1[dctCodigoMoeda].codein.Should().Be("BRL");
            result.Item1[dctCodigoMoeda].name.Should().Be("Dólar Americano/Real Brasileiro");
            result.Item1[dctCodigoMoeda].create_date.Should().Contain(DateTime.Now.Date.ToString("yyyy-MM-dd"));

        }

        [Theory]
        [InlineData("USD-BRL,EUR-BRL,BTC-BRL")]
        public async Task DeveretornarCotacaoAtualDeUsdEurBtc(string moeda)
        {

            var result = await GetCepInfoAsync(moeda);

            List<string> moedas = new List<string>();
            moedas.AddRange(moeda.Replace("-", "").Split(','));

            result.Item2.Should().NotBeNull();
            result.Item2.Should().HaveStatusCode(HttpStatusCode.OK);

            result.Item1.Should().NotBeNull();
            result.Item1.Should().HaveCount(3);

            result.Item1[moedas[0]].code.Should().Be("USD");
            result.Item1[moedas[0]].create_date.Should().Contain(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            result.Item1[moedas[1]].code.Should().Be("EUR");
            result.Item1[moedas[1]].create_date.Should().Contain(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            result.Item1[moedas[2]].code.Should().Be("BTC");
            result.Item1[moedas[2]].create_date.Should().Contain(DateTime.Now.Date.ToString("yyyy-MM-dd"));

        }

        private async Task<(Dictionary<string, Moeda>, HttpResponseMessage)> GetCepInfoAsync(string moeda)
        {

            var url = $"{ApiCotacao}/{moeda}";
            (Dictionary<string, Moeda>, HttpResponseMessage) result = (null, null);

            using var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(url);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                result.Item1 = null;
                result.Item2 = response;

                return result;
            }

            var content = await response.Content.ReadAsStringAsync();

            result.Item1 = JsonSerializer.Deserialize<Dictionary<string, Moeda>> (content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            result.Item2 = response;

            return result;
        }
    }
}