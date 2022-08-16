using System.ComponentModel.DataAnnotations;

namespace connect_personal_test.API.Orders
{
    /// <summary>
    /// Ответ на запрос списка заказов.
    /// </summary>
    public class OrdersListResponse
    {
        /// <summary>
        /// Общее количество записей.
        /// </summary>
        [Required]
        public int TotalCount { get; set; }

        /// <summary>
        /// Элементы ответа.
        /// </summary>
        [Required]
        public OrderItem[] Items { get; set; } = Array.Empty<OrderItem>();
    }
}
