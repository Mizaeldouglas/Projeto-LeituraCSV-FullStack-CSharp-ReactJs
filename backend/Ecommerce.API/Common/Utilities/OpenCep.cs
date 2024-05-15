using System.Text.Json;
using Ecommerce.API.Models;

namespace Ecommerce.API.Common.Utilities;

public class OpenCep
{
    public static async Task<string> GetRegionOpenCep(string cep)
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync($"https://opencep.com/v1/{cep}");
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