using System.ComponentModel.DataAnnotations;

namespace connect_personal_test.API.Orders
{
    /// <summary>
    /// Запрос на редактирование заказа.
    /// </summary>
    public class OrdersEditRequest
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Значение.
        /// </summary>
        [Required]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// ИД категорий.
        /// </summary>
        [Required]
        public int[] CategoryIds { get; set; } = Array.Empty<int>();
    }
}
