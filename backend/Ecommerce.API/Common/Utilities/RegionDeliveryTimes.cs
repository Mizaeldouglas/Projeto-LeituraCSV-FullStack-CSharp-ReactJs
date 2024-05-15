namespace Ecommerce.API.Common.Utilities;

public class RegionDeliveryTimes
{
    public static readonly Dictionary<string, int> Times = new Dictionary<string, int>
    {
        {"AM", 10}, {"RR", 10}, {"AP", 10}, {"PA", 10}, {"TO", 10}, {"RO", 10}, {"AC", 10}, {"MA", 10}, {"PI", 10}, {"CE", 10}, {"RN", 10}, {"PE", 10}, {"PB", 10}, {"SE", 10}, {"AL", 10}, {"BA", 10},
        {"MT", 5}, {"MS", 5}, {"GO", 5}, {"DF", 5}, {"RS", 5}, {"SC", 5}, {"PR", 5},
        {"SP", 1}, {"RJ", 1}, {"ES", 1}, {"MG", 1}
    };
}