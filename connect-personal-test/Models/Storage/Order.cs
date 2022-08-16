using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace connect_personal_test.Models.Storage
{
    /// <summary>
    /// Заказ.
    /// </summary>
    [Table("Order", Schema = "dbo")]
    [Comment("Заказчики")]
    public class Order
    {
        /// <summary>
        /// ИД записи.
        /// </summary>
        [Key]
        [Comment("ИД записи")]
        public int Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        [Required]
        [Comment("Наименование")]
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [Comment("Описание")]
        public string Description { get; set; }

        /// <summary>
        /// Значение.
        /// </summary>
        [Required]
        [Comment("Значение")]
        public string Value { get; set; }

        /// <summary>
        /// Привязанные категории заказов.
        /// </summary>
        [InverseProperty("Order")]
        [Comment("Привязанные категории заказов")]
        public virtual ICollection<OrderCategory> OrderCategories { get; set; }
    }
}
