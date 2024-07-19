using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("70742977-7205-4d72-ba3d-95005df776d2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6f7ada63-4e8c-42be-bd91-2349b88daef8"));

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "PasswordHash", "PasswordSalt", "UpdatedDate", "UserName" },
                values: new object[] { new Guid("f69a0e02-e108-432c-b8e7-fb0efb6aa36b"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@blog.com", new byte[] { 220, 84, 117, 119, 70, 102, 200, 12, 136, 5, 156, 136, 111, 14, 125, 145, 219, 17, 106, 124, 20, 21, 198, 137, 125, 29, 114, 67, 84, 94, 149, 221, 200, 177, 127, 78, 253, 140, 197, 189, 63, 244, 252, 204, 234, 112, 50, 17, 63, 89, 25, 6, 214, 86, 90, 183, 18, 229, 17, 189, 38, 186, 187, 78 }, new byte[] { 47, 141, 40, 209, 205, 83, 66, 72, 129, 78, 212, 241, 111, 5, 42, 19, 178, 220, 233, 103, 242, 146, 23, 207, 15, 112, 245, 195, 32, 36, 240, 119, 111, 187, 94, 100, 43, 151, 239, 244, 27, 83, 208, 13, 190, 37, 235, 44, 4, 1, 189, 99, 149, 24, 62, 218, 56, 155, 33, 238, 27, 229, 30, 34, 18, 89, 193, 248, 190, 80, 97, 96, 41, 57, 163, 119, 142, 253, 39, 114, 14, 94, 239, 225, 191, 194, 36, 152, 55, 63, 239, 58, 206, 15, 80, 108, 160, 205, 58, 50, 19, 58, 169, 52, 117, 71, 178, 202, 52, 155, 199, 240, 140, 161, 45, 160, 74, 36, 159, 240, 119, 22, 194, 201, 208, 200, 66, 4 }, null, "Admin" });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("0a07fb27-61ee-48cf-b4cc-299b41a7cd88"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("f69a0e02-e108-432c-b8e7-fb0efb6aa36b") });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentId",
                table: "Comments",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParentId",
                table: "Comments",
                column: "ParentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParentId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ParentId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("0a07fb27-61ee-48cf-b4cc-299b41a7cd88"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f69a0e02-e108-432c-b8e7-fb0efb6aa36b"));

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Comments");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "PasswordHash", "PasswordSalt", "UpdatedDate", "UserName" },
                values: new object[] { new Guid("6f7ada63-4e8c-42be-bd91-2349b88daef8"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@blog.com", new byte[] { 92, 239, 43, 253, 252, 16, 14, 131, 21, 76, 57, 187, 64, 64, 240, 150, 174, 181, 109, 33, 46, 85, 62, 71, 153, 20, 141, 208, 81, 194, 80, 190, 59, 89, 200, 50, 213, 186, 177, 117, 11, 240, 38, 195, 197, 44, 11, 65, 7, 80, 2, 184, 226, 30, 237, 71, 145, 248, 124, 160, 152, 114, 84, 13 }, new byte[] { 138, 145, 40, 153, 91, 19, 187, 142, 233, 3, 247, 120, 114, 27, 162, 126, 22, 91, 210, 87, 61, 151, 48, 38, 180, 210, 183, 204, 132, 37, 252, 177, 111, 220, 198, 209, 213, 107, 6, 138, 26, 63, 20, 80, 85, 123, 112, 210, 130, 84, 222, 173, 117, 114, 208, 44, 209, 154, 195, 63, 97, 143, 94, 76, 35, 155, 168, 146, 61, 129, 137, 187, 39, 6, 57, 221, 99, 210, 224, 147, 37, 101, 184, 157, 125, 39, 30, 142, 225, 193, 249, 103, 116, 233, 10, 13, 214, 252, 162, 53, 164, 50, 72, 245, 0, 254, 64, 144, 8, 37, 160, 230, 237, 208, 83, 57, 87, 2, 235, 2, 165, 35, 249, 18, 45, 73, 4, 181 }, null, "Admin" });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("70742977-7205-4d72-ba3d-95005df776d2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("6f7ada63-4e8c-42be-bd91-2349b88daef8") });
        }
    }
}
