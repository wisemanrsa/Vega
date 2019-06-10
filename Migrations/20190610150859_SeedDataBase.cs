using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vega_be.Migrations
{
    public partial class SeedDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            GetMakes().ToList().ForEach(make => migrationBuilder.Sql($"INSERT INTO MAKES(Name) VALUES('{make}')"));

            GetBWMModels().ToList().ForEach(model => migrationBuilder.Sql($"INSERT INTO Models(Name, MakeId) VALUES('{model}', (SELECT ID FROM MAKES WHERE NAME = 'BMW'))"));
            GetVWModels().ToList().ForEach(model => migrationBuilder.Sql($"INSERT INTO Models(Name, MakeId) VALUES('{model}', (SELECT ID FROM MAKES WHERE NAME = 'Volkswagen'))"));
            GetKiaModels().ToList().ForEach(model => migrationBuilder.Sql($"INSERT INTO Models(Name, MakeId) VALUES('{model}', (SELECT ID FROM MAKES WHERE NAME = 'KIA'))"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            GetMakes().ToList().ForEach(make =>
            {
                migrationBuilder.Sql($"DELETE FROM MAKES WHERE Name = '{make}'");
                migrationBuilder.Sql($"DELETE FROM Models WHERE MakeId = (SELECT ID FROM MAKES WHERE NAME = '{make}')");
            });
        }

        protected string[] GetMakes()
        {
            return new string[] { "BMW", "Volkswagen", "KIA" };
        }

        protected string[] GetBWMModels()
        {
            return new string[] { "1 series", "M5 coupe", "X5", "M2 sport" };
        }

        protected string[] GetVWModels()
        {
            return new string[] { "Golf 7", "Polo", "Vivo", "Cross" };
        }

        protected string[] GetKiaModels()
        {
            return new string[] { "Picanto", "i30", "i20", "i10" };
        }
    }
}