using System.ComponentModel.DataAnnotations;

namespace connect_personal_test.API.Orders
{
    /// <summary>
    /// Информация о заказе.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// ИД записи.
        /// </summary>
        [Required]
        public int Id { get; set; }

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
        /// Список названий категорий.
        /// </summary>
        public string[]? CategoryNames { get; set; }
    }
}
