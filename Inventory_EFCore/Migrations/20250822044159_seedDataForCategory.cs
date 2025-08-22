using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCore_DBFirstApp.Migrations
{
    /// <inheritdoc />
    public partial class seedDataForCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "category_id", "category_name" },
                values: new object[,]
                {
                    { -4, "Default 04" },
                    { -3, "Default 03" },
                    { -2, "Default 02" },
                    { -1, "Default 01" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
