using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicLibrary.Server.Migrations
{
    /// <inheritdoc />
    public partial class Refactored : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Musician",
                table: "Musician");

            migrationBuilder.RenameTable(
                name: "Musician",
                newName: "Musicians");

            migrationBuilder.RenameColumn(
                name: "Fullname",
                table: "Musicians",
                newName: "Bio");

            migrationBuilder.AddColumn<int>(
                name: "StarPoint",
                table: "Musicians",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Musicians",
                table: "Musicians",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlbumMusician",
                columns: table => new
                {
                    AlbumId = table.Column<int>(type: "int", nullable: false),
                    MusicianId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumMusician", x => new { x.AlbumId, x.MusicianId });
                    table.ForeignKey(
                        name: "FK_AlbumMusician_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumMusician_Musicians_MusicianId",
                        column: x => x.MusicianId,
                        principalTable: "Musicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumMusician_MusicianId",
                table: "AlbumMusician",
                column: "MusicianId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumMusician");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Musicians",
                table: "Musicians");

            migrationBuilder.DropColumn(
                name: "StarPoint",
                table: "Musicians");

            migrationBuilder.RenameTable(
                name: "Musicians",
                newName: "Musician");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "Musician",
                newName: "Fullname");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Musician",
                table: "Musician",
                column: "Id");
        }
    }
}
