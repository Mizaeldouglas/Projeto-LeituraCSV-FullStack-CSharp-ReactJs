namespace Ecommerce.API.Common.Utilities;

public class RegionFreightRates
{
    public static readonly Dictionary<string, decimal> Rates = new Dictionary<string, decimal>
    {
        {"AM", 0.3m}, {"RR", 0.3m}, {"AP", 0.3m}, {"PA", 0.3m}, {"TO", 0.3m}, {"RO", 0.3m}, {"AC", 0.3m}, {"MA", 0.3m}, {"PI", 0.3m}, {"CE", 0.3m}, {"RN", 0.3m}, {"PE", 0.3m}, {"PB", 0.3m}, {"SE", 0.3m}, {"AL", 0.3m}, {"BA", 0.3m},
        {"MT", 0.2m}, {"MS", 0.2m}, {"GO", 0.2m}, {"DF", 0.2m}, {"RS", 0.2m}, {"SC", 0.2m}, {"PR", 0.2m},
        {"SP", 0.1m}, {"RJ", 0.1m}, {"ES", 0.1m}, {"MG", 0.1m}
    };
}