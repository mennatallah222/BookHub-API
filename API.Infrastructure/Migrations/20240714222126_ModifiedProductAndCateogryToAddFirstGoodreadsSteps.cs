using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedProductAndCateogryToAddFirstGoodreadsSteps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "PagesNumber");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId2",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedAt",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                    table.ForeignKey(
                        name: "FK_Authors_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "BookGenres",
                columns: table => new
                {
                    BookGenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenres", x => x.BookGenreId);
                    table.ForeignKey(
                        name: "FK_BookGenres_Categories_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGenres_Products_BookId",
                        column: x => x.BookId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FriendId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_FriendId",
                        column: x => x.FriendId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Friendships_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_AuthorId",
                table: "Products",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CustomerId",
                table: "Products",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CustomerId1",
                table: "Products",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CustomerId2",
                table: "Products",
                column: "CustomerId2");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_CustomerId",
                table: "Authors",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_BookId",
                table: "BookGenres",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_GenreId",
                table: "BookGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_CustomerId",
                table: "Friendships",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_FriendId",
                table: "Friendships",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_UserId",
                table: "Friendships",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Authors_AuthorId",
                table: "Products",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Customers_CustomerId",
                table: "Products",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Customers_CustomerId1",
                table: "Products",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Customers_CustomerId2",
                table: "Products",
                column: "CustomerId2",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Authors_AuthorId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Customers_CustomerId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Customers_CustomerId1",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Customers_CustomerId2",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "BookGenres");

            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Products_AuthorId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CustomerId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CustomerId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CustomerId2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CustomerId2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PublishedAt",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "PagesNumber",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");
        }
    }
}
