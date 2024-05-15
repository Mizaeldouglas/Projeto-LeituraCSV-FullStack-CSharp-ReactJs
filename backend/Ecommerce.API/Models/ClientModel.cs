
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Ecommerce.API.Models;

public class ClientModel
{
    [Key]
    public string CPF_CNPJ { get; set; }
    public string Name { get; set; }
    public string CEP { get; set; }
    
    [JsonIgnore]
    public ICollection<OrderModel> Orders { get; set; }
}