namespace connect_personal_test.API.Orders
{
    /// <summary>
    /// Запрос на список заказов.
    /// </summary>
    public class OrdersListRequest
    {
        /// <summary>
        /// Количество записей.
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Отступ от начала списка.
        /// </summary>
        public int? Offset { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// ИД категории.
        /// </summary>
        public int? CategoryId { get; set; }
    }
}
