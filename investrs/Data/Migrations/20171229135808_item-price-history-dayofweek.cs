using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace investrs.Data.Migrations
{
    public partial class itempricehistorydayofweek : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DayOfWeek",
                table: "ItemPriceHistory",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "ItemPriceHistory");
        }
    }
}
