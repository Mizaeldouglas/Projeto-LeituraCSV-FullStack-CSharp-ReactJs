using System.Text.Json.Serialization;

namespace Ecommerce.API.Models;

public class OrderModel
{
    public int Id { get; set; }
    public string CPF_CNPJ { get; set; }
    public string Name { get; set; }
    public string CEP { get; set; }
    public int NumberOrder { get; set; }
    public DateTime Data { get; set; }
    public decimal TotalPrice { get; set; }
    
    [JsonIgnore]
    public int ProductId { get; set; }
    
    public ProductModel Product { get; set; }  
    
    [JsonIgnore]
    public string ClientCPF_CNPJ { get; set; }
    public ClientModel Client { get; set; }
}