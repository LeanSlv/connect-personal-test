namespace connect_personal_test.Extensions
{
    /// <summary>
    /// Методы расширения для Queryable.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Пагинация списка с проверками limit и offset.
        /// </summary>
        /// <typeparam name="T">Тип элементов списка.</typeparam>
        /// <param name="list">Список, для которого требуется пагинация.</param>
        /// <param name="limit">Количество записей, возвращаемое в запросе.</param>
        /// <param name="offset">Количество записей для пропуска.</param>
        /// <returns></returns>
        public static IQueryable<T> LimitOffset<T>(this IQueryable<T> list, int? limit, int? offset)
        {
            if (offset.HasValue)
                list = list.Skip(offset.Value);
            if (limit.HasValue)
                list = list.Take(limit.Value);

            return list;
        }
    }
}
