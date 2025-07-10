using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class newaddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Disttrict",
                table: "Addresses",
                newName: "District");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Results",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePictureUrl",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CancellationReason",
                table: "Appointments",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CancelledByUserId",
                table: "Appointments",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "SampleId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Results_UserId",
                table: "Results",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SampleId",
                table: "Appointments",
                column: "SampleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Samples_SampleId",
                table: "Appointments",
                column: "SampleId",
                principalTable: "Samples",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_AspNetUsers_UserId",
                table: "Results",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Samples_SampleId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_AspNetUsers_UserId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_UserId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_SampleId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "CancellationReason",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CancelledByUserId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "SampleId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "District",
                table: "Addresses",
                newName: "Disttrict");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "ProfilePictureUrl",
                keyValue: null,
                column: "ProfilePictureUrl",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePictureUrl",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
