using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CustomerDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Customers_CustomerId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Customers_CustomerId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_FriendId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_UserId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Customers_CustomerId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Customers_CustomerId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Customers_CustomerId1",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Customers_CustomerId2",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Customers_CustomerId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Products_CustomerId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CustomerId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_CustomerId",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_FriendId",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_UserId",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Authors_CustomerId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "FriendId",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Reviews",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_CustomerId",
                table: "Reviews",
                newName: "IX_Reviews_UserId");

            migrationBuilder.RenameColumn(
                name: "CustomerId2",
                table: "Products",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CustomerId2",
                table: "Products",
                newName: "IX_Products_UserId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Carts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_CustomerId",
                table: "Carts",
                newName: "IX_Carts_UserId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserCurrentlyReading",
                columns: table => new
                {
                    CurrentlyReadingProductId = table.Column<int>(type: "int", nullable: false),
                    CurrentlyReadingUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCurrentlyReading", x => new { x.CurrentlyReadingProductId, x.CurrentlyReadingUsersId });
                    table.ForeignKey(
                        name: "FK_UserCurrentlyReading_AspNetUsers_CurrentlyReadingUsersId",
                        column: x => x.CurrentlyReadingUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCurrentlyReading_Products_CurrentlyReadingProductId",
                        column: x => x.CurrentlyReadingProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFavouriteAuthors",
                columns: table => new
                {
                    FavouriteAuthorsAuthorId = table.Column<int>(type: "int", nullable: false),
                    FavouriteByUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavouriteAuthors", x => new { x.FavouriteAuthorsAuthorId, x.FavouriteByUsersId });
                    table.ForeignKey(
                        name: "FK_UserFavouriteAuthors_AspNetUsers_FavouriteByUsersId",
                        column: x => x.FavouriteByUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavouriteAuthors_Authors_FavouriteAuthorsAuthorId",
                        column: x => x.FavouriteAuthorsAuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserReadBooks",
                columns: table => new
                {
                    ReadBooksProductId = table.Column<int>(type: "int", nullable: false),
                    ReadUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReadBooks", x => new { x.ReadBooksProductId, x.ReadUsersId });
                    table.ForeignKey(
                        name: "FK_UserReadBooks_AspNetUsers_ReadUsersId",
                        column: x => x.ReadUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserReadBooks_Products_ReadBooksProductId",
                        column: x => x.ReadBooksProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWantToRead",
                columns: table => new
                {
                    WantToReadProductId = table.Column<int>(type: "int", nullable: false),
                    WantToReadUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWantToRead", x => new { x.WantToReadProductId, x.WantToReadUsersId });
                    table.ForeignKey(
                        name: "FK_UserWantToRead_AspNetUsers_WantToReadUsersId",
                        column: x => x.WantToReadUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWantToRead_Products_WantToReadProductId",
                        column: x => x.WantToReadProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ReviewId",
                table: "AspNetUsers",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCurrentlyReading_CurrentlyReadingUsersId",
                table: "UserCurrentlyReading",
                column: "CurrentlyReadingUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavouriteAuthors_FavouriteByUsersId",
                table: "UserFavouriteAuthors",
                column: "FavouriteByUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReadBooks_ReadUsersId",
                table: "UserReadBooks",
                column: "ReadUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWantToRead_WantToReadUsersId",
                table: "UserWantToRead",
                column: "WantToReadUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Reviews_ReviewId",
                table: "AspNetUsers",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Reviews_ReviewId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "UserCurrentlyReading");

            migrationBuilder.DropTable(
                name: "UserFavouriteAuthors");

            migrationBuilder.DropTable(
                name: "UserReadBooks");

            migrationBuilder.DropTable(
                name: "UserWantToRead");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ReviewId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Reviews",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                newName: "IX_Reviews_CustomerId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Products",
                newName: "CustomerId2");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UserId",
                table: "Products",
                newName: "IX_Products_CustomerId2");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Carts",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                newName: "IX_Carts_CustomerId");

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
                name: "CustomerId",
                table: "Friendships",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FriendId",
                table: "Friendships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Friendships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Customers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Authors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CustomerId",
                table: "Products",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CustomerId1",
                table: "Products",
                column: "CustomerId1");

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

            migrationBuilder.CreateIndex(
                name: "IX_Authors_CustomerId",
                table: "Authors",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Customers_CustomerId",
                table: "Authors",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Customers_CustomerId",
                table: "Carts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_FriendId",
                table: "Friendships",
                column: "FriendId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_UserId",
                table: "Friendships",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Customers_CustomerId",
                table: "Friendships",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Customers_CustomerId",
                table: "Reviews",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
