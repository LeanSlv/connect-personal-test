using System.ComponentModel.DataAnnotations;

namespace connect_personal_test.API.Categories
{
    /// <summary>
    /// Ответ на запрос списка категорий.
    /// </summary>
    public class CategoriesListResponse
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
        public CategoryItem[] Items { get; set; } = Array.Empty<CategoryItem>();
    }
}
