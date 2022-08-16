using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace connect_personal_test.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ИД записи")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Наименование")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                },
                comment: "Категория");

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ИД записи")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Наименование"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Описание"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Значение")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                },
                comment: "Заказчики");

            migrationBuilder.CreateTable(
                name: "OrderCategory",
                schema: "dbo",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false, comment: "ИД заказа"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "ИД категории")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCategory", x => new { x.OrderId, x.CategoryId });
                },
                comment: "Категория заказа");

            migrationBuilder.CreateIndex(
                name: "IX_UQ_Categories_Name",
                schema: "dbo",
                table: "Category",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OrderCategory",
                schema: "dbo");
        }
    }
}
