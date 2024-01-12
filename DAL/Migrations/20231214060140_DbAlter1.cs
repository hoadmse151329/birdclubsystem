using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class DbAlter1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Users",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_Blog",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_User",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Gallery_Users",
                table: "Gallery");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Users",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Member",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_memberId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "UQ__Users__66DCF95CC4AB3072",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_User_memberId",
                table: "User",
                column: "memberId",
                unique: true,
                filter: "([memberId] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UQ__User__66DCF95CC4AB3072",
                table: "User",
                column: "userName",
                unique: true,
                filter: "([userName] IS NOT NULL)");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_User",
                table: "Blog",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Comments",
                table: "Comment",
                column: "blogId",
                principalTable: "Blog",
                principalColumn: "blogId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Comments",
                table: "Comment",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gallery_User",
                table: "Gallery",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_User",
                table: "News",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Member",
                table: "User",
                column: "memberId",
                principalTable: "Member",
                principalColumn: "memberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_User",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Comments",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Comments",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Gallery_User",
                table: "Gallery");

            migrationBuilder.DropForeignKey(
                name: "FK_News_User",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Member",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_memberId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "UQ__User__66DCF95CC4AB3072",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_memberId",
                table: "Users",
                column: "memberId",
                unique: true,
                filter: "[memberId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__66DCF95CC4AB3072",
                table: "Users",
                column: "userName",
                unique: true,
                filter: "[userName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Users",
                table: "Blog",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog",
                table: "Comment",
                column: "blogId",
                principalTable: "Blog",
                principalColumn: "blogId");

            migrationBuilder.AddForeignKey(
                name: "FK_User",
                table: "Comment",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gallery_Users",
                table: "Gallery",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Users",
                table: "News",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Member",
                table: "Users",
                column: "memberId",
                principalTable: "Member",
                principalColumn: "memberId");
        }
    }
}
