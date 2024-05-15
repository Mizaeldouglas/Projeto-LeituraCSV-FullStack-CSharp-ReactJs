using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Ecommerce.API.Models;

public class ProductModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Preco { get; set; }
    
    [JsonIgnore]
    public ICollection<OrderModel> Orders { get; set; }
}