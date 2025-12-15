using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bloomify.Migrations
{
    /// <inheritdoc />
    public partial class ReinitWishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TABLE IF EXISTS WishlistItems");
            migrationBuilder.Sql("DROP TABLE IF EXISTS Wishlists");
            migrationBuilder.Sql(@"
                DECLARE @ConstraintName nvarchar(200)
                SELECT @ConstraintName = Name FROM sys.foreign_keys WHERE parent_object_id = OBJECT_ID('Orders') AND referenced_object_id = OBJECT_ID('Users')
                IF @ConstraintName IS NOT NULL
                    EXEC('ALTER TABLE Orders DROP CONSTRAINT ' + @ConstraintName)
            ");
            
            migrationBuilder.Sql(@"
                DECLARE @ConstraintName2 nvarchar(200)
                SELECT @ConstraintName2 = Name FROM sys.foreign_keys WHERE parent_object_id = OBJECT_ID('Reviews') AND referenced_object_id = OBJECT_ID('Users')
                IF @ConstraintName2 IS NOT NULL
                    EXEC('ALTER TABLE Reviews DROP CONSTRAINT ' + @ConstraintName2)
            ");

            migrationBuilder.Sql(@"
                DECLARE @ConstraintName3 nvarchar(200)
                SELECT @ConstraintName3 = Name FROM sys.foreign_keys WHERE parent_object_id = OBJECT_ID('ShoppingCarts') AND referenced_object_id = OBJECT_ID('Users')
                IF @ConstraintName3 IS NOT NULL
                    EXEC('ALTER TABLE ShoppingCarts DROP CONSTRAINT ' + @ConstraintName3)
            ");

            // Truncate tables to allow new FKs to empty AspNetUsers
            migrationBuilder.Sql("DELETE FROM OrderItems");
            migrationBuilder.Sql("DELETE FROM Orders");
            migrationBuilder.Sql("DELETE FROM Reviews");
            migrationBuilder.Sql("DELETE FROM ShoppingCartItems");
            migrationBuilder.Sql("DELETE FROM ShoppingCarts");
            migrationBuilder.Sql("DELETE FROM ShippingDetails");


             // Drop old Users table
            migrationBuilder.Sql("IF OBJECT_ID('Users', 'U') IS NOT NULL DROP TABLE Users");


            /*
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
               ...
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
               ...
             migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
               ...
            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
               ...
            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
               ...
            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
               ...
            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
               ...
            */

            /*
            migrationBuilder.CreateTable(
                name: "Orders",
                ...
            */
            // Since we skipped creating Orders, we need to add the FK manually
            /*migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_userID",
                table: "Orders",
                column: "userID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);*/


            /*
            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                ...
            */
            /*migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_userID",
                table: "ShoppingCarts",
                column: "userID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);*/


            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    WishlistID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.WishlistID);
                    table.ForeignKey(
                        name: "FK_Wishlists_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

             /*
            migrationBuilder.CreateTable(
                name: "Products",
                ...
            */

             /*
            migrationBuilder.CreateTable(
                name: "ShippingDetails",
                ...
            */

             /*
            migrationBuilder.CreateTable(
                name: "OrderItems",
                ...
            */

             /*
            migrationBuilder.CreateTable(
                name: "Reviews",
                ...
            */
            /*
             migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UserID",
                table: "Reviews",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            */


            /*
            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                ...
            */

            migrationBuilder.CreateTable(
                name: "WishlistItems",
                columns: table => new
                {
                    WishlistItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WishlistID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItems", x => x.WishlistItemID);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Wishlists_WishlistID",
                        column: x => x.WishlistID,
                        principalTable: "Wishlists",
                        principalColumn: "WishlistID",
                        onDelete: ReferentialAction.Cascade);
                });

            // Seeding - Commented out
            /*
            migrationBuilder.InsertData(
                table: "Categories",
                ...
            */

            /*migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");*/

            // Existing indexes commented out
            /*
            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_orderID",
                ...
            */

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_ProductID",
                table: "WishlistItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_WishlistID",
                table: "WishlistItems",
                column: "WishlistID");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserID",
                table: "Wishlists",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Simplified Down for safety
            migrationBuilder.DropTable(name: "WishlistItems");
            migrationBuilder.DropTable(name: "Wishlists");
            
            // Reverting FKs and Tables would be complex and likely break more if state is mixed.
            // Leaving minimal.
        }
    }
}
