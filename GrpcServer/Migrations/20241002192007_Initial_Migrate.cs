using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GrpcServer.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "grpc_data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PacketSeqNum = table.Column<int>(type: "integer", nullable: false),
                    RecordSeqNum = table.Column<int>(type: "integer", nullable: false),
                    PacketTimestamp = table.Column<string>(type: "text", nullable: true),
                    Decimal1 = table.Column<double>(type: "double precision", nullable: false),
                    Decimal2 = table.Column<double>(type: "double precision", nullable: false),
                    Decimal3 = table.Column<double>(type: "double precision", nullable: false),
                    Decimal4 = table.Column<double>(type: "double precision", nullable: false),
                    RecordTimestamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grpc_data", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grpc_data");
        }
    }
}
