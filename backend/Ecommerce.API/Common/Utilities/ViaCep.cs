using System.Text.Json;
using Ecommerce.API.Models;

namespace Ecommerce.API.Common.Utilities;

public class ViaCep
{
    private static async Task<string> GetRegionViaCep(string cep)
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var cepData = JsonSerializer.Deserialize<CepData>(content);
                return cepData.uf;
            }
        }
    
        return string.Empty;
    }
}