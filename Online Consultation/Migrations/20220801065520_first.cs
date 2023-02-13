using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Consultation.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "doctorsProfiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    docImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Docname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    speciality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fees = table.Column<int>(type: "int", nullable: false),
                    avail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctorsProfiles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "medicines",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mprice = table.Column<int>(type: "int", nullable: false),
                    mfg = table.Column<DateTime>(type: "datetime2", nullable: false),
                    exp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicines", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "patientProfiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientProfiles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    serviceDetail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Empname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.id);
                    table.ForeignKey(
                        name: "FK_Employees_departments_DepId",
                        column: x => x.DepId,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pid = table.Column<int>(type: "int", nullable: false),
                    did = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointments", x => x.id);
                    table.ForeignKey(
                        name: "FK_appointments_doctorsProfiles_did",
                        column: x => x.did,
                        principalTable: "doctorsProfiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_appointments_patientProfiles_pid",
                        column: x => x.pid,
                        principalTable: "patientProfiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "billings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    billingdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mid = table.Column<int>(type: "int", nullable: false),
                    mdesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pid = table.Column<int>(type: "int", nullable: false),
                    did = table.Column<int>(type: "int", nullable: false),
                    totalFee = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_billings", x => x.id);
                    table.ForeignKey(
                        name: "FK_billings_doctorsProfiles_did",
                        column: x => x.did,
                        principalTable: "doctorsProfiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_billings_medicines_mid",
                        column: x => x.mid,
                        principalTable: "medicines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_billings_patientProfiles_pid",
                        column: x => x.pid,
                        principalTable: "patientProfiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "feedbacks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pid = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rating = table.Column<byte>(type: "tinyint", nullable: false),
                    feedbackTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feedbacks", x => x.id);
                    table.ForeignKey(
                        name: "FK_feedbacks_patientProfiles_pid",
                        column: x => x.pid,
                        principalTable: "patientProfiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_did",
                table: "appointments",
                column: "did");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_pid",
                table: "appointments",
                column: "pid");

            migrationBuilder.CreateIndex(
                name: "IX_billings_did",
                table: "billings",
                column: "did");

            migrationBuilder.CreateIndex(
                name: "IX_billings_mid",
                table: "billings",
                column: "mid");

            migrationBuilder.CreateIndex(
                name: "IX_billings_pid",
                table: "billings",
                column: "pid");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepId",
                table: "Employees",
                column: "DepId");

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_pid",
                table: "feedbacks",
                column: "pid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "billings");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "feedbacks");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "doctorsProfiles");

            migrationBuilder.DropTable(
                name: "medicines");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "patientProfiles");
        }
    }
}
