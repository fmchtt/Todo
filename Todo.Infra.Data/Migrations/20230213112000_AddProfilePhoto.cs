using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddProfilePhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itens_Boards_BoardId",
                table: "Itens");

            migrationBuilder.DropForeignKey(
                name: "FK_Itens_Columns_ColumnId",
                table: "Itens");

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Itens_Boards_BoardId",
                table: "Itens",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Itens_Columns_ColumnId",
                table: "Itens",
                column: "ColumnId",
                principalTable: "Columns",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itens_Boards_BoardId",
                table: "Itens");

            migrationBuilder.DropForeignKey(
                name: "FK_Itens_Columns_ColumnId",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Itens_Boards_BoardId",
                table: "Itens",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Itens_Columns_ColumnId",
                table: "Itens",
                column: "ColumnId",
                principalTable: "Columns",
                principalColumn: "Id");
        }
    }
}
