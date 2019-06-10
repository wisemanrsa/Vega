using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vega_be.Migrations
{
    public partial class SeedFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            GetFeatures().ForEach(feature => migrationBuilder.Sql($"INSERT INTO FEATURES(Name) VALUES('{feature}')"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            GetFeatures().ForEach(feature => migrationBuilder.Sql($"DELETE FROM FEATURES WHERE Name = '{feature}'"));
        }

        protected List<string> GetFeatures()
        {
            return new List<string>() { "USB", "CD", "AC", "Sunroof", "Heated Seats", "Electric Windows" };
        }
    }
}
