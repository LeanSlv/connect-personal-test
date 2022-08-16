using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace connect_personal_test.Models.Storage
{
    /// <summary>
    /// Категория.
    /// </summary>
    [Table("Category", Schema = "dbo")]
    [Comment("Категория")]
    public class Category
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
        /// Привязанные категории заказов.
        /// </summary>
        [InverseProperty("Category")]
        [Comment("Привязанные категории заказов")]
        public virtual ICollection<OrderCategory> OrderCategories { get; set; }
    }
}
