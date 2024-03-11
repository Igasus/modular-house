using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModularHouse.Server.DeviceManagement.Infrastructure.DataAccess.Database.Migrations
{
    /// <inheritdoc />
    public partial class Liquided_Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Users_CreatedByUserId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Users_LastUpdatedByUserId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Users_CreatedByUserId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Users_LastUpdatedByUserId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Routers_Users_CreatedByUserId",
                table: "Routers");

            migrationBuilder.DropForeignKey(
                name: "FK_Routers_Users_LastUpdatedByUserId",
                table: "Routers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Routers_CreatedByUserId",
                table: "Routers");

            migrationBuilder.DropIndex(
                name: "IX_Routers_LastUpdatedByUserId",
                table: "Routers");

            migrationBuilder.DropIndex(
                name: "IX_Modules_CreatedByUserId",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_LastUpdatedByUserId",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Areas_CreatedByUserId",
                table: "Areas");

            migrationBuilder.DropIndex(
                name: "IX_Areas_LastUpdatedByUserId",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Routers");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Routers");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserId",
                table: "Routers");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserId",
                table: "Areas");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedDate",
                table: "Routers",
                newName: "AdditionDate");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedDate",
                table: "Modules",
                newName: "AdditionDate");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedDate",
                table: "Areas",
                newName: "AdditionDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdditionDate",
                table: "Routers",
                newName: "LastUpdatedDate");

            migrationBuilder.RenameColumn(
                name: "AdditionDate",
                table: "Modules",
                newName: "LastUpdatedDate");

            migrationBuilder.RenameColumn(
                name: "AdditionDate",
                table: "Areas",
                newName: "LastUpdatedDate");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Routers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Routers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastUpdatedByUserId",
                table: "Routers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Modules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Modules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastUpdatedByUserId",
                table: "Modules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Areas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Areas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastUpdatedByUserId",
                table: "Areas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AdditionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Routers_CreatedByUserId",
                table: "Routers",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Routers_LastUpdatedByUserId",
                table: "Routers",
                column: "LastUpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CreatedByUserId",
                table: "Modules",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_LastUpdatedByUserId",
                table: "Modules",
                column: "LastUpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_CreatedByUserId",
                table: "Areas",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_LastUpdatedByUserId",
                table: "Areas",
                column: "LastUpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Users_CreatedByUserId",
                table: "Areas",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Users_LastUpdatedByUserId",
                table: "Areas",
                column: "LastUpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Users_CreatedByUserId",
                table: "Modules",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Users_LastUpdatedByUserId",
                table: "Modules",
                column: "LastUpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Routers_Users_CreatedByUserId",
                table: "Routers",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Routers_Users_LastUpdatedByUserId",
                table: "Routers",
                column: "LastUpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
