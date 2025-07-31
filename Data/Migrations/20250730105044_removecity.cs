using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class removecity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19f4b95d-2150-4526-849c-5a700a1facbe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "579bf71d-6eb2-405e-86c8-bbbf73192c52");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87c7f82f-2fc3-485a-9771-49462e2ced70");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eebe71e7-6fc0-4abb-a104-cf46cce2a633");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Addresses");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c0a0380-8452-435b-a92f-128f699daf98", null, "Staff", "STAFF" },
                    { "a4375d6b-5b82-4be5-81da-7fd1e0f679cd", null, "Customer", "CUSTOMER" },
                    { "c2bc1eff-c79c-428f-8485-c2ae043208a1", null, "Admin", "ADMIN" },
                    { "d0c52a60-56f1-42cf-8d1f-5449cd8ef14d", null, "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c0a0380-8452-435b-a92f-128f699daf98");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4375d6b-5b82-4be5-81da-7fd1e0f679cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2bc1eff-c79c-428f-8485-c2ae043208a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0c52a60-56f1-42cf-8d1f-5449cd8ef14d");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Addresses",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "19f4b95d-2150-4526-849c-5a700a1facbe", null, "Admin", "ADMIN" },
                    { "579bf71d-6eb2-405e-86c8-bbbf73192c52", null, "Manager", "MANAGER" },
                    { "87c7f82f-2fc3-485a-9771-49462e2ced70", null, "Customer", "CUSTOMER" },
                    { "eebe71e7-6fc0-4abb-a104-cf46cce2a633", null, "Staff", "STAFF" }
                });
        }
    }
}
