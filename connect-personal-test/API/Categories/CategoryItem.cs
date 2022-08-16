using System.ComponentModel.DataAnnotations;

namespace connect_personal_test.API.Categories
{
    /// <summary>
    /// Информация о категории.
    /// </summary>
    public class CategoryItem
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
    }
}
