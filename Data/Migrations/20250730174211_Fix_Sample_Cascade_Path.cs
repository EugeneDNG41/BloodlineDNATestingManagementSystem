using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Sample_Cascade_Path : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Samples_AspNetUsers_DonorId",
                table: "Samples");

            migrationBuilder.DropForeignKey(
                name: "FK_Samples_Results_ResultId",
                table: "Samples");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "454cb6f8-b35c-4f53-b2a6-84380ee4cd78");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6905476-8940-4ba7-8c23-fbf64fef4ca2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af8ae989-efe8-4b1c-93ae-852617e2ea7d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e72014d9-f5f1-4c6c-86f5-45dd10952995");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5e841e34-98d5-41ad-a36b-dbf2069c0009", null, "Staff", "STAFF" },
                    { "69d9c983-784f-4ddf-a395-aa85040c6cb3", null, "Manager", "MANAGER" },
                    { "9f5b82a8-38f5-4bf4-bafa-04f8cbfedda4", null, "Customer", "CUSTOMER" },
                    { "b0949f91-aecd-4d35-a2a5-1be9349ddbc2", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Samples_AspNetUsers_DonorId",
                table: "Samples",
                column: "DonorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Samples_Results_ResultId",
                table: "Samples",
                column: "ResultId",
                principalTable: "Results",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Samples_AspNetUsers_DonorId",
                table: "Samples");

            migrationBuilder.DropForeignKey(
                name: "FK_Samples_Results_ResultId",
                table: "Samples");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e841e34-98d5-41ad-a36b-dbf2069c0009");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69d9c983-784f-4ddf-a395-aa85040c6cb3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f5b82a8-38f5-4bf4-bafa-04f8cbfedda4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0949f91-aecd-4d35-a2a5-1be9349ddbc2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "454cb6f8-b35c-4f53-b2a6-84380ee4cd78", null, "Customer", "CUSTOMER" },
                    { "a6905476-8940-4ba7-8c23-fbf64fef4ca2", null, "Staff", "STAFF" },
                    { "af8ae989-efe8-4b1c-93ae-852617e2ea7d", null, "Manager", "MANAGER" },
                    { "e72014d9-f5f1-4c6c-86f5-45dd10952995", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Samples_AspNetUsers_DonorId",
                table: "Samples",
                column: "DonorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Samples_Results_ResultId",
                table: "Samples",
                column: "ResultId",
                principalTable: "Results",
                principalColumn: "Id");
        }
    }
}
