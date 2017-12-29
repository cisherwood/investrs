using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace investrs.Data.Migrations
{
    public partial class itempricehistorydailyprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "ItemPriceHistory",
                newName: "DailyPrice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DailyPrice",
                table: "ItemPriceHistory",
                newName: "Price");
        }
    }
}
