using System.ComponentModel.DataAnnotations;

namespace connect_personal_test.API.Categories
{
    /// <summary>
    /// Запрос на редактирование категории.
    /// </summary>
    public class CategoriesEditRequest
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
