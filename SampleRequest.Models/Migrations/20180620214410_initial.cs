using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SamplesRequest.Models.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "catalogs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    key = table.Column<string>(type: "varchar(10)", nullable: true),
                    name = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalogs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    uname = table.Column<string>(type: "varchar(50)", nullable: false),
                    email = table.Column<string>(type: "varchar(60)", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.uname);
                });

            migrationBuilder.CreateTable(
                name: "workflow_steps",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(type: "varchar(150)", nullable: true),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    only_if_lab_test = table.Column<bool>(nullable: false),
                    order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workflow_steps", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    city = table.Column<string>(type: "varchar(30)", nullable: false),
                    client_id = table.Column<int>(nullable: false),
                    country = table.Column<string>(type: "varchar(30)", nullable: false),
                    facility = table.Column<string>(type: "varchar(10)", nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", nullable: false),
                    state = table.Column<string>(type: "varchar(30)", nullable: false),
                    street_number = table.Column<string>(type: "varchar(60)", nullable: false),
                    zip = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.id);
                    table.ForeignKey(
                        name: "FK_addresses_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workflow_users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    primary_responsible_id = table.Column<string>(type: "varchar(50)", nullable: true),
                    primary_responsible_name = table.Column<string>(type: "varchar(50)", nullable: true),
                    secondary_responsible_id = table.Column<string>(type: "varchar(50)", nullable: true),
                    secondary_responsible_name = table.Column<string>(type: "varchar(50)", nullable: true),
                    secundary_responsible_id = table.Column<string>(nullable: true),
                    workflow_step_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workflow_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_workflow_users_users_primary_responsible_id",
                        column: x => x.primary_responsible_id,
                        principalTable: "users",
                        principalColumn: "uname",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_workflow_users_users_secundary_responsible_id",
                        column: x => x.secundary_responsible_id,
                        principalTable: "users",
                        principalColumn: "uname",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_workflow_users_workflow_steps_workflow_step_id",
                        column: x => x.workflow_step_id,
                        principalTable: "workflow_steps",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sample_requests",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    address_id = table.Column<int>(nullable: true),
                    comments = table.Column<string>(type: "varchar(150)", nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    objective = table.Column<string>(type: "varchar(150)", nullable: false),
                    project_name = table.Column<string>(type: "varchar(50)", nullable: false),
                    require_test = table.Column<bool>(nullable: false),
                    sample_priority_id = table.Column<int>(nullable: false),
                    sample_purpose_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sample_requests", x => x.id);
                    table.ForeignKey(
                        name: "FK_sample_requests_addresses_address_id",
                        column: x => x.address_id,
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sample_requests_catalogs_sample_priority_id",
                        column: x => x.sample_priority_id,
                        principalTable: "catalogs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sample_requests_catalogs_sample_purpose_id",
                        column: x => x.sample_purpose_id,
                        principalTable: "catalogs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "request_workflow_steps",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    approved_by = table.Column<string>(nullable: true),
                    approved_date = table.Column<DateTime>(nullable: true),
                    comments = table.Column<string>(type: "varchar(200)", nullable: true),
                    order_workflow_step = table.Column<int>(nullable: false),
                    primary_responsible_id = table.Column<string>(type: "varchar(50)", nullable: false),
                    sample_request_id = table.Column<int>(nullable: false),
                    secondary_responsible_id = table.Column<string>(type: "varchar(50)", nullable: true),
                    workflow_step_id = table.Column<int>(nullable: false),
                    workflow_step_name = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_request_workflow_steps", x => x.id);
                    table.ForeignKey(
                        name: "FK_request_workflow_steps_users_primary_responsible_id",
                        column: x => x.primary_responsible_id,
                        principalTable: "users",
                        principalColumn: "uname",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_request_workflow_steps_sample_requests_sample_request_id",
                        column: x => x.sample_request_id,
                        principalTable: "sample_requests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_request_workflow_steps_users_secondary_responsible_id",
                        column: x => x.secondary_responsible_id,
                        principalTable: "users",
                        principalColumn: "uname",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_request_workflow_steps_workflow_steps_workflow_step_id",
                        column: x => x.workflow_step_id,
                        principalTable: "workflow_steps",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sample_request_details",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(type: "varchar(100)", nullable: false),
                    model_number = table.Column<string>(type: "varchar(20)", nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    request_id = table.Column<int>(nullable: false),
                    specs = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sample_request_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_sample_request_details_sample_requests_request_id",
                        column: x => x.request_id,
                        principalTable: "sample_requests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_addresses_client_id",
                table: "addresses",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_request_workflow_steps_primary_responsible_id",
                table: "request_workflow_steps",
                column: "primary_responsible_id");

            migrationBuilder.CreateIndex(
                name: "IX_request_workflow_steps_sample_request_id",
                table: "request_workflow_steps",
                column: "sample_request_id");

            migrationBuilder.CreateIndex(
                name: "IX_request_workflow_steps_secondary_responsible_id",
                table: "request_workflow_steps",
                column: "secondary_responsible_id");

            migrationBuilder.CreateIndex(
                name: "IX_request_workflow_steps_workflow_step_id",
                table: "request_workflow_steps",
                column: "workflow_step_id");

            migrationBuilder.CreateIndex(
                name: "IX_sample_request_details_request_id",
                table: "sample_request_details",
                column: "request_id");

            migrationBuilder.CreateIndex(
                name: "IX_sample_requests_address_id",
                table: "sample_requests",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_sample_requests_sample_priority_id",
                table: "sample_requests",
                column: "sample_priority_id");

            migrationBuilder.CreateIndex(
                name: "IX_sample_requests_sample_purpose_id",
                table: "sample_requests",
                column: "sample_purpose_id");

            migrationBuilder.CreateIndex(
                name: "IX_workflow_users_primary_responsible_id",
                table: "workflow_users",
                column: "primary_responsible_id");

            migrationBuilder.CreateIndex(
                name: "IX_workflow_users_secundary_responsible_id",
                table: "workflow_users",
                column: "secundary_responsible_id");

            migrationBuilder.CreateIndex(
                name: "IX_workflow_users_workflow_step_id",
                table: "workflow_users",
                column: "workflow_step_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "request_workflow_steps");

            migrationBuilder.DropTable(
                name: "sample_request_details");

            migrationBuilder.DropTable(
                name: "workflow_users");

            migrationBuilder.DropTable(
                name: "sample_requests");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "workflow_steps");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "catalogs");

            migrationBuilder.DropTable(
                name: "clients");
        }
    }
}
