using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportLeader.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Leader",
                columns: table => new
                {
                    LeaderNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeaderName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Leader", x => x.LeaderNo);
                });

            migrationBuilder.CreateTable(
                name: "T_School",
                columns: table => new
                {
                    SchoolNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SchoolName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_School", x => x.SchoolNo);
                });

            migrationBuilder.CreateTable(
                name: "T_Sport",
                columns: table => new
                {
                    SportsNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SportsName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Sport", x => x.SportsNo);
                });

            migrationBuilder.CreateTable(
                name: "T_LeaderWorkInfo",
                columns: table => new
                {
                    LeaderNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SchoolNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SportsNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeaderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpDT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LeaderWorkInfo", x => x.LeaderNo);
                    table.ForeignKey(
                        name: "FK_T_LeaderWorkInfo_T_Leader_LeaderNo",
                        column: x => x.LeaderNo,
                        principalTable: "T_Leader",
                        principalColumn: "LeaderNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_LeaderWorkInfo_T_School_SchoolNo",
                        column: x => x.SchoolNo,
                        principalTable: "T_School",
                        principalColumn: "SchoolNo");
                    table.ForeignKey(
                        name: "FK_T_LeaderWorkInfo_T_Sport_SportsNo",
                        column: x => x.SportsNo,
                        principalTable: "T_Sport",
                        principalColumn: "SportsNo");
                });

            migrationBuilder.CreateTable(
                name: "T_Certificate",
                columns: table => new
                {
                    LeaderNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CertificateSequence = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CertificateNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CertificateDT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Organization = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Certificate", x => new { x.LeaderNo, x.CertificateSequence });
                    table.ForeignKey(
                        name: "FK_T_Certificate_T_LeaderWorkInfo_LeaderNo",
                        column: x => x.LeaderNo,
                        principalTable: "T_LeaderWorkInfo",
                        principalColumn: "LeaderNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_History",
                columns: table => new
                {
                    LeaderNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HistorySequence = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SchoolName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SportsNo = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_History", x => new { x.LeaderNo, x.HistorySequence });
                    table.ForeignKey(
                        name: "FK_T_History_T_LeaderWorkInfo_LeaderNo",
                        column: x => x.LeaderNo,
                        principalTable: "T_LeaderWorkInfo",
                        principalColumn: "LeaderNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_History_T_Sport_SportsNo",
                        column: x => x.SportsNo,
                        principalTable: "T_Sport",
                        principalColumn: "SportsNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_LeaderImage",
                columns: table => new
                {
                    LeaderNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeaderImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LeaderImage", x => x.LeaderNo);
                    table.ForeignKey(
                        name: "FK_T_LeaderImage_T_LeaderWorkInfo_LeaderNo",
                        column: x => x.LeaderNo,
                        principalTable: "T_LeaderWorkInfo",
                        principalColumn: "LeaderNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_History_SportsNo",
                table: "T_History",
                column: "SportsNo");

            migrationBuilder.CreateIndex(
                name: "IX_T_LeaderWorkInfo_SchoolNo",
                table: "T_LeaderWorkInfo",
                column: "SchoolNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_LeaderWorkInfo_SportsNo",
                table: "T_LeaderWorkInfo",
                column: "SportsNo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Certificate");

            migrationBuilder.DropTable(
                name: "T_History");

            migrationBuilder.DropTable(
                name: "T_LeaderImage");

            migrationBuilder.DropTable(
                name: "T_LeaderWorkInfo");

            migrationBuilder.DropTable(
                name: "T_Leader");

            migrationBuilder.DropTable(
                name: "T_School");

            migrationBuilder.DropTable(
                name: "T_Sport");
        }
    }
}
