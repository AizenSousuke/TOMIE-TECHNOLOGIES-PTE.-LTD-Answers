using TomieTechnologies.Models;

namespace TomieTechnologies.Classes
{
    /// <summary>
    /// Fake DB
    /// </summary>
    public static class OrdersDB
    {
        public static List<OrderModel> Orders { get; set; } = new List<OrderModel>();
    }
}