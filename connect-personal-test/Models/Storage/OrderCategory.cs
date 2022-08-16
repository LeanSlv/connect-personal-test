using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace connect_personal_test.Models.Storage
{
    /// <summary>
    /// Категория заказа.
    /// </summary>
    [Table("OrderCategory", Schema = "dbo")]
    [Comment("Категория заказа")]
    public class OrderCategory
    {
        /// <summary>
        /// ИД заказа.
        /// </summary>
        [Comment("ИД заказа")]
        public int OrderId { get; set; }

        /// <summary>
        /// Заказ.
        /// </summary>
        [ForeignKey(nameof(OrderId))]
        [Comment("Заказ")]
        public virtual Order Order { get; set; }

        /// <summary>
        /// ИД категории.
        /// </summary>
        [Comment("ИД категории")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Категория.
        /// </summary>
        [ForeignKey(nameof(CategoryId))]
        [Comment("Категория")]
        public virtual Category Category { get; set; }
    }
}
