using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TODOList.API.Migrations
{
    /// <inheritdoc />
    public partial class categoryIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TodoId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("12ccd14e-21af-4801-bf14-e80d6b1a2ff5"),
                column: "TodoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1ee26f62-463f-4125-801f-7fe91ae329ba"),
                column: "TodoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4521d9f7-c05f-4f76-aa18-6fba6b70e1bc"),
                column: "TodoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6b55a90f-ea79-4e85-accf-540be8f2e2a0"),
                column: "TodoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a65d0695-4f8b-49a7-a09a-5e0ec06a142e"),
                column: "TodoId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TodoId",
                table: "Categories",
                column: "TodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Todos_TodoId",
                table: "Categories",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Todos_TodoId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_TodoId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "TodoId",
                table: "Categories");
        }
    }
}
