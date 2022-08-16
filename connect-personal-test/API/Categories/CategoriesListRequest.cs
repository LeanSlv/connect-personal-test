namespace connect_personal_test.API.Categories
{
    /// <summary>
    /// Запрос на список категорий.
    /// </summary>
    public class CategoriesListRequest
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
        /// Наименование
        /// </summary>
        public string? Name { get; set; }
    }
}
