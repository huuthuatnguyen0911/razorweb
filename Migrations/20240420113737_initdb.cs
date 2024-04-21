using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Bogus;
using Models;

#nullable disable

namespace Learn_code.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            Randomizer.Seed = new Random(8675309);

            var fakerArticle = new Faker<Article>()
                .RuleFor(a => a.Title, f => f.Lorem.Sentence(5, 5))
                .RuleFor(a => a.Created, f => f.Date.Past())
                .RuleFor(a => a.Content, f => f.Lorem.Paragraphs(3));

            for (int i = 0; i < 10; i++)
            {
                migrationBuilder.InsertData(
                    table: "Articles",
                    columns: new[] { "Title", "Created", "Content" },
                    values: new object[] { fakerArticle.Generate().Title, fakerArticle.Generate().Created, fakerArticle.Generate().Content });
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}
